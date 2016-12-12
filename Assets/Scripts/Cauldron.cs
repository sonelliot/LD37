using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Cauldron : MonoBehaviour, IContainer
{
    private RecipeBook m_recipeBook;
    private Brewing m_brewing;
    private GameObject m_itemGO;
    public List<Ingredient> ingredients;

    public void Start()
    {
        m_recipeBook = GameObject.Find("Recipe Book")
            .GetComponent<RecipeBook>();

        m_brewing = GetComponent<Brewing>();

        m_itemGO = transform.Find("Item").gameObject;
    }

    public void Update()
    {
        UpdateIngredients();
        UpdateItem();
        UpdateCooking();
    }

    private void UpdateItem()
    {
        if (m_brewing.InProgress)
        {
            m_itemGO.SetActive(true);

            var renderer = m_itemGO.GetComponent<SpriteRenderer>();
            renderer.color = m_brewing.recipe.potion.GetColor();

            const float offset = -0.06f;

            m_itemGO.transform.localPosition = new Vector3(
                0f, Mathf.Min(0f, offset * (1f - m_brewing.Progress)), 0f);
        }
        else if (!m_brewing.IsDone)
        {
            m_itemGO.SetActive(false);
        }
    }

    private void UpdateCooking()
    {
        if (IsFull)
        {
            var recipe = m_recipeBook.Match(this.ingredients);
            if (recipe != null && !m_brewing.InProgress)
            {
                m_brewing.Begin(recipe);
            }

            if (recipe == null)
            {
                this.ingredients.Clear();
            }

            if (m_brewing.InProgress && m_brewing.IsBurnt)
            {
                this.ingredients.Clear();
                m_brewing.Stop();
            }
        }
    }

    private void UpdateIngredients()
    {
        for (var i = 0; i < 4; i++)
        {
            var renderer = transform.GetChild(i)
                .gameObject
                .GetComponentsInChildren<SpriteRenderer>()
                .FirstOrDefault();

            var ingredient = this.ingredients
                .ElementAtOrDefault(i);

            if (ingredient == Ingredient.Unknown || m_brewing.IsDone)
            {
                renderer.enabled = false;
            }
            else
            {
                renderer.enabled = true;
                renderer.sprite = IngredientSprites.Lookup(ingredient);
            }
        }
    }

    public bool IsFull
    {
        get { return ingredients.Count >= 4; }
    }

    public void Interact(Hand hand)
    {
        if (!IsFull && hand.IsFull && hand.Holding<Ingredient>())
        {
            this.ingredients.Add(hand.Give<Ingredient>());
        }

        if (m_brewing.IsDone && !hand.IsFull)
        {
            hand.Pickup(m_brewing.recipe.potion);

            m_brewing.Stop();
            this.ingredients.Clear();
        }
    }
}
