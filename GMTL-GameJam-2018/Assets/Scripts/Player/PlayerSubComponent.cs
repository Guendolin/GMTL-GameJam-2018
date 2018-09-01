using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSubComponent {

    protected PlayerController _playerController;

    public virtual void Init(PlayerController playerController)
    {
        _playerController = playerController;
    }
}
