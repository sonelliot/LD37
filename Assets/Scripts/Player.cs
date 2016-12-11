using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private Rigidbody2D m_body;
    public float speed = 10f;
    public float braking = 0.9f;

    public void Awake()
    {
        m_body = GetComponent<Rigidbody2D>();
    }

    public void Update()
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
