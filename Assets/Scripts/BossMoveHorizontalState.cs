using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BossMoveHorizontalState : MonoBehaviour, IState
{
    BossStateMachine _bossStateMachine;
    Rigidbody _rb;

    Vector3 leftWall;
    Vector3 rightWall;
    Vector3 target;
    Vector3 direction;
    float timeUntilChangeState;

    public BossMoveHorizontalState(BossStateMachine bossStateMachine, Rigidbody rb)
    {
        _bossStateMachine = bossStateMachine;
        _rb = rb;
    }

    public void Enter()
    {
        Debug.Log("STATE CHANGE - MoveHorizontal");
        leftWall = new Vector3(-27f, _rb.position.y, _rb.position.z);
        rightWall = new Vector3(27f, _rb.position.y, _rb.position.z);
        target = leftWall;
        direction = Vector3.left;
        timeUntilChangeState = 8f;
    }

    public void Exit()
    {

    }

    public void FixedTick()
    {
        Quaternion rotGoal = Quaternion.LookRotation(direction);
        _rb.rotation = Quaternion.Slerp(_rb.rotation, rotGoal, _bossStateMachine.RotateSpeed);

        _rb.Sleep();

        Vector3 moveOffset = _bossStateMachine.transform.forward * _bossStateMachine.MoveSpeed;
        _rb.MovePosition(_rb.position + moveOffset);
    }

    public void Tick()
    {
        timeUntilChangeState -= Time.deltaTime;

        if(timeUntilChangeState <= 0f)
        {
            _bossStateMachine.ChangeState(_bossStateMachine.MoveCircle);
        }
        
        if(target == leftWall && _rb.position.x <= leftWall.x)
        {
            target = rightWall;
            direction = Vector3.right;
        }

        if(target == rightWall && _rb.position.x >= rightWall.x)
        {
            target = leftWall;
            direction = Vector3.left;
        }
    }
}