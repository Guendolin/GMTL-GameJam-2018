using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioClip mainMenuMusic;

	public AudioClip gameMusic;

	private AudioSource src;

	// Use this for initialization
	void Start () {
		src = GetComponent<AudioSource>();
		src.clip = mainMenuMusic;
		src.volume = 1;
		src.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeToMain()
	{
		StartCoroutine(ChangeTrackRoutine(mainMenuMusic));
	}

	public void ChangeToGame()
	{
		StartCoroutine(ChangeTrackRoutine(gameMusic));
	}

	public void ChangeToGameOver()
	{
		StartCoroutine(ChangePitchRoutine());
	}

	IEnumerator ChangeTrackRoutine(AudioClip newClip)
	{
		float timer = 0;
		float duration = 0.5f;

		while(timer < duration)
		{
			timer += Time.deltaTime;
			src.volume = Mathf.Lerp(src.volume, 0, timer/duration);

			yield return null;
		}
		src.volume = 0f;
		src.clip = newClip;
		src.pitch = 1f;
		src.Play();
		timer = 0;

		while(timer < duration)
		{
			timer += Time.deltaTime;
			src.volume = Mathf.Lerp(src.volume, 1, timer/duration);

			yield return null;
		}
		src.volume = 1f;

		yield return null;
	}

	IEnumerator ChangePitchRoutine()
	{
		float timer = 0;
		float duration = 2f;

		while(timer < duration)
		{
			timer += Time.deltaTime;
			src.pitch = Mathf.Lerp(src.volume, 0.3f, timer/duration);

			yield return null;
		}
		src.pitch = 0.3f;

		yield return null;
	}


}
