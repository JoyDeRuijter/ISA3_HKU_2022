// Written by Joy de Ruijter
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(NPC))]
public class NPCCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        NPC npc = (NPC)target;

        if(GUILayout.Button("Create NPC of this type"))
            npc.FormNPC();
    }
}

[CustomEditor(typeof(FriendlyNPC))]
public class FriendlyNPCCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        FriendlyNPC friendlyNPC = (FriendlyNPC)target;

        #region Can React To Player

        SerializedProperty canReactToPlayer = serializedObject.FindProperty("canReactToPlayer");
        canReactToPlayer.boolValue = EditorGUILayout.Toggle("NPC Can React To Player", canReactToPlayer.boolValue);

        SerializedProperty sightRange = serializedObject.FindProperty("sightRange");
        if (canReactToPlayer.boolValue)
        {
            sightRange.floatValue = EditorGUILayout.Slider("Sight Range", sightRange.floatValue, 0f, 50f);
        }
        else
            sightRange.floatValue = 0f;

        #endregion

        #region Can Interact With Player

        SerializedProperty canInteractWithPlayer = serializedObject.FindProperty("canInteractWithPlayer");
        canInteractWithPlayer.boolValue = EditorGUILayout.Toggle("NPC Can Interact With Player", canInteractWithPlayer.boolValue);

        SerializedProperty interactRange = serializedObject.FindProperty("interactRange");
        if (canInteractWithPlayer.boolValue)
        {
            float maxRange;
            if (!canReactToPlayer.boolValue || sightRange.floatValue == 0f)
                maxRange = 10f;
            else
                maxRange = sightRange.floatValue;

            interactRange.floatValue = EditorGUILayout.Slider("Interact Range", interactRange.floatValue, 0f, maxRange);
        }
        else
            interactRange.floatValue = 0f;

        #endregion

        #region Moves

        int maxNode;

        SerializedProperty moves = serializedObject.FindProperty("moves");
        moves.boolValue = EditorGUILayout.Toggle("NPC Moves", moves.boolValue);

        SerializedProperty moveSpeed = serializedObject.FindProperty("moveSpeed");
        SerializedProperty pathIsACircuit = serializedObject.FindProperty("pathIsACircuit");
        SerializedProperty pathStartNode = serializedObject.FindProperty("pathStartNode");
        SerializedProperty pathStartDirection = serializedObject.FindProperty("pathStartDirection");

        if (moves.boolValue)
        {
            moveSpeed.floatValue = EditorGUILayout.Slider("Movement Speed", moveSpeed.floatValue, 0.1f, 10f);
            friendlyNPC.pathTransform = EditorGUILayout.ObjectField("Path", friendlyNPC.pathTransform, typeof(Transform), true) as Transform;
            pathIsACircuit.boolValue = EditorGUILayout.Toggle("Path Is A Circuit", pathIsACircuit.boolValue);

            if (friendlyNPC.pathTransform != null)
                maxNode = friendlyNPC.pathTransform.GetComponent<NodePath>().nodes.Count - 1;
            else
                maxNode = 0;

            pathStartNode.intValue = EditorGUILayout.IntSlider("Start Node Index", pathStartNode.intValue, 0, maxNode);
            pathStartDirection.enumValueIndex = (int)(Direction)EditorGUILayout.EnumPopup("Start Direction", (Direction)pathStartDirection.enumValueIndex);
        }
        else
        { 
            moveSpeed.floatValue = 0f;
            friendlyNPC.pathTransform = null;
            pathIsACircuit.boolValue = false;
            pathStartNode.intValue = 0;
            pathStartDirection.enumValueIndex = (int)Direction.forward;
        }
        #endregion

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(friendlyNPC);
            EditorSceneManager.MarkSceneDirty(friendlyNPC.gameObject.scene);
        }
    }
}

