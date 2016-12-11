using UnityEngine;
using System.Collections;

public class IngredientStation : MonoBehaviour, IContainer
{
    public Ingredient ingredient;

    public void Interact(Hand hand)
    {
        hand.Pickup(this.ingredient);
    }
}
