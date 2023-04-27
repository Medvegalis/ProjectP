using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityControler : MonoBehaviour
{
    [SerializeField]
    private Ability[] abilities;
    public int abiltyCount;

    [SerializeField]
    private Ability[] enabledAbilities;
    public int enabledAbilityCount;

    // Start is called before the first frame update
    void Start()
    {
        abiltyCount = abilities.Length;
        enabledAbilityCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableAbility(int abilityId) 
    {
		if (abilityId >= abiltyCount || abilityId < 0)
		{
            return;
		}

        enabledAbilities[enabledAbilityCount++] = abilities[abilityId];
    }

    public Ability getAbility(int abilityId) 
    {
        if (abilityId > abiltyCount || abilityId < 0)
        {
            return null;
        }
        return abilities[abilityId];
    }

    public Ability getEnabledAbility(int abilityId)
    {
        if (abilityId > enabledAbilityCount || abilityId < 0)
        {
            return null;
        }
        return enabledAbilities[abilityId];
    }
}
