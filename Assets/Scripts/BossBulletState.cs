using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletState : MonoBehaviour, IState
{
    private BossStateMachine _bossStateMachine;
    private BossBulletAttack _bulletAttack;
    private Rigidbody _rb;
    private Player _player;

    public BossBulletState(BossStateMachine bossStateMachine, Rigidbody rb, Player player)
    {
        _bossStateMachine = bossStateMachine;
        _rb = rb;
        _player = player;
    }

    public void Enter()
    {
        _bulletAttack = _rb.gameObject.GetComponent<BossBulletAttack>();
        Debug.Log("STATE CHANGE - Bullets");
        _bulletAttack.BulletAttack();

    }

    public void Exit()
    {
        _bulletAttack.stopAttack();
    }

    public void FixedTick()
    {
        Quaternion rotGoal = Quaternion.LookRotation(_player.transform.position - _rb.transform.position);
        _rb.rotation = Quaternion.Slerp(_rb.rotation, rotGoal, _bossStateMachine.RotateSpeed);
    }

    public void Tick()
    {
        if (_bulletAttack.checkAttackFinished())
        {
            _bossStateMachine.ChangeState(_bossStateMachine.MoveHorizontal);
        }
    }
}
