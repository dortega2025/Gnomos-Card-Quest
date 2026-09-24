using Unity.VisualScripting;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public float damage = 10f;
    private float timePassed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > 5f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
