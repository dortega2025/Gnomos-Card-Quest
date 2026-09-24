using Assets.Core.Scripts;
using UnityEngine;

public class player : MonoBehaviour
{
    public bool playerTurn;
    public float maxHealth = 50f;
    public float currHealth = 50f;
    public float maxEnergy = 10f;
    public float currEnergy = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        currHealth -= collision.gameObject.GetComponent<enemyBullet>().damage;
    }

    void CheckHealth()
    {
        if (currHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
