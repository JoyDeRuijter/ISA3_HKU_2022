// Written by Joy de Ruijter
using UnityEngine;

public class Unit : MonoBehaviour
{
    #region Variables

    public int maxHealth = 100;
    public int currentHealth = 100;

    #endregion

    public virtual void Die()
    {
        currentHealth = 0;
        Debug.Log(gameObject.name + " has died!");
    }

    public virtual void TakeDamage(int damage)
    {
        Debug.Log(gameObject.name + " Took " + damage + " damage");
        if (currentHealth - damage > 0)
            currentHealth -= damage;
        else
            Die();
    }
}
