using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab; // Collectable item prefab
    public List<Loot> lootList = new List<Loot>(); // A list of droppable loot

    /// <summary>
    /// Returns one loot from the lootList list.
    /// Draws a random number then checks if the drop chances is bigger 
    /// adds it to the possibleLoot list. Then one item from the possibleLoot list is chosen by random.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Creates the gameObject for the loot object
    /// </summary>
    /// <param name="spanwPoint"></param>
    public void InstantiateLoot(Vector3 spanwPoint)
    {
        Loot droppedItem = GetDropppedItem();
        if(droppedItem != null)
        {
            GameObject lootGameObject = Instantiate(droppedItemPrefab, spanwPoint, Quaternion.identity);
            lootGameObject.name = droppedItem.lootName;
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;
        }
    }
}
