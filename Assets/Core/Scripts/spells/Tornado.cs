using System;
using System.Collections;
using Assets.Core.Scripts.Spells;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Tornado : Spell
{
    public GameObject bulletPrefab;
    private float fireSpeed = 300f;
    private float timePassed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        base.Start();
        energy = 3f;
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
        StartCoroutine(FireControl());
    }

    public IEnumerator FireControl()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject bullet;
            Vector2 direction = target.transform.position - transform.position;
            float playerAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, playerAngle - 90));
            bullet.GetComponent<bullet>().damage += damageModifier;
            bullet.GetComponent<Rigidbody2D>().AddForce(direction.normalized * fireSpeed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.5f);
        }
        currPlayer.currEnergy -= energy;
    }
}


