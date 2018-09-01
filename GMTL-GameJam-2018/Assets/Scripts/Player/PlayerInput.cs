using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class InputNames
{
    public const string VERTICAL = "Vertical";
    public const string HORIZONTAL = "Horizontal";

    public const string SWING = "Swing";
}


[System.Serializable]
public class PlayerInput : PlayerSubComponent {

    public Vector2 GetMovement()
    {
        return new Vector2(Input.GetAxisRaw(InputNames.HORIZONTAL), Input.GetAxisRaw(InputNames.VERTICAL));
    }

    public Vector2? GetAim()
    {
        Vector2 mouseScreenPos = Input.mousePosition;
        Vector2 playerScreenPos = MainCameraController.Instance.mainCamera.WorldToScreenPoint(_playerController.transform.position);
        return (mouseScreenPos - playerScreenPos).normalized;
    }

    public bool GetSwing()
    {
        return Input.GetButtonDown(InputNames.SWING);
    }

}
