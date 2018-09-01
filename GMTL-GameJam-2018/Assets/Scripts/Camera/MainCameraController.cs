using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class MainCameraController : MonoBehaviour {

    public static MainCameraController Instance;

    public Camera mainCamera { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        mainCamera = GetComponent<Camera>();
    }
}
