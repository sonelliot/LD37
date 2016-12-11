using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour
{
    private SpriteRenderer m_renderer;
    public object thing;

    public void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if (this.thing is Ingredient)
        {
            var ingredient = (Ingredient)this.thing;
            m_renderer.color = ingredient.GetColor();
        }
    }

    public void Pickup(object thing)
    {
        this.thing = thing;
    }
}
