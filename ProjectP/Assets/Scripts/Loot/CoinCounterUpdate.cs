using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Updates the UI coin counter
/// </summary>
public class CoinCounterUpdate : MonoBehaviour
{
    private TextMeshProUGUI coinCounter; 
    private int currentCount = 0;
    void Start()
    {
        coinCounter = GetComponent<TextMeshProUGUI>();
        Coin.OnCollected += AddToCounter;
    }
    private void AddToCounter(int value)
    {
        currentCount += value;
        coinCounter.text = currentCount.ToString();
    }
}
