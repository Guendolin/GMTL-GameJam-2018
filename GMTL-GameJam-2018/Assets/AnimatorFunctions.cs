using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFunctions : MonoBehaviour {

	public GameObject woosh;
	public GameObject hit;

	public GameObject dustClouds;

	public Transform dustCloudSpawnPos;

	public void Shake()
	{
		CameraShakerController.CameraShake();
		Instantiate(hit, transform.position, Quaternion.identity);
	}

	public void PlayWoosh()
	{
		Instantiate(woosh, transform.position, Quaternion.identity);
	}

	public void SpawnDustCloud()
	{
		Instantiate(dustClouds, dustCloudSpawnPos.position, Quaternion.identity);
	}
}
