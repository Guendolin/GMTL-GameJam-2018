using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Camera mainCamera;

    [Space]

    public PlayerInput playerInput;
    public PlayerBat playerBat;


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

        Vector2? aim = playerInput.GetAim();
        Vector3 movementTranslate = (MOVEMENT_SPEED * Time.deltaTime) * movement;

        Vector3 newPos = transform.position + movementTranslate;
        transform.position = newPos;

        if (playerInput.GetSwing())
        {
            playerBat.SwingBat();
        }

        playerBat.Tick(aim);
  

        Debug.DrawRay(transform.position, movement, Color.red);
        if (aim.HasValue)
        {
            Debug.DrawRay(transform.position, aim.Value, Color.green);
        }
    }


}
