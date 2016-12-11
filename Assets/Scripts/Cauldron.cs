using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Cauldron : MonoBehaviour, IContainer
{
    public List<Ingredient> ingredients;

    public void Update()
    {
        for (var i = 0; i < 4; i++)
        {
            var renderer = transform.GetChild(i)
                .gameObject
                .GetComponent<SpriteRenderer>();

            var ingredient = this.ingredients
                .ElementAtOrDefault(i);

            if (ingredient == Ingredient.None)
            {
                renderer.enabled = false;
            }
            else
            {
                renderer.enabled = true;
                renderer.color = ingredient.GetColor();
            }
        }
    }

    public bool IsFull
    {
        get { return ingredients.Count >= 4; }
    }

    public void Interact(Hand hand)
    {
        if (!IsFull && hand.IsFull)
        {
            this.ingredients.Add(hand.Give<Ingredient>());
        }
    }
}
