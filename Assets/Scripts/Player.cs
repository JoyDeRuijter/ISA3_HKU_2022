// Written by Joy de Ruijter
using UnityEngine;

public class Player : Unit
{
    #region Variables

    
    [SerializeField] private GameObject holsterObject;
    private WeaponHolster holster;
    private WeaponBehaviour currentWeaponBehaviour;
    [SerializeField] private Unit testUnit;

    #endregion

    private void Start()
    {
        holster = holsterObject.GetComponent<WeaponHolster>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !holster.isHolstered)
            holster.currentBehaviour.Attack(this, holster);
    }
}
