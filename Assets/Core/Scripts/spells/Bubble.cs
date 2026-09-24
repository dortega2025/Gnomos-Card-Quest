using Assets.Core.Scripts.Spells;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bubble : Spell
{
    private float extraEnergy = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        base.Start();
        energy = 2f;
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
        if (selected && targetedPlayer && !hasAttacked)
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
        if (hitData.collider && Mouse.current.leftButton.wasPressedThisFrame && hitData.collider.CompareTag("Player"))
        {
            targetedPlayer = true;
        }
    }

    void Ability()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        currPlayer.currEnergy -= energy;
        player.GetComponent<player>().currEnergy += extraEnergy;
    }
}
