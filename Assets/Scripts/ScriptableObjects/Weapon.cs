// Written by Joy de Ruijter
using UnityEngine;

public class Weapon : ScriptableObject
{
    #region Variables

    [Header("Weapon name")]
    public new string name = "Insert weapon name";

    [Header("Weapon prefab")]
    [Space(10)]
    public GameObject weaponPrefab;

    [Header("Weapon Properties")]
    public int damage;
    public float range;

    [Header("Spawn Data")]
    [Space(10)]
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;

    #endregion
}
