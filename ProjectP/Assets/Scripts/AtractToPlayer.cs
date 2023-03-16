using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AtractToPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinCounter;
    private int coinCount = 0;
  void OnTriggerStay2D (Collider2D coll)
  {
        if(coll.CompareTag("Player"))
        {
            transform.position = Vector3.MoveTowards(transform.position, coll.transform.position, 1f);
            AddCoinCoin();
        }

        
  }

  private void AddCoinCoin()
  {
    coinCount++;
    coinCounter.text = coinCount.ToString();
  }
}
