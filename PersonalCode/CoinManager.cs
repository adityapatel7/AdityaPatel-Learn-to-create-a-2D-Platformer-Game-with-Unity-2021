using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : Singleton<CoinManager>
{
    public int TotalCoins { get; set; }
    private string COINS_KEY = "MyGame_Total_Coins";


    private void Start()
    {
        LoadCoins();
    }
    // private void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.P))
    //     {
    //         AddCoins(10);
    //     }
    // }
    private void LoadCoins()
    {
        TotalCoins = PlayerPrefs.GetInt(COINS_KEY,0);
    }
   public void AddCoins(int amount)
   {
        TotalCoins += amount;

        PlayerPrefs.SetInt(COINS_KEY,TotalCoins);
        PlayerPrefs.Save();
   }

    public void RemoveCoins(int amount)
    {
        TotalCoins -= amount;
        PlayerPrefs.SetInt(COINS_KEY,TotalCoins);
        PlayerPrefs.Save();
    }
}
