using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserState : MonoBehaviour, IState
{
    BossStateMachine _bossStateMachine;
    BossLaserAttack _laserAttack;
    Health _health;
    Rigidbody _rb;
    int attackTimes;

    Vector3 pos;

    public BossLaserState(BossStateMachine bossStateMachine, Rigidbody rb)
    {
        _bossStateMachine = bossStateMachine;
        _rb = rb;
    }

    public void Enter()
    {
        Debug.Log("STATE CHANGE - Laser");
        _laserAttack = _rb.gameObject.GetComponent<BossLaserAttack>();
        _health = _rb.gameObject.GetComponent<Health>();
        if (_health.CurrentHealth / _health.MaxHealth >= 0.75f)
        {
            attackTimes = 1;
            _laserAttack.ShootLaser(attackTimes);
        }
        else if (_health.CurrentHealth / _health.MaxHealth >= 0.5f && _health.CurrentHealth / _health.MaxHealth < 0.75f)
        {
            attackTimes = 2;
            _laserAttack.ShootLaser(attackTimes);
        }
        else if (_health.CurrentHealth / _health.MaxHealth >= 0.25f && _health.CurrentHealth / _health.MaxHealth < 0.5f)
        {
            attackTimes = 3;
            _laserAttack.ShootLaser(attackTimes);
        }
        else if (_health.CurrentHealth / _health.MaxHealth < 0.25f)
        {
            attackTimes = 4;
            _laserAttack.ShootLaser(attackTimes);
        }
    }

    public void Exit()
    {
        _laserAttack.stopAttack();
    }

    public void FixedTick()
    {
        Quaternion rotGoal = Quaternion.LookRotation(_laserAttack.UpdateAim() - _rb.transform.position);
        _rb.rotation = Quaternion.Slerp(_rb.rotation, rotGoal, _bossStateMachine.RotateSpeed);
    }

    public void Tick()
    {
        if(_laserAttack.checkAttackFinished())
        {
            _bossStateMachine.ChangeState(_bossStateMachine.MoveHorizontal);
        }
    }
}
