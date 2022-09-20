using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BossMoveBurrowState : MonoBehaviour, IState
{
    [SerializeField] GameObject burrowMarker;

    BossStateMachine _bossStateMachine;
    Rigidbody _rb;

    float timeUntilChangeState;

    bool isBurrowed = false;

    public BossMoveBurrowState(BossStateMachine bossStateMachine, Rigidbody rb)
    {
        _bossStateMachine = bossStateMachine;
        _rb = rb;
    }


    public void Enter()
    {
        Debug.Log("STATE CHANGE - MoveBurrow");
        _rb.detectCollisions = false;
        _rb.gameObject.GetComponent<Collider>().enabled = false;
        _rb.useGravity = false;
        timeUntilChangeState = 6f;
    }

    public void Exit()
    {
        _rb.detectCollisions = true;
        _rb.gameObject.GetComponent<Collider>().enabled = true;
        _rb.useGravity = true;
    }

    public void FixedTick()
    {
        if(!isBurrowed)
        {
            Burrow();
            isBurrowed = true;
            CreateMarker();
        }
        else
        {

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

    private void Burrow()
    {
        Vector3 moveOffset = Vector3.down * _bossStateMachine.MoveSpeed;
        _rb.MovePosition(_rb.position + moveOffset);
    }

    private void CreateMarker()
    {
        float x = Random.Range(-25, 25);
        float z = Random.Range(-15, 15);

        Vector3 randomPoint = new Vector3(x, 0.01f, z);

        burrowMarker.SetActive(true);
    }
}