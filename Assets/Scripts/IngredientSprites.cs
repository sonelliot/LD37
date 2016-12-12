using UnityEngine;
using System.Collections;
using System.Linq;

public class IngredientSprites : MonoBehaviour
{
    public Sprite[] sprites;
    public static IngredientSprites Instance; // yuck

    public void Start()
    {
        IngredientSprites.Instance = this;
    }

    public static Sprite Lookup(Ingredient ingredient)
    {
        var sprites = Instance.sprites;

        switch (ingredient)
        {
            case Ingredient.Red:
                return sprites.FirstOrDefault(s => s.name == "orb_red");
            case Ingredient.Green:
                return sprites.FirstOrDefault(s => s.name == "orb_green");
            case Ingredient.Blue:
                return sprites.FirstOrDefault(s => s.name == "orb_blue");
            case Ingredient.Purple:
                return sprites.FirstOrDefault(s => s.name == "orb_purple");
            case Ingredient.Yellow:
                return sprites.FirstOrDefault(s => s.name == "orb_yellow");
        }

        return null;
    }
}
