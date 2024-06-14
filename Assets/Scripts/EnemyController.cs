using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float dame;

    public int value;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("arrow"))
        {
            Destroy(gameObject);
            Score.Instance.IncreaseCoins(value);
        }
        else if (other.CompareTag("Player"))
        {
            Heath playerHealth = other.GetComponent<Heath>();
            if (playerHealth != null)
            {
                playerHealth.Gaydame(dame);
            }
        }
    }

}
