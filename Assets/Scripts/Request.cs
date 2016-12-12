using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Request : MonoBehaviour
{
    public Recipe recipe;
    public float timeout = 60f;
    public float elapsed = 0f;

    public bool IsExpired
    {
        get { return this.elapsed >= this.timeout; }
    }

    public float Progress
    {
        get { return this.elapsed / this.timeout; }
    }

    public void Update()
    {
        UpdateProgress();
        UpdateIngredients();
    }

    public void Expire()
    {
        Destroy(this.gameObject);
        GameManager.Restart();
    }

    public void Complete()
    {
        Destroy(this.gameObject);
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
