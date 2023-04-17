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
    List <Loot> GetDropppedItem()
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
            List<Loot> dropppedItem = new List<Loot>();

            for (int i = 0; i < Random.Range(0f, 2f); i++)
                dropppedItem.Add( possibleLoot[Random.Range(0, possibleLoot.Count)]);

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
        List<Loot> droppedItems = GetDropppedItem();
        float dropforce = 100f;
        if (droppedItems != null)
        {
            foreach(Loot loot in droppedItems)
            {
                GameObject lootGameObject = Instantiate(droppedItemPrefab, spanwPoint, Quaternion.identity);
                lootGameObject.name = loot.lootName;
                lootGameObject.GetComponent<SpriteRenderer>().sprite = loot.lootSprite;
                lootGameObject.GetComponent<Collectable>().value = loot.value;
                lootGameObject.GetComponent<Collectable>().lootType = loot.lootType;

                Vector2 dropDirection = new Vector2(Random.Range(-1f,1f), Random.Range(-1f, 1f));
                lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropforce, ForceMode2D.Impulse);
            }
        }
    }
}
