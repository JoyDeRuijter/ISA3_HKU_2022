// Written by Joy de Ruijter
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    #region Variables

    public new Camera camera;
    public Weapon weapon;
    public Unit owner;

    #endregion

    public virtual void Awake()
    {
        camera = Camera.main;
    }

    public virtual void Update()
    {
        
    }

    public virtual void Attack(Unit attacker, WeaponHolster _holster)
    {
        Debug.Log("Attacked " + "with " + weapon.name + " and does " + weapon.damage + " damage");
        //bool hit =  false;

        //if (hit)
        //{ 
        //    if (unit.currentHealth - weapon.damage <= 0)
        //        unit.Die();
        //    else
        //        unit.currentHealth -= weapon.damage;        
        //}
    }

    public virtual void SecundairyBehaviour(){}

    public virtual void TertiaryBehaviour(){}
}
