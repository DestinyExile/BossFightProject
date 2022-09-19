using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] int _damageAmount = 1;
    [SerializeField] ParticleSystem _impactParticles;
    [SerializeField] AudioClip _impactSound;

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
            ImpactFeedback();
        }
    }

    private void ImpactFeedback()
    {
        //particles
        if(_impactParticles != null)
        {
            _impactParticles.Play();
        }
        //audio. TODO - consider Object Pooling for performance
        if(_impactSound != null)
        {
            AudioHelper.PlayClip2D(_impactSound, 1f);
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
