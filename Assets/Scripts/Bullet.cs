using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] protected float TravelSpeed = .4f;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        Health playerHealth = collision.gameObject.GetComponent<Health>();

        if(player != null && playerHealth != null)
        {
            playerHealth.TakeDamage(1);
        }

        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        Vector3 moveOffset = transform.forward * TravelSpeed;
        _rb.MovePosition(_rb.position + moveOffset);
    }
}
