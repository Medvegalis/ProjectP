using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Net.NetworkInformation;
using UnityEngine.UI;
using System.Linq;

public class AbilitiesManagerUI : MonoBehaviour
{
    public List<Ability> abilities = new List<Ability>();
    public List<TextMeshProUGUI> names = new List<TextMeshProUGUI>();   
    public List<TextMeshProUGUI> description = new List<TextMeshProUGUI>();
    public List<string> debug = new List<string>();

    private List<Ability> abilitiesToAdd = new List<Ability>();
    public AbilityControler abilityControler;


    public void OnEnable()
    {

        for(int i = 0; i < 3; i++)
        {
            names[i].text = "Button";
            description[i].text = "desc";
        }

        LoadAbilities();
    }


    private List<Ability> GetDropppedAbility()
    {
        int randomNum = Random.Range(1, 101); //1-100
        List<Ability> possibleAbilities = new List<Ability>();

        foreach (Ability ability in abilities)
        {
            if (randomNum <= ability.rarity)
            {
                possibleAbilities.Add(ability);
            }
        }

        if (possibleAbilities.Count > 0)
        {
            List<Ability> dropppedability = new List<Ability>();
            HashSet<Ability> set = new HashSet<Ability>();

            while(set.Count != 3)
            {
                Ability toAdd = possibleAbilities[Random.Range(0, possibleAbilities.Count)];

                set.Add(toAdd);
            }
            

            return set.ToList<Ability>();
        }

        return null;
    }

    public void LoadAbilities()
    {
        abilitiesToAdd = GetDropppedAbility();

        for (int i = 0; i < abilitiesToAdd.Count; i++)
        {
            names[i].text = abilitiesToAdd[i].name;
            description[i].text = abilitiesToAdd[i].explanation;
        }
    }

    public void PickAbility(int buttonIndex)
    {
        abilityControler.enableAbility(abilitiesToAdd[buttonIndex].GetId());
    }
}