[CustomEditor(typeof(ProtectiveNPC))]
public class ProtectiveNPCCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        ProtectiveNPC protectiveNPC = (ProtectiveNPC)target;

        #region Can React To Player

        SerializedProperty canReactToPlayer = serializedObject.FindProperty("canReactToPlayer");
        canReactToPlayer.boolValue = EditorGUILayout.Toggle("NPC Can React To Player", canReactToPlayer.boolValue);

        SerializedProperty sightRange = serializedObject.FindProperty("sightRange");
        if (canReactToPlayer.boolValue)
        {
            sightRange.floatValue = EditorGUILayout.Slider("Sight Range", sightRange.floatValue, 0f, 50f);
        }
        else
            sightRange.floatValue = 0f;

        #endregion

        #region Can Interact With Player

        SerializedProperty canInteractWithPlayer = serializedObject.FindProperty("canInteractWithPlayer");
        canInteractWithPlayer.boolValue = EditorGUILayout.Toggle("NPC Can Interact With Player", canInteractWithPlayer.boolValue);

        SerializedProperty interactRange = serializedObject.FindProperty("interactRange");
        if (canInteractWithPlayer.boolValue)
        {
            float maxRange;
            if (!canReactToPlayer.boolValue || sightRange.floatValue == 0f)
                maxRange = 10f;
            else
                maxRange = sightRange.floatValue;

            interactRange.floatValue = EditorGUILayout.Slider("Interact Range", interactRange.floatValue, 0f, maxRange);
        }
        else
            interactRange.floatValue = 0f;

        #endregion

        #region Moves

        int maxNode;

        SerializedProperty moves = serializedObject.FindProperty("moves");
        moves.boolValue = EditorGUILayout.Toggle("NPC Moves", moves.boolValue);

        SerializedProperty moveSpeed = serializedObject.FindProperty("moveSpeed");
        SerializedProperty pathIsACircuit = serializedObject.FindProperty("pathIsACircuit");
        SerializedProperty pathStartNode = serializedObject.FindProperty("pathStartNode");
        SerializedProperty pathStartDirection = serializedObject.FindProperty("pathStartDirection");

        if (moves.boolValue)
        {
            moveSpeed.floatValue = EditorGUILayout.Slider("Movement Speed", moveSpeed.floatValue, 0.1f, 10f);
            protectiveNPC.pathTransform = EditorGUILayout.ObjectField("Path", protectiveNPC.pathTransform, typeof(Transform), true) as Transform;
            pathIsACircuit.boolValue = EditorGUILayout.Toggle("Path Is A Circuit", pathIsACircuit.boolValue);

            if (protectiveNPC.pathTransform != null)
                maxNode = protectiveNPC.pathTransform.GetComponent<NodePath>().nodes.Count - 1;
            else
                maxNode = 0;

            pathStartNode.intValue = EditorGUILayout.IntSlider("Start Node Index", pathStartNode.intValue, 0, maxNode);
            pathStartDirection.enumValueIndex = (int)(Direction)EditorGUILayout.EnumPopup("Start Direction", (Direction)pathStartDirection.enumValueIndex);
        }
        else
        {
            moveSpeed.floatValue = 0f;
            protectiveNPC.pathTransform = null;
            pathIsACircuit.boolValue = false;
            pathStartNode.intValue = 0;
            pathStartDirection.enumValueIndex = (int)Direction.forward;
        }
        #endregion

        #region Weapon

        SerializedProperty usesWeapon = serializedObject.FindProperty("usesWeapon");
        usesWeapon.boolValue = EditorGUILayout.Toggle("NPC Uses A Weapon", usesWeapon.boolValue);

        if (usesWeapon.boolValue)
            protectiveNPC.weapon = EditorGUILayout.ObjectField("Weapon", protectiveNPC.weapon, typeof(Weapon), true) as Weapon;
        else
            protectiveNPC.weapon = null;

        #endregion

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(protectiveNPC);
            EditorSceneManager.MarkSceneDirty(protectiveNPC.gameObject.scene);
        }
    }
}

