using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerBat {

    private enum BatPosition { None, Left = -1, Right = 1}

    public Transform batPivot;
    public Transform batGraphics;

    public AnimationCurve batSwing;

    private BatPosition _currentBatPos = BatPosition.Left;
    private float _swingTime = -1;

    private const float ANGLE_LEFT = 75f;
    private const float ANGLE_RIGHT = -255;

    public void Init()
    {
        SetBatGrahpicsRotation(_currentBatPos == BatPosition.Left ? ANGLE_LEFT : ANGLE_RIGHT);
    }

	// Update is called once per frame
	public void Tick (Vector2? aim = null)
    {
        if (aim.HasValue)
        {
            float angle = Mathf.Atan2(aim.Value.y, aim.Value.x) * Mathf.Rad2Deg;
            Quaternion batRotation = Quaternion.Euler(0, 0, angle);
            batPivot.localRotation = batRotation;
        }

        if(_swingTime >= 0)
        {
            _swingTime += Time.deltaTime;

            float from = _currentBatPos == BatPosition.Left ? ANGLE_LEFT : ANGLE_RIGHT;
            float to = _currentBatPos == BatPosition.Left ? ANGLE_RIGHT : ANGLE_LEFT;

            float current = Mathf.LerpUnclamped(from, to, batSwing.Evaluate(_swingTime));

            SetBatGrahpicsRotation(current);

            if (_swingTime >= batSwing.Duration())
            {
                _swingTime = -1f;
                _currentBatPos = (BatPosition)(((int)_currentBatPos) * -1);
            }
        }	
	}

    private void SetBatGrahpicsRotation(float angle)
    {
        batGraphics.localRotation = Quaternion.Euler(0, 0, angle); ;
    }

    public void SwingBat()
    {
        if(_swingTime < 0)
        {
            _swingTime = 0;
        }
    }
}
