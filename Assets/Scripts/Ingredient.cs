using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public enum Ingredient
{
    Unknown,
    Red,
    Green,
    Blue,
    Purple,
    Yellow
}

public class Combination
{
    public Ingredient x;
    public Ingredient y;
    public Ingredient mix;
    public float duration;

    public Combination(
        Ingredient x,
        Ingredient y,
        Ingredient mix,
        float duration)
    {
        this.x = x;
        this.y = y;
        this.mix = mix;
        this.duration = duration;
    }

    public bool Match(Ingredient a, Ingredient b)
    {
        return
            (this.x == a && this.y == b) ||
            (this.x == b && this.y == a);
    }
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
            case Ingredient.Yellow:  return Colors.yellow;

            case Ingredient.Unknown:
            default:
                return Color.white;
        }
    }

    public static Combination Combine(this Ingredient a, Ingredient b)
    {
        var combinations = new List<Combination>
        {
            new Combination(Ingredient.Blue, Ingredient.Green, Ingredient.Yellow, 10f)
        };

        return combinations
            .Where(c => c.Match(a, b))
            .FirstOrDefault();
    }
}
