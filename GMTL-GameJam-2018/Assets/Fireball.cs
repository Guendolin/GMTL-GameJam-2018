using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    private Rigidbody2D rb;

    public GameObject destroyFX;
    public GameObject bounceFX;
    // Use this for initialization
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine(ChangeLayer());
    }

    public void Init(float force)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.forward * force, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 v = rb.velocity;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Enemy"))
		{
			other.gameObject.GetComponent<EnemyBehaviour>().Die();
			DestroyMe();
		}
	}
    void OnCollisionEnter2D(Collision2D other)
	{
		Instantiate(bounceFX, other.contacts[0].point, Quaternion.identity);
	}

	IEnumerator ChangeLayer()
	{
		yield return new WaitForSeconds(0.3f);
		gameObject.layer = 10;
	}

    public void DestroyMe()
    {
        Instantiate(bounceFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
