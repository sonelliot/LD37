using UnityEngine;
using System;
using System.Collections;

public class Player : MonoBehaviour
{
    private SpriteRenderer m_renderer;
    private Rigidbody2D m_body;
    private Hand m_left;
    private Hand m_right;
    private RaycastHit2D m_hit;

    public Sprite down;
    public Sprite up;
    public Sprite left;
    public Sprite right;
    public Interactable target;

    public AudioSource dropIngredient;
    public AudioSource dropPotion;
    public AudioSource gulp;
    public AudioSource hurt;

    public float speed = 10f;
    public float maximumSpeed = 20f;

    public float currentBrewing = 1f;
    public float maximumBrewing = 2f;

    public float braking = 0.9f;

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

    public void Hurt(int amount)
    {
        Health -= amount;
        this.hurt.Play();
    }

    public void Heal(int amount)
    {
        Health += amount;
    }

    public void Boost(float amount)
    {
        this.speed = Mathf.Min(this.speed + amount, this.maximumSpeed);
    }

    public void BrewSpeed(float amount)
    {
        this.currentBrewing = Mathf.Min(
            this.currentBrewing + amount, this.maximumBrewing);
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
        UpdateInteractable();
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

        if (m_hit.collider != null &&
            m_hit.collider.GetComponent<Player>())
        {
            if (hand.Holding<Ingredient>())
            {
                hand.Discard();
                this.dropIngredient.Play();
            }

            if (hand.Holding<Potion>())
            {
                hand.Discard();
                this.dropPotion.Play();
            }
        }
    }

    private void UpdateHandSecondary(Hand hand)
    {
        if (m_hit.collider != null &&
            m_hit.collider.GetComponent<Player>())
        {
            if (hand.Holding<Potion>())
            {
                hand.Give<Potion>().Apply(this);
                this.gulp.Play();
            }
        }
    }

    private void UpdateInteractable()
    {
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_hit = Physics2D.Raycast(position, Vector2.zero);

        if (m_hit.collider != null &&
            m_hit.collider.GetComponent<Interactable>())
        {
            this.target = m_hit.collider.GetComponent<Interactable>();
        }
        else
        {
            this.target = null;
        }
    }

    private T ClickOnContainer<T>()
        where T: class, IContainer
    {
        if (m_hit.collider != null &&
            m_hit.collider.GetComponent<T>() != null)
        {
            var distance = Vector3.Distance(
                this.transform.position, m_hit.collider.transform.position);

            var interactable = m_hit.collider.GetComponent<Interactable>();

            var radius = interactable != null ? interactable.radius : 1.5f;

            if (distance < radius)
            {
                return m_hit.collider.GetComponent<T>();
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
