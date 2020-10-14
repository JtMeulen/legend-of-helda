using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinTextManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Text coinText;

    private void Start()
    {
        coinText.text = inventory.coins.ToString("000");
    }

    public void UpdateCoinCount()
    {
        coinText.text = inventory.coins.ToString("000");
    }
}
