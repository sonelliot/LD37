﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RecipeBook : MonoBehaviour
{
    private List<Recipe> m_recipes;

    public void Start()
    {
        m_recipes = GetComponentsInChildren<Recipe>().ToList();
    }

    public Recipe Match(IEnumerable<Ingredient> ingredients)
    {
        return m_recipes.FirstOrDefault(r => r.IsMatch(ingredients));
    }

    public Recipe Random()
    {
        int i = new System.Random().Next(0, m_recipes.Count);
        return m_recipes[i];
    }
}
