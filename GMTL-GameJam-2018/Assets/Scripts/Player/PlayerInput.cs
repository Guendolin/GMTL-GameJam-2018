using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class InputNames
{
    public const string VERTICAL_MOUSE = "Vertical-Mouse";
    public const string HORIZONTAL_MOUSE = "Horizontal-Mouse";

    public const string SWING_MOUSE = "Swing-Mouse";

    public const string VERTICAL_JOYSTICK = "Vertical-Joystick";
    public const string HORIZONTAL_JOYSTICK = "Horizontal-Joystick";

    public const string AIM_VERTICAL_JOYSTICK = "Aim-Vertical-Joystick";
    public const string AIM_HORIZONTAL_JOYSTICK = "Aim-Horizontal-Joystick";

    public const string SWING_JOYSTICK = "Swing-Joystick";
}

[System.Serializable]
public class PlayerInput : PlayerSubComponent {

    private enum ControllerType { None, Mouse, Joystick}
    private ControllerType _currentControllerType = ControllerType.Mouse;

    private float _prevJoystickSwingAxis = 0f;
    private float _currJoystickSwingAxis = 0f;

    private bool _joystickSwingDown = false; 
    private bool _joystickReset = true;

    public override void Init(PlayerController playerController)
    {
        base.Init(playerController);

        if(Input.GetJoystickNames().Length > 0)
        {
            Debug.Log("Select Joystick");
            _currentControllerType = ControllerType.Joystick;
        }
    }

    public void Tick()
    {
        if (IsJoyPadActive() && !IsMouseActive() && _currentControllerType != ControllerType.Joystick)
        {
            Debug.Log("Select Joystick");
            _currentControllerType = ControllerType.Joystick;
            
        }
        else if (IsMouseActive() && !IsJoyPadActive() && _currentControllerType != ControllerType.Mouse)
        {
            Debug.Log("Select mouse + keyboard");
            _currentControllerType = ControllerType.Mouse;
        }

        _prevJoystickSwingAxis = _currJoystickSwingAxis;
        _currJoystickSwingAxis = Input.GetAxisRaw(InputNames.SWING_JOYSTICK);
        _joystickSwingDown = false;

        if (_joystickReset && _currJoystickSwingAxis > _prevJoystickSwingAxis)
        {
            _joystickSwingDown = true;
            _joystickReset = false;
        }
        else if (!_joystickReset && _currJoystickSwingAxis < _prevJoystickSwingAxis)
        {
            _joystickReset = true;
        }
    }


    public Vector2 GetMovement()
    {
        if (_currentControllerType == ControllerType.Mouse)
        {
            return new Vector2(Input.GetAxisRaw(InputNames.HORIZONTAL_MOUSE), Input.GetAxisRaw(InputNames.VERTICAL_MOUSE));
        }
        else if (_currentControllerType == ControllerType.Joystick)
        {
            return new Vector2(Input.GetAxisRaw(InputNames.HORIZONTAL_JOYSTICK), Input.GetAxisRaw(InputNames.VERTICAL_JOYSTICK));
        }

        return Vector2.zero;
    }

    public Vector2? GetAim()
    {
        if (_currentControllerType == ControllerType.Mouse)
        {
            Vector2 mouseScreenPos = Input.mousePosition;
            Vector2 playerScreenPos = MainCameraController.Instance.mainCamera.WorldToScreenPoint(_playerController.transform.position);
            return (mouseScreenPos - playerScreenPos).normalized;
        }
        else if (_currentControllerType == ControllerType.Joystick)
        {
            Vector2 t = new Vector2(Input.GetAxisRaw(InputNames.AIM_HORIZONTAL_JOYSTICK), Input.GetAxisRaw(InputNames.AIM_VERTICAL_JOYSTICK));
            //Debug.LogFormat(">{0}-{1}", Input.GetAxisRaw(InputNames.AIM_HORIZONTAL_JOYSTICK), Input.GetAxisRaw(InputNames.AIM_VERTICAL_JOYSTICK));
            //Debug.LogFormat("={0}-{1}", t.x, t.y);

            return t.normalized;
        }

        return null;
    }

    public bool GetSwing()
    {
        if (_currentControllerType == ControllerType.Mouse)
        {
            return Input.GetButtonDown(InputNames.SWING_MOUSE);
        }
        else if (_currentControllerType == ControllerType.Joystick)
        {
            return Input.GetButtonDown(InputNames.SWING_JOYSTICK) || _joystickSwingDown;
        }

        return false;
    }

    private bool IsMouseActive()
    {
        return Input.GetAxisRaw(InputNames.HORIZONTAL_MOUSE) > 0f || Input.GetAxisRaw(InputNames.VERTICAL_MOUSE) > 0f || 
                Input.GetButtonDown(InputNames.SWING_MOUSE);
    }

    private bool IsJoyPadActive()
    {
        return Input.GetAxisRaw(InputNames.HORIZONTAL_JOYSTICK) > 0f || Input.GetAxisRaw(InputNames.VERTICAL_JOYSTICK) > 0f ||
                Input.GetAxisRaw(InputNames.AIM_HORIZONTAL_JOYSTICK) > 0f || Input.GetAxisRaw(InputNames.AIM_VERTICAL_JOYSTICK) > 0f ||
                Input.GetButtonDown(InputNames.SWING_JOYSTICK) || _joystickSwingDown;
    }

}
