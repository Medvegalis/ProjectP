using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityControler : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private Ability[] abilities;
    private List<Ability> enabledAbilities;
    public int abiltyCount;

    // Start is called before the first frame update
    void Start()
    {
        // enabledAbilities = new List<Ability>();
        StartCoroutine(nameof(StartDelayed));
    }

    IEnumerator StartDelayed() 
    {
        yield return new WaitForSeconds(0.6f);
        abiltyCount = abilities.Length;

        foreach (Ability ability in abilities)
        {
            if (!enabledAbilities.Contains(ability))
            {
                ability.disableAbility();
                ability.SetLevel(1);
            }
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
            enabledAbilities.Add(ability);
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

    public void LoadData(GameData data)
    {
        enabledAbilities = data.activeAbilities;
    }

    public void SaveData(GameData data)
    {
        data.activeAbilities = enabledAbilities;
    }
}
