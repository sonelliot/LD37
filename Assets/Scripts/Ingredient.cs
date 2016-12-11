using UnityEngine;

public enum Ingredient
{
    None,
    Red,
    Green,
    Blue,
    Purple
}

public static class IngredientExtensions
{
    public static Color GetColor(this Ingredient ingredient)
    {
        switch (ingredient)
        {
            case Ingredient.Red:     return Colors.red;
            case Ingredient.Green:   return Colors.green;
            case Ingredient.Blue:    return Colors.blue;
            case Ingredient.Purple:  return Colors.purple;

            case Ingredient.None:
            default:
                return Color.white;
        }
    }
}
