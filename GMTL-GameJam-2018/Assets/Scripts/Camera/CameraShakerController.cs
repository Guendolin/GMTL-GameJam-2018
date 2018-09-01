using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakerController : ShakeController {

    public static CameraShakerController Instance;

    public static void CameraShake(float duration = -1, float shakeCycleTime = -1, float shakeDistance = -1)
    {
        if(Instance != null)
        {
            Instance.Shake(duration, shakeCycleTime, shakeDistance);
        }
    }

    // Use this for initialization
    void Start () {
		if(Instance == null)
        {
            Instance = this;
        }
	}
}
