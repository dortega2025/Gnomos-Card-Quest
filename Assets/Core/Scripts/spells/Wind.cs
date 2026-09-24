using System.Collections;
using Assets.Core.Scripts.Spells;
using UnityEngine;
using UnityEngine.InputSystem;
//INCORRECT IMPLEMENTION
//NEED TO CHANGE
public class Wind : Spell
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        base.Start();
        energy = 4f;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (playerTurn != currPlayer.playerTurn)
        {
            playerTurn = currPlayer.playerTurn;
        }
        if (playerTurn)
        {
            IsClicked();
        }
        if (selected)
        {
            SelectTarget();
        }
        if (hasAttacked && !extraTurn)
        {
            StartCoroutine(Discard());
        } else if (hasAttacked && extraTurn)
        {
            extraTurn = false;
            hasAttacked = false;
        }
        if (selected && target != null && !hasAttacked)
        {
            Ability();
            hasAttacked = true;
        }
    }

    void IsClicked()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hitData = Physics2D.Raycast(mousePos, Vector2.zero, 0);
        if (hitData.collider && Mouse.current.leftButton.wasPressedThisFrame && hitData.collider.gameObject == gameObject)
        {
            if (!selected)
            {
                selected = true;
            } else
            {
                selected = false;
            }
        }
    }

    void SelectTarget()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hitData = Physics2D.Raycast(mousePos, Vector2.zero, 0);
        if (hitData.collider && Mouse.current.leftButton.wasPressedThisFrame && hitData.collider.CompareTag("card"))
        {
            targetCard = hitData.collider.gameObject.GetComponent<Spell>();
        }
    }

    void Ability()
    {
        targetCard.extraTurn = true;
        currPlayer.currEnergy -= energy;
    }
}
