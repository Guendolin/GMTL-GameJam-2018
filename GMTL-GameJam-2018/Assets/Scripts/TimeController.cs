using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

    // Singleton
    public static TimeController Instance;

    public static void SlowdownTime(float scale = 0.01f, float duration = 0.2f)
    {
        if(Instance != null)
        {
            Instance._SlowdownTime(scale, duration);
        }
    }

    public AnimationCurve slowDownCurve;

    private float _currentScale = 1f;
    private float _currentDuration = 0.4f;
    private float _slowDownTime = -1;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update ()
    {
		if(_slowDownTime >= 0f)
        {
            _slowDownTime += Time.unscaledDeltaTime / _currentDuration;

            SetTimeScale(_slowDownTime);

            if (_slowDownTime >= 1f)
            {
                _slowDownTime = -1f;
                Time.timeScale = 1f;
            }
        }
	}

    public void _SlowdownTime(float scale = 0.1f, float duration = 0.1f)
    {
        _currentScale = scale;
        _currentDuration = duration;
        _slowDownTime = 0f;
        SetTimeScale(0f);
    }

    private void SetTimeScale(float t)
    {
        Time.timeScale = Mathf.LerpUnclamped(1f, _currentScale, slowDownCurve.Evaluate(t));
    }
}
