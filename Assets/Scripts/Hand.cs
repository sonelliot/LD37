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
            m_renderer.enabled = true;
            m_renderer.sprite = IngredientSprites.Lookup(ingredient);
        }
        else if (this.thing is Potion)
        {
            var potion = (Potion)this.thing;
            m_renderer.enabled = true;
            m_renderer.color = potion.GetColor();
        }
        else
        {
            m_renderer.color = Color.white;
            m_renderer.enabled = false;
        }
    }

    public void Discard()
    {
        this.thing = null;
    }

    public void Pickup(object thing)
    {
        this.thing = thing;
    }

    public bool IsFull
    {
        get { return this.thing != null; }
    }

    public bool IsEmpty
    {
        get { return this.thing == null; }
    }

    public T Give<T>()
    {
        var thing = this.thing;
        this.thing = null;
        return (T)thing;
    }

    public bool Holding<T>()
    {
        return this.thing is T;
    }
}
