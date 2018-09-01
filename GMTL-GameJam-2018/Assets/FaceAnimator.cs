using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceAnimator : MonoBehaviour {

	private Animator anim;

	
	public float blinkInterval;

	public float randomInterval;

	private float randomExtra;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		randomExtra = Random.Range(-randomInterval, randomInterval);

		StartCoroutine(BlinkRoutine());
	}

	IEnumerator BlinkRoutine()
	{
		yield return new WaitForSeconds(blinkInterval+randomExtra);

		anim.SetTrigger("Blink");

		StartCoroutine(BlinkRoutine());
	}

}
