using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public LayerMask blockingLayers;

    public PlayerInput playerInput;
    public PlayerBat playerBat;

    private Rigidbody2D _rioggidBody;
    

    private const float RADIUS = 0.5f;
    private const float MOVEMENT_SPEED = 5f;

	// Use this for initialization
	void Start () {

        playerInput.Init(this);
        playerBat.Init(this);

        _rioggidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 movement = playerInput.GetMovement();

        Vector2? aim = playerInput.GetAim();
        Vector3 movementVelocity = movement * MOVEMENT_SPEED;

        _rioggidBody.velocity = movementVelocity;

    
        if (playerInput.GetSwing())
        {
            playerBat.SwingBat();
        }
        playerBat.Tick(aim);
    }

    private void FixedUpdate()
    {
        playerBat.FixedTick();
    }

}
