using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounterUpdate : MonoBehaviour
{
    private TextMeshProUGUI coinCounter; 
    private int currentCount = 0;
    void Awake()
    {
        coinCounter = GetComponent<TextMeshProUGUI>();
        Coin.OnCoinCollected += AddToCounter;
    }

    public void AddToCounter()
    {
        currentCount++;
        coinCounter.text = currentCount.ToString();
    }
}
