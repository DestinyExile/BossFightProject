using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{
    TankController _tankController;
    Health _tankHealth;
    [SerializeField] HealthBar _healthBar;

    private void Awake()
    {
        _tankController = GetComponent<TankController>();
        _tankHealth = GetComponent<Health>();
    }

    private void Start()
    {
        _healthBar.SetMaxHealth(_tankHealth.MaxHealth);
    }

    private void OnEnable()
    {
        _tankHealth.Damaged += TookDamage;
    }

    private void OnDisable()
    {
        _tankHealth.Damaged -= TookDamage;
    }

    void TookDamage(int damage)
    {
        _healthBar.SetHealth(_tankHealth.CurrentHealth);
    }
}
