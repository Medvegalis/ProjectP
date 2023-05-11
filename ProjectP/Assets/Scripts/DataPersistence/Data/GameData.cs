using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int coinCount;
    public Vector3 playerPosition;
    public int maxHealthLevel;
    public int speedLevel;
    public int damageLevel;
    public int playerLevel;
    public int playerCurrentXP;
    public int pointsCount;

    public List<Ability> activeAbilities;
    public GameObject[] weaponList;

    public GameData()
    {
        this.coinCount = 0;
        this.playerPosition = new Vector3(17, 23, 0);
        this.maxHealthLevel = 0;
        this.speedLevel = 0;
        this.damageLevel = 0;
        this.playerLevel = 0;
        this.playerCurrentXP = 0;
        this.pointsCount = 0;
        this.activeAbilities = new List<Ability>();
        this.weaponList = new GameObject[2];
}
}
