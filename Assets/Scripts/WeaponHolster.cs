// Written by Joy de Ruijter
using UnityEngine;

public class WeaponHolster : MonoBehaviour
{
    #region Variables

    public int capacity = 2;
    public Weapon[] weapons;

    public bool isHolstered { get; private set; }

    [HideInInspector] public int selectedWeapon = 0;

    private Weapon currentWeapon;
    [HideInInspector] public GameObject[] goWeapons;

    private WeaponBehaviour[] behaviours = new WeaponBehaviour[2]; // 0 is melee, 1 is ranged
    [HideInInspector] public WeaponBehaviour currentBehaviour;

    #endregion

    private void Awake()
    {
        InitializeWeaponBehaviours();
    }

    private void Start()
    {
        if (weapons.Length == 0)
            return;

        currentWeapon = weapons[selectedWeapon];

        LoadWeaponBehaviour();
        
        goWeapons = new GameObject[weapons.Length];
        InstantiateWeapons();
        SelectWeapon();
    }

    private void Update()
    {
        Mathf.Clamp(weapons.Length, 0, capacity);

        if (capacity <= 1)
            return;

        if (!isHolstered)
        {
            int previousSelectedWeapon = selectedWeapon;
            HandleInput();

            if (previousSelectedWeapon != selectedWeapon)
                SelectWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Q))
            HolsterWeapon(selectedWeapon);
    }

    private void InitializeWeaponBehaviours()
    {
        behaviours[0] = gameObject.AddComponent<MeleeWeaponBehaviour>();
        behaviours[1] = gameObject.AddComponent<RangedWeaponBehaviour>();
    }

    private void InstantiateWeapons()
    {
        for(int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].weaponPrefab == null)
            {
                Debug.Log(weapons[i].name + " has no weaponprefab attached");
                return;
            }
            GameObject instantiateObject = Instantiate(weapons[i].weaponPrefab, transform);
            instantiateObject.transform.localPosition = weapons[i].spawnPosition;
            instantiateObject.transform.localRotation = weapons[i].spawnRotation;
            goWeapons[i] = instantiateObject;
        }
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Weapon weapon in weapons)
        {
            if (i == selectedWeapon)
            {
                currentWeapon = weapon;
                goWeapons[i].SetActive(true);
            }
            else
                goWeapons[i].SetActive(false);

            i++;
        }
        LoadWeaponBehaviour();
    }

    private void LoadWeaponBehaviour()
    {
        if (currentWeapon.GetType() == typeof(MeleeWeapon))
        {
            currentBehaviour = behaviours[0];
            currentBehaviour.weapon = currentWeapon;
        }
        else if (currentWeapon.GetType() == typeof(RangedWeapon))
        {
            currentBehaviour = behaviours[1];
            currentBehaviour.weapon = currentWeapon;
        }
    }

    private void HandleInput()
    {
        // Scrollwheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // Up
        {
            if (selectedWeapon >= weapons.Length - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // Down
        {
            if (selectedWeapon <= 0)
                selectedWeapon = weapons.Length - 1;
            else
                selectedWeapon--;
        }

        // Numberkeys
        if (Input.GetKeyDown(KeyCode.Alpha1))
            selectedWeapon = 0;

        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Length >= 2)
            selectedWeapon = 1;

        if (Input.GetKeyDown(KeyCode.Alpha3) && weapons.Length >= 3)
            selectedWeapon = 2;

        if (Input.GetKeyDown(KeyCode.Alpha4) && weapons.Length >= 4)
            selectedWeapon = 3;
    }

    private void HolsterWeapon(int selectedWeapon)
    {
        if (isHolstered)
        {
            isHolstered = false;
            goWeapons[selectedWeapon].SetActive(true);
        }

        else if (!isHolstered)
        {
            isHolstered = true;
            goWeapons[selectedWeapon].SetActive(false);
        }
    }
}
