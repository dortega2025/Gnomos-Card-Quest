using System.Collections;
using UnityEngine;

public class enemyMultiAttack : enemy
{
    public GameObject bulletPrefab;
    private float fireSpeed = 300f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        base.Start();
        health = 10f;
        damageModifier = -5f;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        base.Update();
        if (isTurn && !hasAttacked)
        {
            Ability();
            hasAttacked = true;
        }
    }

    void Ability()
    {
        StartCoroutine(Attack());
    }

    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 3; i++)
        {
            GameObject bullet;
            Vector2 direction = player.transform.position - transform.position;
            float playerAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, playerAngle - 90));
            bullet.GetComponent<enemyBullet>().damage += damageModifier;
            bullet.GetComponent<Rigidbody2D>().AddForce(direction.normalized * fireSpeed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.5f);
        }
        
        isTurn = false;
        battleSystemManager battleSystem = FindAnyObjectByType<battleSystemManager>();
        battleSystem.nextTurn();
    }
}
