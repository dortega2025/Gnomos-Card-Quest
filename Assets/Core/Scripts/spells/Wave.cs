using System.Collections.Generic;
using Assets.Core.Scripts.Spells;
using UnityEngine;

public class Wave : Spell
{
    private GameObject[] targets;
    public GameObject bulletPrefab;
    private float fireSpeed = 300f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        base.Start();
        energy = 5f;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        base.Update();
        if (selected && target != null && !hasAttacked)
        {
            Ability();
            hasAttacked = true;
        }
    }

    void Ability()
    {
        targets = GameObject.FindGameObjectsWithTag("enemy");
        for (int i = 0; i < targets.Length; i++)
        {
            target = targets[i].GetComponent<enemy>();
            GameObject bullet;
            Vector2 direction = target.transform.position - transform.position;
            float playerAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, playerAngle - 90));
            bullet.GetComponent<bullet>().damage += damageModifier;
            bullet.GetComponent<Rigidbody2D>().AddForce(direction.normalized * fireSpeed, ForceMode2D.Impulse);
        }
        currPlayer.currEnergy -= energy;
    }
}
