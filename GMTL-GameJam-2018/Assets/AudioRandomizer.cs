using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomizer : MonoBehaviour {

	public AudioClip clip;

	public AudioClip [] randomClips;
	
	public bool randomStartTime;

	public float minPitch;

	public float maxPitch;

	private AudioSource src;

	public bool useRandomList;

	void Start()
	{
		src = GetComponent<AudioSource>();
		if(useRandomList == false)
		{
			src.clip = clip;
		}
		else
		{
			int randomClip = Random.Range(0, randomClips.Length);
			src.clip = randomClips[randomClip];
		}
		if(randomStartTime)
		{
			src.time = Random.Range(0.00f, clip.length);
		}

		src.pitch = Random.Range(minPitch, maxPitch);

		src.Play();
	}
	
}
