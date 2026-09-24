using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Assets.Core.Scripts.Spells {
    public class Spell : MonoBehaviour
    {
        protected float energy;
        public bool selected;
        [SerializeField] protected enemy target;
        [SerializeField] protected Spell targetCard;
        protected bool targetedPlayer;
        protected bool hasAttacked;
        public bool playerTurn;
        public bool extraTurn;
        public float damageModifier = 0f;
        public player currPlayer;
        private Vector3 baseSize;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public virtual void Start()
        {
            currPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
            baseSize = transform.localScale;
        }

        // Update is called once per frame
        public virtual void Update()
        {
            if (playerTurn != currPlayer.playerTurn)
            {
                playerTurn = currPlayer.playerTurn;
            }
            if (playerTurn && currPlayer.currEnergy > energy)
            {
                IsClicked();
            } else
            {
                //display "not enough energy" message
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
                    Select();
                } else
                {
                    selected = false;
                    Deselect();
                }
            }
        }

        void SelectTarget()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hitData = Physics2D.Raycast(mousePos, Vector2.zero, 0);
            if (hitData.collider && Mouse.current.leftButton.wasPressedThisFrame && hitData.collider.CompareTag("enemy"))
            {
                target = hitData.collider.gameObject.GetComponent<enemy>();
            }
        }

        public IEnumerator Discard()
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }

        void Select()
        {
            transform.localScale += new Vector3(baseSize.x * 0.5f, baseSize.y * 0.5f, 0f);
        }

        void Deselect()
        {
            transform.localScale = baseSize;
        }
    }
}
