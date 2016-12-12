using UnityEngine;
using System;
using System.Collections;

public enum Potion
{
    Unknown,
    HealthPotion,
    MagicPotion,
    SpeedPotion,
    LuckPotion,
    YellowPotion
}

public static class PotionExtensions
{
    public static Color GetColor(this Potion potion)
    {
        switch (potion)
        {
            case Potion.HealthPotion: return Colors.red;
            case Potion.SpeedPotion:  return Colors.green;
            case Potion.MagicPotion:  return Colors.blue;
            case Potion.LuckPotion:   return Colors.purple;
            case Potion.YellowPotion: return Colors.yellow;
            default:
                return Color.white;
        }
    }

    public static void Apply(this Potion potion, Player player)
    {
        switch (potion)
        {
            case Potion.HealthPotion:
                player.Heal(1);
                break;
            case Potion.SpeedPotion:
                player.Boost(5f);
                break;
            case Potion.MagicPotion:
                player.BrewSpeed(0.2f);
                break;
            default:
                // nothing!
                break;
        }
    }
}
