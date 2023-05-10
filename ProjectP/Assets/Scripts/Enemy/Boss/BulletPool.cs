using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{

    public static BulletPool instance;

    [SerializeField] private GameObject pooledBullet;
    private bool notEnoughInPool = true;

    private List<GameObject> bulletList;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        bulletList = new List<GameObject>();
    }

    public GameObject GetBullet() 
    {
        if(bulletList.Count > 0)
        {
            for(int i = 0; i < bulletList.Count; i++)
            {
                if (!bulletList[i].activeInHierarchy)
                {
                    return bulletList[i];
                }
            }
        }

        if(notEnoughInPool)
        {
            GameObject bul = Instantiate(pooledBullet);
            bul.SetActive(false);
            bulletList.Add(bul);
            return bul;
        }

        return null;
    }
}
