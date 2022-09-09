﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerStates
{
    [Header("Settings")] 
    [SerializeField] private float speed = 10f;

    public float Speed { get; set; }
    public float InitialSpeed => speed;
    
    private float _horizontalMovement;
    private float _movement;

    private int _idleAnimatorParameter = Animator.StringToHash("Idle");
    private int _runAnimatorParameter = Animator.StringToHash("Run");

    protected override void InitState()
    {
        base.InitState();
        Speed = speed;
    }

    public override void ExecuteState()
    {
        MovePlayer();
    }

    /// <summary>
    /// Moves our Player
    /// </summary>
    private void MovePlayer()
    {
        if (Mathf.Abs(_horizontalMovement) > 0.1f)
        {
            _movement = _horizontalMovement;
        }
        else
        {
            _movement = 0f;
        }

        float moveSpeed = _movement * Speed;
        moveSpeed = EvaluateFriction(moveSpeed);
        
        _playerController.SetHorizontalForce(moveSpeed);
    }

    /// <summary>
    /// Initialize our internal movement direction
    /// </summary>
    protected override void GetInput()
    {
        _horizontalMovement = _horizontalInput;
    }

    public override void SetAnimation()
    {
        _animator.SetBool(_idleAnimatorParameter, _horizontalMovement == 0 && _playerController.Conditions.IsCollidingBelow);
        _animator.SetBool(_runAnimatorParameter, Mathf.Abs(_horizontalInput) > 0.1f && _playerController.Conditions.IsCollidingBelow);
    }

    private float EvaluateFriction(float moveSpeed)
    {
        if (_playerController.Friction > 0)
        {
            moveSpeed = Mathf.Lerp(_playerController.Force.x, moveSpeed, Time.deltaTime * 10f * _playerController.Friction);
        }

        return moveSpeed;
    }
}
