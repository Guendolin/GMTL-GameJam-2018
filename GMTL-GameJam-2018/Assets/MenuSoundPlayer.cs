using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundPlayer : MonoBehaviour {

	private AudioSource src;

	public AudioClip [] clips;
	// Use this for initialization
	void Start () {
		src = GetComponent<AudioSource>();
	}

	public void PlayHover()
	{
		src.Stop();
		src.clip = clips[0];
		src.pitch = Random.Range(1.2f, 1.4f);
		src.Play();
	}
	public void PlaySelect()
	{
		src.Stop();
		src.clip = clips[0];
		src.pitch = Random.Range(2.2f, 2.4f);
		src.Play();
	}
	public void StartGame()
	{
		src.Stop();
		src.clip = clips[1];
		src.pitch = 1f;
		src.Play();
	}
}
