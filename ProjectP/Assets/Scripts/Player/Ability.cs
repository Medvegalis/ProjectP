using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Ability : ScriptableObject
{
    [Header("Display info")]
    [SerializeField] 
    public string name;
    [SerializeField]
    public string explanation;

    [Header("Ability info")]
    [SerializeField]
    private int id;
    [SerializeField]
    private int currentLevel;
    [SerializeField]
    private int maxLevel;
    [SerializeField]
    private float valuePerLevel;

    public Ability() 
    {
        currentLevel = 1;
    }

    public void LevelUp()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel++;
        }
    }

    public void SetLevel(int value)
    {
        if (value != 0)
        {
            this.currentLevel = value;
        }
        else
        {
            this.currentLevel = 1;
        }
    }

    public int GetLevel()
    {
        return currentLevel;
    }
    public float GetValuePerLevel()
    {
        return valuePerLevel;
    }
    public int GetId()
    {
        return id;
    }
}
