using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : StateMachine
{
    public BossStationaryState Stationary { get; private set; }
    public BossMoveHorizontalState MoveHorizontal { get; private set; }
    public BossMoveCircleState MoveCircle { get; private set; }
    public BossMoveBurrowState MoveBurrow { get; private set; }
    public BossLaserState Laser { get; private set; }
    public BossBulletState Bullets { get; private set; }

    [Header("Required References")]
    [SerializeField] Rigidbody _rb = null;
    [SerializeField] Player _player = null;

    [Header("Movement Settings")]
    [SerializeField] float _rotateSpeed = 6;
    public float RotateSpeed => _rotateSpeed;
    [SerializeField] float _moveSpeed = 4;
    public float MoveSpeed => _moveSpeed;

    private void Awake()
    {
        Stationary = new BossStationaryState(this);
        MoveHorizontal = new BossMoveHorizontalState(this, _rb);
        MoveCircle = new BossMoveCircleState(this, _rb);
        MoveBurrow = new BossMoveBurrowState(this, _rb);
        Laser = new BossLaserState(this, _rb);
        Bullets = new BossBulletState(this, _rb, _player);
    }

    private void Start()
    {
        ChangeState(Stationary);
    }
}
