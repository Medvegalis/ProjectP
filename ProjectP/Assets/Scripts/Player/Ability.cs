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
    private bool isEnabled;
    [SerializeField]
    private int currentLevel;
    [SerializeField]
    private int maxLevel;
    [SerializeField]
    private float valuePerLevel;
    [SerializeField]
    public int rarity;

    public Ability() 
    {
        currentLevel = 1;
        isEnabled = false;
    }

    public void LevelUp()
    {
        if (currentLevel <= maxLevel)
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
    public bool abilityIsEnabled()
    {
        return isEnabled;
    }

    public void enableAbility() 
    {
        isEnabled = true;
    }

    public void disableAbility()
    {
        isEnabled = false;
    }
}
