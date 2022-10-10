using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserAttack : MonoBehaviour
{
    [SerializeField] BossStateMachine _bossStateMachine;
    [SerializeField] GameObject LaserMarker;
    [SerializeField] GameObject Laser;
    [SerializeField] Rigidbody _rb;
    [SerializeField] Player _player;

    private int _numOfAttacks;
    private bool attackFinished = false;
    private Vector3 pos;

    public void ShootLaser(int numOfAttacks)
    {
        _numOfAttacks = numOfAttacks;
        attackFinished = false;
        StartCoroutine(FireLaser());  
    }

    private IEnumerator FireLaser()
    {
        for(int i = 0; i < _numOfAttacks; i++)
        {
            pos = _player.transform.position;
            float chargeTime = 2.0f;
            float laserDuration = 0.5f;
            LaserMarker.SetActive(true);

            while (chargeTime > 0)
            {
                chargeTime -= Time.deltaTime;
                yield return null;
            }

            LaserMarker.SetActive(false);
            Laser.SetActive(true);

            while (laserDuration > 0)
            {
                laserDuration -= Time.deltaTime;
                yield return null;
            }
            Laser.SetActive(false);
        }
        attackFinished = true;
    }

    public Vector3 UpdateAim()
    {
        return pos;
    }

    public bool checkAttackFinished()
    {
        return attackFinished;
    }

    public void stopAttack()
    {
        StopCoroutine(FireLaser());
    }
}
