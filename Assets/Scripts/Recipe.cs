using UnityEngine;
using System.Collections;

public enum Ingredient
{
    Red,
    Green,
    Blue,
    Purple
}

public class Recipe : MonoBehaviour
{
    public string recipeName;
    public Ingredient[] ingredients;
}
