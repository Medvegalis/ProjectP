using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSaveLoad : MonoBehaviour,IDataPersistence
{

    [SerializeField] private Stat health;
    [SerializeField] private Stat damage;
    [SerializeField] private Stat speed;
    public void LoadData(GameData data)
    {
        this.health.SetLevel(data.maxHealthLevel);
        this.damage.SetLevel(data.damageLevel);
        this.speed.SetLevel(data.speedLevel);
    }

    public void SaveData(GameData data)
    {
        data.maxHealthLevel = health.GetLevel();
        data.damageLevel = damage.GetLevel();
        data.speedLevel = speed.GetLevel();
    }
}
