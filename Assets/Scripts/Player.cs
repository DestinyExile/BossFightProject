using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{
    [SerializeField] Text treasureCountText;

    [SerializeField] int _maxHealth = 3;
    int _currentHealth;
    public int CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            if(value > _maxHealth)
            {
                value = _maxHealth;
            }

            _currentHealth = value;
        }
    }

    TankController _tankController;
    Inventory inventory;

    private void Awake()
    {
        _tankController = GetComponent<TankController>();
        inventory = GetComponent<Inventory>();
    }

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        Debug.Log("Player's health: " + _currentHealth);
    }

    public void DecreaseHealth(int amount)
    {
        _currentHealth -= amount;
        Debug.Log("Player's health: " + _currentHealth);
        if(_currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        gameObject.SetActive(false);
        //play particles
        //play sounds
    }

    public void IncreaseTreasure()
    {
        inventory.treasureCount++;
        treasureCountText.text = "Treasure Count: " + inventory.treasureCount;
    }
}
