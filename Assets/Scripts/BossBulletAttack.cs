using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletAttack : MonoBehaviour
{
    [SerializeField] Rigidbody _bossRB;
    [SerializeField] GameObject _bullet;
    [SerializeField] int numOfBullets = 20;
    [SerializeField] float attackDelay = 0.25f;
    [SerializeField] AudioClip _shootSound;

    private bool attackFinished = false;

    public void BulletAttack()
    {
        attackFinished = false;
        StartCoroutine(ShootBullets());
    }

    IEnumerator ShootBullets()
    {
        float delay = attackDelay;
        for(int i = 0; i < numOfBullets; i++)
        {
            delay = attackDelay;
            if (_bullet != null)
            {
                Instantiate(_bullet, this.gameObject.transform.TransformPoint(Vector3.forward * 3f),
                    new Quaternion(this.gameObject.transform.rotation.x, this.gameObject.transform.rotation.y, this.gameObject.transform.rotation.z, this.gameObject.transform.rotation.w));
                if(_shootSound != null)
                {
                    AudioHelper.PlayClip2D(_shootSound, 1f);
                }
            }
            while(delay > 0)
            {
                delay -= Time.deltaTime;
                yield return null;
            }
        }
        attackFinished = true;
    }

    public bool checkAttackFinished()
    {
        return attackFinished;
    }

    public void stopAttack()
    {
        StopCoroutine(ShootBullets());
    }
}
