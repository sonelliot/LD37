using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Request : MonoBehaviour
{
    public Recipe recipe;
    public float timeout = 60;

    public void Update()
    {
        for (var i = 0; i < 4; i++)
        {
            var renderer = transform.GetChild(i)
                .gameObject
                .GetComponent<SpriteRenderer>();

            var ingredient = this.recipe.ingredients
                .ElementAtOrDefault(i);

            renderer.color = ingredient.GetColor();
        }
    }
}
