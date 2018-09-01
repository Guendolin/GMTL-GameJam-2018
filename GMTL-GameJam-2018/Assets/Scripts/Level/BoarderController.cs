using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarderController : MonoBehaviour {

    public Collider2D left;
    public Collider2D right;
    public Collider2D up;
    public Collider2D down;

    private Camera _mainCamera;
    private float _cameraDepth;

    private void Start()
    {
        _mainCamera = MainCameraController.Instance.mainCamera;
        _cameraDepth = Mathf.Abs(_mainCamera.transform.position.z);

        left.transform.position = _mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.5f, _cameraDepth));
        right.transform.position = _mainCamera.ViewportToWorldPoint(new Vector3(1f, 0.5f, _cameraDepth));
        up.transform.position = _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1f, _cameraDepth));
        down.transform.position = _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f, _cameraDepth));
    }
}
