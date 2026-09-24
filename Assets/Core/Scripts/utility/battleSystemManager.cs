using System;
using System.Collections.Generic;
using Assets.Core.Scripts;
using Assets.Core.Scripts.Spells;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class battleSystemManager : MonoBehaviour
{
    public Queue<GameObject> turnOrder;
    public GameObject player;
    public GameObject[] enemies;
    public bool win;
    public bool lose;
    public bool hasSkipped;
    public TMP_Text energyLabel;
    public TMP_Text healthLabel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turnOrder = new Queue<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        turnOrder.Enqueue(player);
        for (int i = 0; i < enemies.Length; i++)
        {
            turnOrder.Enqueue(enemies[i]);
        }
        energyLabel = GameObject.FindGameObjectWithTag("energy").GetComponent<TMP_Text>();
        energyLabel.text = "Energy: " + player.GetComponent<player>().currEnergy;
        healthLabel = GameObject.FindGameObjectWithTag("health").GetComponent<TMP_Text>();
        healthLabel.text = "Health: " + player.GetComponent<player>().currHealth;
    }

    // Update is called once per frame
    void Update()
    {
        energyLabel.text = "Energy: " + player.GetComponent<player>().currEnergy;
        healthLabel.text = "Health: " + player.GetComponent<player>().currHealth;
        if (GameObject.FindGameObjectsWithTag("enemy") == null)
        {
            win = true;
            //display something
        }
        if (player.GetComponent<player>().currHealth <= 0)
        {
            lose = true;
            //display something
        }
    }

    public void nextTurn()
    {
        Debug.Log(turnOrder.Peek().ToString());
        if (turnOrder.Peek().CompareTag("enemy"))
        {
            turnOrder.Peek().GetComponent<enemy>().isTurn = false;
            turnOrder.Peek().GetComponent<enemy>().hasAttacked = false;
        }
        turnOrder.Enqueue(turnOrder.Peek());
        turnOrder.Dequeue();
        if (turnOrder.Peek().CompareTag("Player") && hasSkipped)
        {
            turnOrder.Clear();
            enemies = GameObject.FindGameObjectsWithTag("enemy");
            turnOrder.Enqueue(player);
            for (int i = 0; i < enemies.Length; i++)
            {
                turnOrder.Enqueue(enemies[i]);
            }
            hasSkipped = false;
        }
        if (turnOrder.Peek().CompareTag("Player"))
        {
            ActivateCards();
            player.GetComponent<player>().playerTurn = true;
            player.GetComponent<player>().currEnergy = player.GetComponent<player>().maxEnergy;
        } else
        {
            DeactivateCards();
            player.GetComponent<player>().playerTurn = false;
        }
        if (turnOrder.Peek().CompareTag("enemy"))
        {
            turnOrder.Peek().GetComponent<enemy>().isTurn = true;
        }
    }

    void ActivateCards()
    {
        Spell[] spells = FindObjectsByType<Spell>(FindObjectsSortMode.None);
        for (int i = 0; i < spells.Length; i++)
        {
            spells[i].playerTurn = true;
        }
    }

    public void DeactivateCards()
    {
        Spell[] spells = FindObjectsByType<Spell>(FindObjectsSortMode.None);
        for (int i = 0; i < spells.Length; i++)
        {
            spells[i].playerTurn = false;
        }
    }
}
