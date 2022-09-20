using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] int _damageAmount = 1;

    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        Health playerHealth = other.gameObject.GetComponent<Health>();
        if (player != null && playerHealth != null)
        {
            playerHealth.TakeDamage(_damageAmount);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        
    }
}
