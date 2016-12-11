using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Recipe : MonoBehaviour
{
    private Dictionary<Ingredient, int> m_count;
    public Potion potion;
    public Ingredient[] ingredients;
    public float duration;

    public void Start()
    {
        m_count = Recipe.Count(this.ingredients);
    }

    public bool IsMatch(IEnumerable<Ingredient> ingredients)
    {
        var other = Recipe.Count(ingredients);
        if (m_count.Count != other.Count)
        {
            return false;
        }

        foreach (var ingredient in m_count.Keys)
        {
            if (!other.ContainsKey(ingredient))
            {
                return false;
            }

            if (m_count[ingredient] != other[ingredient])
            {
                return false;
            }
        }

        return true;
    }

    public static Dictionary<Ingredient, int> Count(
        IEnumerable<Ingredient> ingredients)
    {
        var count = new Dictionary<Ingredient, int>();
        foreach (var ingredient in ingredients)
        {
            if (!count.ContainsKey(ingredient))
            {
                count[ingredient] = 0;
            }
            count[ingredient]++;
        }
        return count;
    }
}
