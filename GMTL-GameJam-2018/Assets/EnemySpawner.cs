using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;

    public float spawnTime;

    [HeaderAttribute("From 0 + and - to edge of screen")]
    public float spawnXPos;
    public float spawnYPos;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemyRoutine()
    {
		//Delay to make sure that the player has been spawned
		yield return new WaitForSeconds(0.1f);
        float randomX = Random.Range(-spawnXPos, spawnXPos);
        float randomY = Random.Range(-spawnYPos, spawnYPos);
        Vector2 randomSpawnPos = new Vector2(randomX, randomY);
        Instantiate(enemyPrefab, randomSpawnPos, Quaternion.identity);

        yield return new WaitForSeconds(spawnTime);

        if (spawnTime > 0.5f)
        {
            spawnTime -= 0.1f;
        }
        if (GameManager.instance.gameState == GameManager.GameState.Playing)
        {
            StartCoroutine(SpawnEnemyRoutine());
        }

    }
}
