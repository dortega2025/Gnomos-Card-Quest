using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float health;
    public GameObject player;
    public bool isTurn;
    public bool hasAttacked;
    public float damageModifier = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    public void Update()
    {
        CheckHealth();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        health -= collision.gameObject.GetComponent<bullet>().damage;
    }

    void CheckHealth()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