[CustomEditor(typeof(HostileNPC))]
public class HostileNPCCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        HostileNPC hostileNPC = (HostileNPC)target;

        #region Can React To Player

        SerializedProperty canReactToPlayer = serializedObject.FindProperty("canReactToPlayer");
        canReactToPlayer.boolValue = EditorGUILayout.Toggle("NPC Can React To Player", canReactToPlayer.boolValue);

        SerializedProperty sightRange = serializedObject.FindProperty("sightRange");
        if (canReactToPlayer.boolValue)
        {
            sightRange.floatValue = EditorGUILayout.Slider("Sight Range", sightRange.floatValue, 0f, 50f);
        }
        else
            sightRange.floatValue = 0f;

        #endregion

        #region Can Interact With Player

        SerializedProperty canInteractWithPlayer = serializedObject.FindProperty("canInteractWithPlayer");
        canInteractWithPlayer.boolValue = EditorGUILayout.Toggle("NPC Can Interact With Player", canInteractWithPlayer.boolValue);

        SerializedProperty interactRange = serializedObject.FindProperty("interactRange");
        if (canInteractWithPlayer.boolValue)
        {
            float maxRange;
            if (!canReactToPlayer.boolValue || sightRange.floatValue == 0f)
                maxRange = 10f;
            else
                maxRange = sightRange.floatValue;

            interactRange.floatValue = EditorGUILayout.Slider("Interact Range", interactRange.floatValue, 0f, maxRange);
        }
        else
            interactRange.floatValue = 0f;

        #endregion

        #region Moves

        int maxNode;

        SerializedProperty moves = serializedObject.FindProperty("moves");
        moves.boolValue = EditorGUILayout.Toggle("NPC Moves", moves.boolValue);

        SerializedProperty moveSpeed = serializedObject.FindProperty("moveSpeed");
        SerializedProperty pathIsACircuit = serializedObject.FindProperty("pathIsACircuit");
        SerializedProperty pathStartNode = serializedObject.FindProperty("pathStartNode");
        SerializedProperty pathStartDirection = serializedObject.FindProperty("pathStartDirection");

        if (moves.boolValue)
        {
            moveSpeed.floatValue = EditorGUILayout.Slider("Movement Speed", moveSpeed.floatValue, 0.1f, 10f);
            hostileNPC.pathTransform = EditorGUILayout.ObjectField("Path", hostileNPC.pathTransform, typeof(Transform), true) as Transform;
            pathIsACircuit.boolValue = EditorGUILayout.Toggle("Path Is A Circuit", pathIsACircuit.boolValue);

            if (hostileNPC.pathTransform != null)
                maxNode = hostileNPC.pathTransform.GetComponent<NodePath>().nodes.Count - 1;
            else
                maxNode = 0;

            pathStartNode.intValue = EditorGUILayout.IntSlider("Start Node Index", pathStartNode.intValue, 0, maxNode);
            pathStartDirection.enumValueIndex = (int)(Direction)EditorGUILayout.EnumPopup("Start Direction", (Direction)pathStartDirection.enumValueIndex);
        }
        else
        {
            moveSpeed.floatValue = 0f;
            hostileNPC.pathTransform = null;
            pathIsACircuit.boolValue = false;
            pathStartNode.intValue = 0;
            pathStartDirection.enumValueIndex = (int)Direction.forward;
        }
        #endregion

        #region Weapon

        SerializedProperty usesWeapon = serializedObject.FindProperty("usesWeapon");
        usesWeapon.boolValue = EditorGUILayout.Toggle("NPC Uses A Weapon", usesWeapon.boolValue);

        if (usesWeapon.boolValue)
            hostileNPC.weapon = EditorGUILayout.ObjectField("Weapon", hostileNPC.weapon, typeof(Weapon), true) as Weapon;
        else
            hostileNPC.weapon = null;

        #endregion

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(hostileNPC);
            EditorSceneManager.MarkSceneDirty(hostileNPC.gameObject.scene);
        }
    }
}



