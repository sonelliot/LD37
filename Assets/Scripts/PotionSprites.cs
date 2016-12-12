using UnityEngine;
using System.Collections;
using System.Linq;

public class PotionSprites : MonoBehaviour
{
    public Sprite[] sprites;
    public static PotionSprites Instance; // yuck

    public void Start()
    {
        PotionSprites.Instance = this;
    }

    public static Sprite Lookup(Potion potion)
    {
        var sprites = Instance.sprites;

        switch (potion)
        {
            case Potion.HealthPotion:
                return sprites.FirstOrDefault(s => s.name == "potion_health");
            case Potion.SpeedPotion:
                return sprites.FirstOrDefault(s => s.name == "potion_speed");
            case Potion.MagicPotion:
                return sprites.FirstOrDefault(s => s.name == "potion_magic");
            case Potion.LuckPotion:
                return sprites.FirstOrDefault(s => s.name == "potion_luck");
            case Potion.YellowPotion:
                return sprites.FirstOrDefault(s => s.name == "potion_yellow");
        }

        return null;
    }
}
