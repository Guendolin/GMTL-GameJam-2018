using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

	public float timeToDestroy;

	public bool destroyWithParticle;

	public GameObject particle;
	// Use this for initialization
	void Start () {
		StartCoroutine(DestroyRoutine());
	}

	IEnumerator DestroyRoutine()
	{
		yield return new WaitForSeconds(timeToDestroy);
		if(destroyWithParticle)
		{
			Instantiate(particle, transform.position, Quaternion.identity);
		}
		Destroy(gameObject);
	}

}
