using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public GameObject deadPrefab;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void DecreaseHealth(float damage)
    {
        currentHealth -= damage;

        CheckHealth();
    }

    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    protected virtual void Dead()
    {
        //behaviour
        Destroy(Instantiate(deadPrefab, transform.position, transform.rotation), 3.0f);
        Destroy(gameObject);
    }
}
