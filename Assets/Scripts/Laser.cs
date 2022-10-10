using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] int _damageAmount = 2;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        Health playerHealth = other.gameObject.GetComponent<Health>();
        if (player != null && playerHealth != null)
        {
            playerHealth.TakeDamage(_damageAmount);
        }
    }
}
