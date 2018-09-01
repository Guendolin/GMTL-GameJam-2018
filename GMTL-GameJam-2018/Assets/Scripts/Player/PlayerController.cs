using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Movement")]
    public LayerMask blockingLayers;

    [Header("Subcomponents")]
    public PlayerInput playerInput;
    public PlayerBat playerBat;
    public PlayerAnimation playerAnimation;

    public Rigidbody2D riggidBody { get; private set; }

    private const float RADIUS = 0.5f;
    private const float MOVEMENT_SPEED = 5f;

	// Use this for initialization
	void Start ()
    {
        riggidBody = GetComponent<Rigidbody2D>();

        playerInput.Init(this);
        playerBat.Init(this);
        playerAnimation.Init(this);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = playerInput.GetMovement();

        Vector2? aim = playerInput.GetAim();
        Vector3 movementVelocity = movement * MOVEMENT_SPEED;

        riggidBody.velocity = movementVelocity;

        playerAnimation.SetPlayerRun(movementVelocity.magnitude > 0.01f);
    
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
