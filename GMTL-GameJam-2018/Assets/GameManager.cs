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

    private Transform playerPrefab;

    public Transform healthContainer;

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
    }

    void Start()
    {
        gameState = GameState.Menu;
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
