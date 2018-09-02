using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public enum GameState { Menu, Playing, GameOver };

    public GameState gameState;

	public GameObject mainMenu;
	public GameObject gameOverMenu;
	public GameObject mainGameUI;

    void Awake()
    {
        if (instance != null)
        {
			Destroy(instance.gameObject);
        }

		instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        gameState = GameState.Menu;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
		gameState = GameState.Playing;
		mainGameUI.SetActive(true);
    }
	public void Died()
    {
        gameState = GameState.GameOver;
		mainGameUI.SetActive(false);
		gameOverMenu.SetActive(true);
    }

}
