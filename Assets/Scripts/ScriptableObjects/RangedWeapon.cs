// Written by Joy de Ruijter
using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Weapons/RangedWeapon")]
public class RangedWeapon : Weapon
{
    #region Variables

    [Header("Weapon Properties")]
    public float reloadTime;
    public float maxAmmo;
    public float fireRate;
    public float impactForce;

    [Header("Weapon Visualisations")]
    [Space(10)]
    public Vector3 muzzleFlashLocation;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject bloodEffect;
    public Animator weaponAnimator;
    public Sprite icon;

    [Header("Weapon Sounds")]
    [Space(10)]
    public AudioSource shotSound;
    public AudioSource reloadSound;

    [HideInInspector] public float currentAmmo;
    [HideInInspector] public bool isReloading;

    #endregion

    private void Awake()
    {
        currentAmmo = maxAmmo;
    }
}
