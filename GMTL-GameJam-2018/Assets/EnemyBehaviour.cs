﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    private Transform playerTarget;

	public Transform firePosition;

    private Rigidbody2D rb;

    public float speed;

    [SerializeField]private Animator headAnim;

    public GameObject fireball;

    public float fireballSpeed;

	public GameObject destroyFX;

	private bool isdead;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ShootFireballRoutine());
		playerTarget = GameManager.instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Playing)
        {
            if (GameManager.instance.Player == null)
            {
                return;
            }

            if (playerTarget == null)
            {
                playerTarget = GameManager.instance.Player;
            }

            Vector2 dirVector = playerTarget.position - transform.position;
            rb.AddForce(dirVector * speed * Time.deltaTime, ForceMode2D.Impulse);
        }

    }

    IEnumerator ShootFireballRoutine()
    {
        yield return new WaitForSeconds(3f);
        if (GameManager.instance.gameState == GameManager.GameState.Playing)
        {
            Quaternion towardsPlayerRot = Quaternion.LookRotation(playerTarget.position - transform.position, Vector3.forward);
            GameObject newFireball = Instantiate(fireball, firePosition.position, towardsPlayerRot);
            newFireball.GetComponent<Fireball>().Init(fireballSpeed);
			headAnim.SetTrigger("Fire");
            StartCoroutine(ShootFireballRoutine());
        }
    }

	public void Die()
	{
		if(isdead == false)
		{
			Instantiate(destroyFX, transform.position, Quaternion.identity);
			GameManager.instance.AddScore();
			isdead = true;
		}
		Destroy(gameObject);
	}
}
