using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] HealthBar _healthBar;

    Health _tankHealth;

    private void Awake()
    {
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
