using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    [SerializeField] float _acceleration = 0.005f;

    protected override void Impact(Collision otherCollision)
    {
        if(otherCollision.gameObject.tag != "Player")
        {
            Debug.Log(otherCollision.gameObject.name + " has been hit");
            ImpactFeedback();
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        TravelSpeed += _acceleration;
        Move();
    }
}
