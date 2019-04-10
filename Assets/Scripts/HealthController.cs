using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

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
            //behaviour
            Destroy(Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cylinder), transform.position, transform.rotation), 3.0f);
            Destroy(gameObject);
        }
    }
}
