using System.Collections;
using UnityEngine;

public class shieldEnemy : enemy
{
    public GameObject shieldPrefab;
    private float fireSpeed = 300f;
    public GameObject bulletPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        base.Start();
        health = 10f;
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
        int chanceRoll = Random.Range(1, 100);
        if (chanceRoll <= 25)
        {
            StartCoroutine(Shield());
        } else
        {
            StartCoroutine(Attack());
        }
    }

    public IEnumerator Shield()
    {
        yield return new WaitForSeconds(2);
        Instantiate(shieldPrefab, new Vector3(0, 100, 0), Quaternion.Euler(0, 0, 90));
        isTurn = false;
        battleSystemManager battleSystem = FindAnyObjectByType<battleSystemManager>();
        battleSystem.nextTurn();
    }

    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(2);
        GameObject bullet;
        Vector2 direction = player.transform.position - transform.position;
        float playerAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, playerAngle - 90));
        bullet.GetComponent<enemyBullet>().damage += damageModifier;
        bullet.GetComponent<Rigidbody2D>().AddForce(direction.normalized * fireSpeed, ForceMode2D.Impulse);
        isTurn = false;
        battleSystemManager battleSystem = FindAnyObjectByType<battleSystemManager>();
        battleSystem.nextTurn();
    }    
}
