// Written by Joy de Ruijter
using UnityEngine;

public class RangedWeaponBehaviour : WeaponBehaviour
{
    #region Variables

    private RangedWeapon rangedWeapon;

    #endregion

    private void Start()
    {
        rangedWeapon = weapon as RangedWeapon; 
    }

    public override void Attack(Unit _attacker, WeaponHolster _holster)
    {
        base.Attack(_attacker, _holster);

        if (_attacker.gameObject.GetComponent<Player>() != null)
        {
            Debug.Log("Player is actually shooting");
            PlayerShoot(_holster);
        }
        else if (_attacker.gameObject.GetComponent<NPC>() != null)
        { 
            NPCShoot();
        }

    }

    private void PlayerShoot(WeaponHolster _holster)
    {
        //rangedWeapon.muzzleFlash.Play();
        //rangedWeapon.shotSound.Play();
        rangedWeapon.currentAmmo--;

        RaycastHit _hit;

        if (Physics.Raycast(transform.position, camera.transform.forward, out _hit, rangedWeapon.range))
        {
            Debug.Log("Hit: " + _hit.transform.gameObject.name);
            if (_hit.transform.gameObject.GetComponent<NPC>() != null)
            {
                Debug.Log("Hit an npc");
                NPC _npc = _hit.transform.gameObject.GetComponent<NPC>();
                _npc.TakeDamage(weapon.damage);
            }

            if(_hit.rigidbody != null)
                _hit.rigidbody.AddForce(-_hit.normal * rangedWeapon.impactForce);
        }

        //GameObject _impactGO = Instantiate(rangedWeapon.impactEffect, _hit.point, Quaternion.LookRotation(_hit.normal));
        //_impactGO.GetComponent<ParticleSystem>().Play();
        //Destroy(_impactGO, 2f);
    }

    private void NPCShoot()
    {
        Debug.Log("Attacked " + "with " + weapon.name + " and does " + weapon.damage + " damage");

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, rangedWeapon.range))
        {
            if (hit.transform.GetComponent<Player>() != null)
            {
                Player player = hit.transform.GetComponent<Player>();
                player.TakeDamage(weapon.damage);
                GameObject bloodGO = Instantiate(rangedWeapon.bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                bloodGO.GetComponent<ParticleSystem>().Play();
                Destroy(bloodGO, 2f);
            }
            else if (hit.transform.GetComponent<NPC>() != null)
            {
                NPC npc = hit.transform.GetComponent<NPC>();
                npc.TakeDamage(weapon.damage);
                GameObject bloodGO = Instantiate(rangedWeapon.bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                bloodGO.GetComponent<ParticleSystem>().Play();
                Destroy(bloodGO, 2f);
            }
            else
            {
                GameObject impactGO = Instantiate(rangedWeapon.impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                impactGO.GetComponent<ParticleSystem>().Play();
                Destroy(impactGO, 2f);
            }
            hit.rigidbody.AddForce(-hit.normal * rangedWeapon.impactForce);
        }
    }
}
