﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	public Transform playerTarget;

	private Rigidbody2D rb;

	public float speed;

	private Animator mouthAnim;

	public GameObject fireball;

	public float fireballSpeed;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine(ShootFireballRoutine());
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 dirVector = playerTarget.position - transform.position;
		rb.AddForce(dirVector*speed*Time.deltaTime, ForceMode2D.Impulse);
		
	}

	IEnumerator ShootFireballRoutine()
	{
		yield return new WaitForSeconds(3f);
		Quaternion towardsPlayerRot = Quaternion.LookRotation(playerTarget.position - transform.position, Vector3.forward);
		GameObject newFireball = Instantiate(fireball, transform.position, towardsPlayerRot);
		newFireball.GetComponent<Fireball>().Init(fireballSpeed);
		StartCoroutine(ShootFireballRoutine());
	}
}
