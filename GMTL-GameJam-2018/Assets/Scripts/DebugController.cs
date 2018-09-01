using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour {

    private bool _slowDownActive = false;

    private const float NORMAL_TIME = 1f;
    private const float DEBUG_TIME = 0.2f;

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _slowDownActive = !_slowDownActive;
            Time.timeScale = _slowDownActive ? DEBUG_TIME : NORMAL_TIME;
        }
	}
}
