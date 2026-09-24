using UnityEngine;

public class shield : MonoBehaviour
{
    public float health = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        health -= collision.gameObject.GetComponent<enemyBullet>().damage;
    }

    void CheckHealth()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
