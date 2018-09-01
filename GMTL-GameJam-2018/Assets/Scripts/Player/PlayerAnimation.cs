﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnimation
{
    public Animator playerAnimator;
    public Animator batAnimator;

    PlayerController _playerController;

    private int _playerRunBool;

    private int _batSwingRightTrigger;
    private int _batSwingLeftTrigger;

    public void Init(PlayerController playerController)
    {
        _playerController = playerController;

        _playerRunBool = Animator.StringToHash("Run");

        _batSwingRightTrigger = Animator.StringToHash("SwingLeft");
        _batSwingLeftTrigger = Animator.StringToHash("SwingRight");
    }

    public void SetPlayerRun(bool run)
    {
        playerAnimator.SetBool(_playerRunBool, run);
    }

    public void BatWobbleLeft()
    {
        batAnimator.SetTrigger(_batSwingLeftTrigger);
    }

    public void BatWobbleRight()
    {
        batAnimator.SetTrigger(_batSwingRightTrigger);
    }
}

