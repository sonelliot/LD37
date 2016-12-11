using UnityEngine;
using System.Collections;

public enum Potion
{
    HealthPotion,
    ManaPotion,
    StaminaPotion,
    LuckPotion
}

public static class PotionExtensions
{
    public static Color GetColor(this Potion potion)
    {
        switch (potion)
        {
            case Potion.HealthPotion:   return Colors.red;
            case Potion.StaminaPotion:  return Colors.green;
            case Potion.ManaPotion:     return Colors.blue;
            case Potion.LuckPotion:     return Colors.purple;
            default:
                return Color.white;
        }
    }
}
