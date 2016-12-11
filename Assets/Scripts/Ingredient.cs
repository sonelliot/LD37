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
            case Ingredient.Red:     return new Color(1.00f, 0.50f, 0.50f);
            case Ingredient.Green:   return new Color(0.52f, 0.74f, 0.37f);
            case Ingredient.Blue:    return new Color(0.41f, 0.67f, 0.83f);
            case Ingredient.Purple:  return new Color(0.83f, 0.41f, 0.75f);

            case Ingredient.None:
            default:
                return Color.white;
        }
    }
}
