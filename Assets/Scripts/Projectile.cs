using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected abstract void Impact(Collision otherCollision);

    [Header("Base Settings")]
    [SerializeField] protected float TravelSpeed = .001f;
    [SerializeField] protected Rigidbody RB;
    [SerializeField] protected ParticleSystem _impactParticles;
    [SerializeField] protected AudioClip _impactSound;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Projectile collision!");
        Impact(collision);
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        Vector3 moveOffset = transform.forward * TravelSpeed;
        RB.MovePosition(RB.position + moveOffset);
    }

    protected void ImpactFeedback()
    {
        //particles
        if (_impactParticles != null)
        {
            Instantiate(_impactParticles, this.gameObject.transform.TransformPoint(Vector3.zero), Quaternion.identity);
        }
        //audio. TODO - consider Object Pooling for performance
        if (_impactSound != null)
        {
            AudioHelper.PlayClip2D(_impactSound, 1f);
        }
    }
}
