using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heath : MonoBehaviour
{
    [SerializeField] private float startingHealth;

    public float currentHealth { get; private set; }
    public bool dead;

    

    private void Awake()
    {
        currentHealth = startingHealth;
    }
   
    public void Gaydame(float _dame)
    {
        currentHealth = Mathf.Clamp(currentHealth - _dame, 0, startingHealth);

        if (currentHealth > 0)
        {

        }
        else
        {
            if (!dead)
            {
                GetComponent<PlayerController>().enabled = false;
                dead = true;
                Destroy(gameObject);
            }
        }

    }
   
}

