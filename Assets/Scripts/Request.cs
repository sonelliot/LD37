using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Request : MonoBehaviour
{
    public Recipe recipe;
    public float timeout = 60f;
    public float elapsed = 0f;

    public float Progress
    {
        get { return this.elapsed / this.timeout; }
    }

    public void Update()
    {
        UpdateProgress();
        UpdateIngredients();
    }

    private void UpdateProgress()
    {
        this.elapsed += Time.deltaTime;
    }

    private void UpdateIngredients()
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
