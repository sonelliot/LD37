using UnityEngine;
using System;
using System.Collections;

public class Player : MonoBehaviour
{
    private SpriteRenderer m_renderer;
    private Rigidbody2D m_body;
    private Hand m_left;
    private Hand m_right;

    public Sprite down;
    public Sprite up;
    public Sprite left;
    public Sprite right;

    public float speed = 10f;
    public float braking = 0.9f;
    public float pickupDistance = 2.0f;

    public int currentHealth = 3;
    public int maximumHealth = 3;

    public int Health
    {
        get { return this.currentHealth; }
        set
        {
            this.currentHealth = Math.Max(
               Math.Min(value, this.maximumHealth), 0);
        }
    }

    public void Start()
    {
        m_body = GetComponent<Rigidbody2D>();
        m_renderer = GetComponent<SpriteRenderer>();

        var hands = GetComponentsInChildren<Hand>();
        m_left  = hands[0];
        m_right = hands[1];
    }

    public void Update()
    {
        UpdateMovement();
        UpdateHands();
    }

    private void UpdateHands()
    {
        bool secondary = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetMouseButtonDown(0))
        {
            UpdateHand(m_left, secondary);
        }

        if (Input.GetMouseButtonDown(1))
        {
            UpdateHand(m_right, secondary);
        }
    }

    private void UpdateHand(Hand hand, bool secondary)
    {
        if (secondary)
        {
            UpdateHandSecondary(hand);
        }
        else
        {
            UpdateHandPrimary(hand);
        }
    }

    private void UpdateHandPrimary(Hand hand)
    {
        var containers = new IContainer[]
        {
            ClickOnContainer<IngredientStation>(),
            ClickOnContainer<Cauldron>(),
            ClickOnContainer<RequestChest>(),
            ClickOnContainer<MixingTable>()
        };

        foreach (var container in containers)
        {
            if (container != null)
            {
                container.Interact(hand);
                return;
            }
        }
    }

    private void UpdateHandSecondary(Hand hand)
    {
        if (hand.Holding<Ingredient>())
        {
            hand.Discard();
        }
        else if (hand.Holding<Potion>())
        {
            hand.Give<Potion>().Apply(this);
        }
    }

    private T ClickOnContainer<T>()
        where T: class, IContainer
    {
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.Raycast(position, Vector2.zero);

        if (hit.collider != null &&
            hit.collider.GetComponent<T>() != null)
        {
            var distance = Vector3.Distance(
                this.transform.position, hit.collider.transform.position);

            if (distance < this.pickupDistance)
            {
                return hit.collider.GetComponent<T>();
            }
        }

        return null;
    }

    private void UpdateMovement()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        if (Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0)
        {
            Move(new Vector2(h, v).normalized);
        }
        else
        {
            Stop();
        }
    }

    private Sprite FacingSprite(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            var side = Mathf.Sign(direction.x);
            return side > 0 ? this.right : this.left;
        }
        else
        {
            var side = Mathf.Sign(direction.y);
            return side > 0 ? this.up : this.down;
        }
    }

    private void Move(Vector2 direction)
    {
        m_body.AddForce(this.speed * direction);
        m_renderer.sprite = FacingSprite(direction);
    }

    private void Stop()
    {
        m_body.velocity = m_body.velocity * this.braking;
    }
}
