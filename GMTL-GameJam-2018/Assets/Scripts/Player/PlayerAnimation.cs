using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnimation : PlayerSubComponent
{
    public Animator playerAnimator;
    public Animator batAnimator;

    private int _playerRunBool;

    private int _batSwingRightTrigger;
    private int _batSwingLeftTrigger;
    
    public AudioSource runningSound;

    public override void Init(PlayerController playerController)
    {
        base.Init(playerController);

        _playerController = playerController;

        _playerRunBool = Animator.StringToHash("Run");

        _batSwingRightTrigger = Animator.StringToHash("SwingLeft");
        _batSwingLeftTrigger = Animator.StringToHash("SwingRight");
    }

    public void SetPlayerRun(bool run)
    {
        playerAnimator.SetBool(_playerRunBool, run);
        if(run == true)
        {
            runningSound.volume = 1f;
        }
        else
        {
            runningSound.volume = 0f;
        }
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

