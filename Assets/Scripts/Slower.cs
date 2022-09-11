using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slower : Enemy
{
    [SerializeField] float _speedAmount = .10f;
    [SerializeField] float duration = 3f;

    bool playerisSlow = false;

    TankController controller;

    protected override void PlayerImpact(Player player)
    {
        controller = player.GetComponent<TankController>();
        if (controller != null)
        {
            controller.MaxSpeed -= _speedAmount;
            playerisSlow = true;
        }
    }

    private void Update()
    {
        if(playerisSlow)
        {
            duration -= Time.deltaTime;

            if(duration <= 0f)
            {
                durationEnded();
            }
        }
    }

    private void durationEnded()
    {
        duration = 3f;
        controller.MaxSpeed += _speedAmount;
        playerisSlow = false;
    }
}
