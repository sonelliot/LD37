using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private Rigidbody2D m_body;
    private Hand m_left;
    private Hand m_right;

    public float speed = 10f;
    public float braking = 0.9f;
    public float pickupDistance = 2.0f;

    public void Start()
    {
        m_body = GetComponent<Rigidbody2D>();

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
        if (Input.GetMouseButtonDown(0))
        {
            UpdateHand(m_left);
        }

        if (Input.GetMouseButtonDown(1))
        {
            UpdateHand(m_right);
        }
    }

    private void UpdateHand(Hand hand)
    {
        var containers = new IContainer[]
        {
            ClickOnContainer<IngredientStation>(),
            ClickOnContainer<Cauldron>(),
            ClickOnContainer<RequestChest>()
        };

        foreach (var container in containers)
        {
            if (container != null)
            {
                container.Interact(hand);
                return;
            }
        }

        hand.Discard();
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

    private void Move(Vector2 direction)
    {
        m_body.AddForce(this.speed * direction);
    }

    private void Stop()
    {
        m_body.velocity = m_body.velocity * this.braking;
    }
}
