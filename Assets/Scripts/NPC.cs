// Written by Joy de Ruijter
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum NPCType {Friendly, Protective, Hostile}

public class NPC : Unit
{
    #region Variables

    [Header("NPC Properties")]
    [SerializeField] protected NPCType npcType;
    [SerializeField] protected new string name;

    protected Rigidbody rb;
    protected Animator anim;
    protected CapsuleCollider col;

    // Movement & Pathfinding
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public Transform pathTransform;
    [HideInInspector] public bool pathIsACircuit;
    [HideInInspector] public int pathStartNode;
    [HideInInspector] public Direction pathStartDirection;
    [HideInInspector] public int currentNode;
    protected Direction currentDirection;
    protected NodePath path;
    protected List<Transform> nodes = new List<Transform>();

    // Checks for possible actions
    [HideInInspector] public bool canReactToPlayer;
    [HideInInspector] public float sightRange;
    [HideInInspector] public bool canInteractWithPlayer;
    [HideInInspector] public float interactRange;
    [HideInInspector] public bool moves;

    #endregion

    public NPC(NPCType _npcType, string _name)
    { 
        npcType = _npcType;
        name = _name;
    }

    public void FormNPC()
    {
        switch (npcType)
        { 
            case NPCType.Friendly:
                GameObject friendlyNpcObject = new GameObject("NPC_" + npcType + "_" + name, typeof(Rigidbody), typeof(Animator), typeof(CapsuleCollider), typeof(FriendlyNPC));
                FriendlyNPC friendlyNPC = friendlyNpcObject.GetComponent<FriendlyNPC>();
                friendlyNPC.name = name;
                friendlyNPC.npcType = npcType;
                friendlyNpcObject.transform.position = transform.position;
                friendlyNpcObject.transform.rotation = transform.rotation;
                friendlyNpcObject.transform.localScale = transform.localScale;
                friendlyNPC.rb = friendlyNpcObject.GetComponent<Rigidbody>();
                friendlyNPC.rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                friendlyNPC.anim = friendlyNpcObject.GetComponent<Animator>();
                friendlyNPC.col = friendlyNpcObject.GetComponent<CapsuleCollider>();
                friendlyNPC.col.center = new Vector3(0, 1, 0);
                friendlyNPC.col.radius = 0.25f;
                friendlyNPC.col.height = 2f;
                gameObject.transform.parent = friendlyNpcObject.transform;
                DestroyImmediate(this);
                break;

            case NPCType.Protective:
                GameObject protectiveNpcObject = new GameObject("NPC_" + npcType + "_" + name, typeof(Rigidbody), typeof(Animator), typeof(CapsuleCollider), typeof(ProtectiveNPC));
                ProtectiveNPC protectiveNPC = protectiveNpcObject.GetComponent<ProtectiveNPC>();
                protectiveNPC.name = name;
                protectiveNPC.npcType = npcType;
                protectiveNpcObject.transform.position = transform.position;
                protectiveNpcObject.transform.rotation = transform.rotation;
                protectiveNpcObject.transform.localScale = transform.localScale;
                protectiveNPC.rb = protectiveNpcObject.GetComponent<Rigidbody>();
                protectiveNPC.rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                protectiveNPC.anim = protectiveNpcObject.GetComponent<Animator>();
                protectiveNPC.col = protectiveNpcObject.GetComponent<CapsuleCollider>();
                protectiveNPC.col.center = new Vector3(0, 1, 0);
                protectiveNPC.col.radius = 0.25f;
                protectiveNPC.col.height = 2f;

                GameObject protectiveHolsterObject = new GameObject("WeaponHolster", typeof(WeaponHolster));
                protectiveHolsterObject.transform.parent = gameObject.transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Clavicle_R/Shoulder_R/Elbow_R/Hand_R");
                protectiveHolsterObject.transform.localScale = new Vector3(100, 100, 100);
                protectiveHolsterObject.transform.localPosition = Vector3.zero;
                protectiveHolsterObject.transform.localRotation = Quaternion.identity;
                protectiveNPC.holster = protectiveHolsterObject.GetComponent<WeaponHolster>();

                gameObject.transform.parent = protectiveNpcObject.transform;

                DestroyImmediate(this);
                break;

            case NPCType.Hostile:
                GameObject hostileNpcObject = new GameObject("NPC_" + npcType + "_" + name, typeof(Rigidbody), typeof(Animator), typeof(CapsuleCollider), typeof(HostileNPC));
                HostileNPC hostileNPC = hostileNpcObject.GetComponent<HostileNPC>();
                hostileNPC.name = name;
                hostileNPC.npcType = npcType;
                hostileNpcObject.transform.position = transform.position;
                hostileNpcObject.transform.rotation = transform.rotation;
                hostileNpcObject.transform.localScale = transform.localScale;
                hostileNPC.rb = hostileNpcObject.GetComponent<Rigidbody>();
                hostileNPC.rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                hostileNPC.anim = hostileNpcObject.GetComponent<Animator>();
                hostileNPC.col = hostileNpcObject.GetComponent<CapsuleCollider>();
                hostileNPC.col.center = new Vector3(0, 1, 0);
                hostileNPC.col.radius = 0.25f;
                hostileNPC.col.height = 2f;

                GameObject hostileHolsterObject = new GameObject("WeaponHolster", typeof(WeaponHolster));
                hostileHolsterObject.transform.parent = gameObject.transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Clavicle_R/Shoulder_R/Elbow_R/Hand_R");
                hostileHolsterObject.transform.localScale = new Vector3(100, 100, 100);
                hostileHolsterObject.transform.localPosition = Vector3.zero;
                hostileHolsterObject.transform.localRotation = Quaternion.identity;
                hostileNPC.holster = hostileHolsterObject.GetComponent<WeaponHolster>();

                gameObject.transform.parent = hostileNpcObject.transform;
                DestroyImmediate(this);
                break;

            default:
                break;
        }
    }

    #region Movement & Pathfinding

    protected void InitializePath()
    {
        Transform[] pathTransforms = pathTransform.GetComponentsInChildren<Transform>();
        if (nodes == null)
            nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != pathTransform.transform)
                nodes.Add(pathTransforms[i]);
        }
    }

    public virtual void Move()
    {
        CheckWaypointDistance();
        MoveToNode();
    }

    protected void MoveToNode()
    {
        transform.position = Vector3.MoveTowards(transform.position, nodes[currentNode].position, Time.deltaTime * moveSpeed);
        transform.LookAt(nodes[currentNode].position);
        transform.rotation = Quaternion.Euler(0, transform.localEulerAngles.y, 0);
    }

    protected void FlipDirection()
    {
        if (currentDirection == Direction.forward)
            currentDirection = Direction.backward;
        else if (currentDirection == Direction.backward)
            currentDirection = Direction.forward;        
    }

    protected void CheckWaypointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 0.3f)
        {
            if (currentNode == nodes.Count - 1 && currentDirection == Direction.forward && !path.isACircuit)
            {
                FlipDirection();
                currentNode--;
            }
            else if (currentNode == nodes.Count - 1 && currentDirection == Direction.forward && path.isACircuit)
                currentNode = 0;
            else if (currentNode == 0 && currentDirection == Direction.backward && !path.isACircuit)
            {
                FlipDirection();
                currentNode++;
            }
            else if (currentNode == 0 && currentDirection == Direction.backward && path.isACircuit)
                currentNode = nodes.Count - 1;
            else if (currentDirection == Direction.forward)
                currentNode++;
            else if (currentDirection == Direction.backward)
                currentNode--;
        }
    }

    #endregion
}

public enum Direction { forward, backward }
