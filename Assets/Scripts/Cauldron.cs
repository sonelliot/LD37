using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cauldron : MonoBehaviour, IContainer
{
    public List<Ingredient> ingredients;

    public void Update()
    {
    }

    public bool IsFull
    {
        get { return ingredients.Count >= 4; }
    }

    public void Interact(Hand hand)
    {
        if (!IsFull)
        {
            this.ingredients.Add(hand.Give<Ingredient>());
        }
    }
}
