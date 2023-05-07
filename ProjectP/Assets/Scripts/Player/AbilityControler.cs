using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityControler : MonoBehaviour
{
    [SerializeField]
    private Ability[] abilities;
    public int abiltyCount;

    // Start is called before the first frame update
    void Start()
    {
        abiltyCount = abilities.Length;
		foreach (Ability ability in abilities)
		{
            ability.disableAbility();
            ability.SetLevel(1);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableOrLevelUpAbility(int abilityId) 
    {
        int index = findAbilityIndexByID(abilityId);
        if (index < 0)
        {
            return;
        }

        Ability ability = abilities[index];

        if (ability.abilityIsEnabled())
        {
            ability.LevelUp();
        }
        else 
        {
            ability.enableAbility();
        }
    }

    public void enableAbility(int abilityId)
    {
        int index = findAbilityIndexByID(abilityId);
        if (index < 0)
        {
            return;
        }

        abilities[index].enableAbility();
    }

    public Ability getAbility(int abilityId)
    {
        int index = findAbilityIndexByID(abilityId);
        if (index < 0)
        {
            return null;
        }
        return abilities[index];
    }

    private int findAbilityIndexByID(int abilityId) 
    {
        int abilityIndex = -1;
		for (int i = 0; i < abilities.Length; i++)
		{
			if (abilities[i].GetId() == abilityId)
			{
                abilityIndex = i;
                break;
			}
		}
        return abilityIndex;
    }
}
