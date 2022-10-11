using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BossStationaryState : MonoBehaviour, IState
{
    BossStateMachine _bossStateMachine;

    float timeUntilChangeState;
    bool usedLasers = false;

    public BossStationaryState(BossStateMachine bossStateMachine)
    {
        _bossStateMachine = bossStateMachine;
    }

    public void Enter()
    {
        Debug.Log("STATE CHANGE - Stationary");
        timeUntilChangeState = 2f;
    }

    public void Exit()
    {
        
    }

    public void FixedTick()
    {
        
    }

    public void Tick()
    {
        timeUntilChangeState -= Time.deltaTime;

        if (timeUntilChangeState <= 0f)
        {
            if (usedLasers == true)
            {
                usedLasers = false;
                _bossStateMachine.ChangeState(_bossStateMachine.Bullets);
            }
            else
            {
                usedLasers = true;
                _bossStateMachine.ChangeState(_bossStateMachine.Laser);
            }
        }
    }
}