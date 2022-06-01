// Written by Joy de Ruijter
using UnityEngine;

[System.Serializable]
public class FriendlyNPC : NPC
{
    #region Variables

    #endregion

    FriendlyNPC(NPCType _npcType, string _name)
    : base(_npcType, _name) { }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();

        if (moves)
        {
            path = pathTransform.GetComponent<NodePath>();
            path.isACircuit = pathIsACircuit;
            currentNode = pathStartNode;
            currentDirection = pathStartDirection;
            InitializePath();
            transform.position = nodes[pathStartNode].position;
        }

        maxHealth = 100;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (moves)
            Move();
    }

    private void OnDrawGizmosSelected()
    {
        if (canReactToPlayer)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }
        if (canInteractWithPlayer)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, interactRange);
        }
    }
}

