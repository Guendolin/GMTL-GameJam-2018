using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class InputNames
{
    public const string VERTICAL = "Vertical";
    public const string HORIZONTAL = "Horizontal";
}


[System.Serializable]
public class PlayerInput {
    private PlayerController _playerController;

    public void Init(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public Vector2 GetMovement()
    {
        return new Vector2(Input.GetAxisRaw(InputNames.HORIZONTAL), Input.GetAxisRaw(InputNames.VERTICAL));
    }

    public Vector2 GetAim(out bool aimPressent)
    {
        Vector2 mouseScreenPos = Input.mousePosition;
        Vector2 playerScreenPos = _playerController.mainCamera.WorldToScreenPoint(_playerController.transform.position);

        aimPressent = true;

        return (mouseScreenPos - playerScreenPos).normalized;
    }

}
