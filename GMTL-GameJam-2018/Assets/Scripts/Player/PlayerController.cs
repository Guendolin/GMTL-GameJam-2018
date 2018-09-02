using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    public LayerMask blockingLayers;

    [Header("Subcomponents")]
    public PlayerInput playerInput;
    public PlayerBat playerBat;
    public PlayerAnimation playerAnimation;

    public Rigidbody2D riggidBody { get; private set; }

    private Vector3 _movementVelocity;

    private const float RADIUS = 0.5f;
    private const float MOVEMENT_SPEED = 2f;

    // Use this for initialization
    void Start()
    {
        riggidBody = GetComponent<Rigidbody2D>();

        playerInput.Init(this);
        playerBat.Init(this);
        playerAnimation.Init(this);

        GameManager.instance.Player = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Playing)
        {
            Vector2 movement = playerInput.GetMovement();

            Vector2? aim = playerInput.GetAim();
            _movementVelocity = movement * MOVEMENT_SPEED;

            playerAnimation.SetPlayerRun(_movementVelocity.magnitude > 0.01f);

            if (playerInput.GetSwing())
            {
                playerBat.SwingBat();
            }
            playerBat.Tick(aim);
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Playing)
        {
            riggidBody.velocity = _movementVelocity;
            playerBat.FixedTick();
        }
        else
        {
            playerAnimation.SetPlayerRun(false);
            riggidBody.velocity = Vector2.zero;
        }
    }

}
