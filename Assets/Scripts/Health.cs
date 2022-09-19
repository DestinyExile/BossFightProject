using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] int _maxHealth = 5;

    protected int currentHealth;

    void Start()
    {
        currentHealth = _maxHealth;
    }

    public void TakeDamage(int amount)
    {
        Debug.Log(gameObject.name + " took " + amount + " damage.");
        currentHealth -= amount;
        Debug.Log(gameObject.name + "'s current health is " + currentHealth);
        if(currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        currentHealth = 0;
        Destroy(gameObject);
    }

    private void DamageFeedback()
    {

    }
}
