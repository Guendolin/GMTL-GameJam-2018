using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    private Transform playerTarget;

    private Rigidbody2D rb;

    public float speed;

    private Animator mouthAnim;

    public GameObject fireball;

    public float fireballSpeed;

	public GameObject destroyFX;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ShootFireballRoutine());
		playerTarget = GameManager.instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Playing)
        {
            Vector2 dirVector = playerTarget.position - transform.position;
            rb.AddForce(dirVector * speed * Time.deltaTime, ForceMode2D.Impulse);
        }

    }

    IEnumerator ShootFireballRoutine()
    {
        yield return new WaitForSeconds(3f);
        if (GameManager.instance.gameState == GameManager.GameState.Playing)
        {
            Quaternion towardsPlayerRot = Quaternion.LookRotation(playerTarget.position - transform.position, Vector3.forward);
            GameObject newFireball = Instantiate(fireball, transform.position, towardsPlayerRot);
            newFireball.GetComponent<Fireball>().Init(fireballSpeed);
            StartCoroutine(ShootFireballRoutine());
        }
    }

	public void Die()
	{
		Instantiate(destroyFX, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
