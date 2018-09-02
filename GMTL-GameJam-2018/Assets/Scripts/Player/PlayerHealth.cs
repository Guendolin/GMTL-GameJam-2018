using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerHealth : MonoBehaviour
{
    public int health;

    public bool wasHit;

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            if(wasHit == false)
            {
                StartCoroutine(HitRoutine());
                wasHit = true;
            }
        }
        else if(other.gameObject.CompareTag("Projectile"))
        {
            if(wasHit == false)
            {
                StartCoroutine(HitRoutine());
                wasHit = true;
            }
            Destroy(other.gameObject);
        }
    }

    IEnumerator HitRoutine()
    {
        health --;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(0.2f);
        wasHit = false;
    }
}