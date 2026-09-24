using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Core.Scripts.Spells;
using NUnit.Framework;
using UnityEngine;

public class Sun : Spell
{
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
        battleSystemManager battleSystem = FindAnyObjectByType<battleSystemManager>();
        battleSystem.turnOrder = new Queue<GameObject>(battleSystem.turnOrder.Where(x => x.gameObject.name != target.name));
        GameObject[] turns = battleSystem.turnOrder.ToArray();
        for (int i = 0; i < turns.Length; i++)
        {
            Debug.Log(turns[i].ToString());
        }
        battleSystem.hasSkipped = true;
        currPlayer.currEnergy -= energy;
    }
}
