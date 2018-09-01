using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnimation
{
    public Animator playerAnimator;

    PlayerController _playerController;

    private int _playerRunTrigger;

    public void Init(PlayerController playerController)
    {
        _playerController = playerController;

        _playerRunTrigger = Animator.StringToHash("Run");
    }

    public void SetPlayerRun(bool run)
    {
        playerAnimator.SetBool(_playerRunTrigger, run);
    }
}

