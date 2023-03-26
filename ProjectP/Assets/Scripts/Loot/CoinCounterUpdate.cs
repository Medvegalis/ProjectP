using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Updates the UI coin counter
/// </summary>
public class CoinCounterUpdate : MonoBehaviour, IDataPersistence
{
    private TextMeshProUGUI coinCounter; 
    private int currentCount = 0;

    void Start()
    {
        coinCounter = GetComponent<TextMeshProUGUI>();
        coinCounter.text = currentCount.ToString();
        Coin.OnCollected += AddToCounter;
    }
    private void AddToCounter(int value)
    {
        currentCount += value;
        coinCounter.text = currentCount.ToString();
    }

    public void LoadData(GameData data)
    {
        this.currentCount = data.coinCount;
    }

    public void SaveData(GameData data)
    {
        data.coinCount = this.currentCount;
    }
}
