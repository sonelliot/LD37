using UnityEngine;
using System;
using System.Collections;

public enum ItemType
{
    Unknown,
    RedPotion,
    GreenPotion,
    BluePotion
}

public class Request
{
    public ItemType ItemType;
    public int Count;

    public static Request Random()
    {
        var itemType = ItemType.Unknown;
        var itemCount = 0;

        var rng = new System.Random();

        switch (rng.Next(0, 3))
        {
            case 0:   { itemType = ItemType.RedPotion;   break; }
            case 1:   { itemType = ItemType.GreenPotion; break; }
            case 2:   { itemType = ItemType.BluePotion;  break; }
            default:  { itemType = ItemType.RedPotion;   break; }
        }

        itemCount = rng.Next(1, 11); // 1 - 10

        return new Request
        {
            ItemType = itemType,
            Count    = itemCount
        };
    }
}
