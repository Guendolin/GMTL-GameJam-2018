using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Camera mainCamera;

    [Space]

    public Transform bat;

    [Space]

    public PlayerInput playerInput;

    private const float MOVEMENT_SPEED = 5f;


	// Use this for initialization
	void Start () {

        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }


        playerInput.Init(this);
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 movement = playerInput.GetMovement();

        bool aimPressent;
        Vector2 aim = playerInput.GetAim(out aimPressent);
        Vector3 movementTranslate = (MOVEMENT_SPEED * Time.deltaTime) * movement;

        Vector3 newPos = transform.position + movementTranslate;
        transform.position = newPos;

        if (aimPressent)
        {
            float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
            Quaternion batRotation = Quaternion.Euler(0, 0, angle);

            bat.localRotation = batRotation;
        }

        Debug.DrawRay(transform.position, movement, Color.red);
        if (aimPressent)
        {
            Debug.DrawRay(transform.position, aim, Color.green);
        }

        
    }
}
