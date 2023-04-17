using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum typeOfLoot
    {
        coin,
        health,
        statCoin,
        xp
    }
/// <summary>
/// Loot Object
/// </summary>
[CreateAssetMenu]
public class Loot : ScriptableObject
{

    public typeOfLoot lootType;
    public Sprite lootSprite;
    public string lootName;
    public int dropChance;
    public int value;

    public Loot(string lootName, int dropChance, int value)
    {
        this.lootName = lootName;
        this.dropChance = dropChance;
        this.value = value;
    }
}
