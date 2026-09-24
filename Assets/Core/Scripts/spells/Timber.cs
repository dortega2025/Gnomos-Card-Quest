using Assets.Core.Scripts.Spells;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Timber : Spell
{
    public GameObject shieldPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        base.Start();
        energy = 2f;
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
        Instantiate(shieldPrefab, new Vector3(0, 60, 0), Quaternion.Euler(0, 0, 90));
        currPlayer.currEnergy -= energy;
    }
}
