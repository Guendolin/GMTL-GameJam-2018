using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public enum GameState { Menu, Playing, GameOver };

    public GameState gameState;

    public GameObject mainMenu;
    public GameObject gameOverMenu;
    public GameObject mainGameUI;

    private Transform playerPrefab;

    public Transform healthContainer;

	private int score;
	private int highScore;

	public Text scoreText;

	public Text scoreTextGameOver;
	public GameObject highScoreObject;
	public GameObject mainMenuHighScoreObject;

	public Text mainMenuHighScoreText;

	public Animator scoreAnim;

	public Animator fadeAnimator;

    public Transform Player
    {
        get { return playerPrefab.transform; }
        set { playerPrefab = value; }
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

		highScore = PlayerPrefs.GetInt("Highscore");
        //gameState = GameState.Menu;
		Debug.Log(highScore);
		UpdateHighscore();
		fadeAnimator.SetTrigger("FadeIn");
    }

    void Start()
    {
		
    }

	void UpdateHighscore()
	{
		if(PlayerPrefs.GetInt("Highscore") > 0)
		{
			mainMenuHighScoreObject.SetActive(true);
			mainMenuHighScoreText.text = PlayerPrefs.GetInt("Highscore").ToString();
		}
	}

    public void Damage(int playerHealth)
    {
		if(playerHealth != 0)
		{
        	healthContainer.GetChild(playerHealth - 1).gameObject.GetComponent<Animator>().SetTrigger("Damage");
		}
        CameraShakerController.CameraShake();
        Debug.Log("PlayerHit");
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(ChangeSceneRoutine(sceneName));
    }

	IEnumerator ChangeSceneRoutine(string sceneName)
	{
		fadeAnimator.SetTrigger("FadeOut");
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene(sceneName);
		fadeAnimator.SetTrigger("FadeIn");
		if(sceneName == "MainMenu")
		{
			mainMenu.SetActive(true);
		}
		UpdateHighscore();
	}

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        ChangeScene("MainGame");
		StartCoroutine(StartGameRoutine());
    }

	IEnumerator StartGameRoutine()
	{
		yield return new WaitForSeconds(0.3f);
		mainGameUI.SetActive(true);
		yield return new WaitForSeconds (3.8f);
		score = 0;
        gameState = GameState.Playing;
        
	}
    public void Died()
    {
        gameState = GameState.GameOver;
		StartCoroutine(GameOverRoutine());
    }

	IEnumerator GameOverRoutine()
	{
		yield return new WaitForSeconds(1f);
		mainGameUI.SetActive(false);
        gameOverMenu.SetActive(true);
		scoreTextGameOver.text = score.ToString();
		if(score > PlayerPrefs.GetInt("Highscore"))
		{
			highScoreObject.SetActive(true);
			PlayerPrefs.SetInt("Highscore", score);
			Debug.Log(PlayerPrefs.GetInt("Highscore"));
		}
	}

	public void AddScore()
	{
		scoreAnim.SetTrigger("GetScore");
		score ++;
		scoreText.text = score.ToString();
	}

}
