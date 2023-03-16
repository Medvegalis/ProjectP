using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<Loot> lootList = new List<Loot>();

    Loot GetDropppedItem()
    {
        int randomNum = Random.Range(1, 101); //1-100
        List<Loot> possibleLoot = new List<Loot>();

        foreach(Loot item in lootList)
        {
            if(randomNum <= item.dropChance)
            {
                possibleLoot.Add(item);
            }
        }

        if(possibleLoot.Count > 0)
        {
            Loot dropppedItem = possibleLoot[Random.Range(0, possibleLoot.Count)];
            return dropppedItem;
        }
        
        return null;
    }

    public void InstantiateLoot(Vector3 spanwPoint)
    {
        Loot droppedItem = GetDropppedItem();
        if(droppedItem != null)
        {
            GameObject lootGameObject = Instantiate(droppedItemPrefab, spanwPoint, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;
        }
    }
}
