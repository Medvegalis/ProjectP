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
    public int pointsCount;

    public GameData()
    {
        this.coinCount = 0;
        this.playerPosition = new Vector3(17, 23, 0);
        this.maxHealthLevel = 0;
        this.speedLevel = 0;
        this.damageLevel = 0;
        this.pointsCount = 0;
}
}
