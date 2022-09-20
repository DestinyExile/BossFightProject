using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BossMoveCircleState : MonoBehaviour, IState
{
    BossStateMachine _bossStateMachine;
    Rigidbody _rb;

    float timeUntilChangeState;
    Vector3 startingPoint = new Vector3(0f, 1.347735f, 14f);
    bool startCircle = false;

    public BossMoveCircleState(BossStateMachine bossStateMachine, Rigidbody rb)
    {
        _bossStateMachine = bossStateMachine;
        _rb = rb;
    }


    public void Enter()
    {
        Debug.Log("STATE CHANGE - MoveCircle");
        timeUntilChangeState = 8.5f;
        startCircle = false;
    }

    public void Exit()
    {
        
    }

    public void FixedTick()
    {
        if(startCircle)
        {
            CircleMovement();
        }
        else if(_rb.position.z >= startingPoint.z)
        {
            startCircle = true;
        }
        else
        {
            MoveToStart();
        }
    }

    public void Tick()
    {
        timeUntilChangeState -= Time.deltaTime;

        if (timeUntilChangeState <= 0f)
        {
            _bossStateMachine.ChangeState(_bossStateMachine.Stationary);
        }
    }

    private void MoveToStart()
    {
        Vector3 direction = (startingPoint - _rb.position).normalized;
        Quaternion rotGoal = Quaternion.LookRotation(direction);
        _rb.rotation = Quaternion.Slerp(_rb.rotation, rotGoal, _bossStateMachine.RotateSpeed);
        _rb.Sleep();
        Vector3 moveOffset = _bossStateMachine.transform.forward * _bossStateMachine.MoveSpeed;
        _rb.MovePosition(_rb.position + moveOffset);
    }

    private void CircleMovement()
    {
        _rb.transform.RotateAround(new Vector3(0, _rb.position.y, 0), Vector3.up, 75 * Time.deltaTime);
    }
}