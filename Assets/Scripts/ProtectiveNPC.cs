// Written by Joy de Ruijter
using UnityEngine;

public class ProtectiveNPC : NPC
{
    #region Variables

    [HideInInspector] public WeaponHolster holster;
    [HideInInspector] public Weapon weapon;

    public bool usesWeapon;

    #endregion

    ProtectiveNPC(NPCType _npcType, string _name)
    : base(_npcType, _name) { }

    private void Awake()
    {
        holster.capacity = 1;
        holster.weapons = new Weapon[holster.capacity];
        holster.weapons[0] = weapon;
    }
}
