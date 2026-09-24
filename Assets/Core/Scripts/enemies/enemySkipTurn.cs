using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class enemySkipTurn : enemy
{
    public GameObject bulletPrefab;
    private bool hasSkipped;
    private float fireSpeed = 300f;
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
        if (!hasSkipped && chanceRoll <= 25)
        {
            StartCoroutine(SkipTurn());
        } else
        {
            StartCoroutine(Attack());
        }
    }

    public IEnumerator SkipTurn()
    {
        yield return new WaitForSeconds(2);
        battleSystemManager battleSystem = FindAnyObjectByType<battleSystemManager>();
        battleSystem.turnOrder = new Queue<GameObject>(battleSystem.turnOrder.Where(x => x.CompareTag("enemy")));
        battleSystem.turnOrder.Enqueue(player);
        isTurn = false;
        hasSkipped = true;
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
