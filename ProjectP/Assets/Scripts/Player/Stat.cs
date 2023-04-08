using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Stat : ScriptableObject
{
    [SerializeField] private int baseValue;
    [SerializeField] public int currentValue;

    [SerializeField] public int currentLevel;
    [SerializeField] public int maxLevel;
    [SerializeField] public int[] statValues;

    public void LevelUp()
    {
        if(currentLevel < maxLevel)
        {
            currentLevel++;
            currentValue = statValues[currentLevel-1];
        }
    }

    public void SetLevel(int value)
    {
        if (value != 0)
        {
            this.currentLevel = value;
            this.currentValue = statValues[currentLevel - 1];
        }
        else
        {
            this.currentLevel = 0;
            this.currentValue = baseValue;
        }
    }
    public int GetLevel()
    {
        return currentLevel;
    }

    public void AddValue()
    {
        
    }
}
