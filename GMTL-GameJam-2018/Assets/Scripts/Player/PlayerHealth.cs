﻿using System.Collections;
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
            other.gameObject.GetComponent<Fireball>().DestroyMe();
        }
    }

    IEnumerator HitRoutine()
    {
        GameManager.instance.Damage(health);
        health --;
        if(health <= 0)
        {
            GameManager.instance.Died();
            PlayerDied();
        }
        yield return new WaitForSeconds(0.2f);
        wasHit = false;
    }

    public void PlayerDied()
    {
        //Hide UI arrow
        //Play death animation
    }
}