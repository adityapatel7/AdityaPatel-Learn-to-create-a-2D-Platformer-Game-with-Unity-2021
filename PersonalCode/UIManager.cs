using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{

    [Header("Settings")]
    [SerializeField] private Image fuelImage;

    [SerializeField] private GameObject[] playerLives;


    [Header("Coins")]
    [SerializeField] private TextMeshProUGUI coinTMP;


    private float _currentJetpackFuel;
    private float _jetpackFuel; 

   

    // Update is called once per frame
    void Update()
    {
        InternalJetpackUpdate(); 
        UpdateCoins();
    }


    public void UpdateFuel(float currentFuel, float maxFuel)
    {
        _currentJetpackFuel = currentFuel;
        _jetpackFuel = maxFuel; 
    }

    private void InternalJetpackUpdate()
    {
        fuelImage.fillAmount = Mathf.Lerp(fuelImage.fillAmount , _currentJetpackFuel/_jetpackFuel , Time.deltaTime * 10f);
    }

    private void OnPlayerLives(int currentLives)
    {
        for (int i = 0; i < playerLives.Length; i++)
        {
            if(i < currentLives)
            {
                playerLives[i].gameObject.SetActive(true);
            }
            else
            {
                playerLives[i].gameObject.SetActive(false);
            }
        }
    }

    private void UpdateCoins()
    {
        coinTMP.text = CoinManager.Instance.TotalCoins.ToString();
    }






    void OnEnable()
    {
        PlayerHealth.OnLivesChanged += OnPlayerLives;
    }

    void OnDisable()
    {
        PlayerHealth.OnLivesChanged -= OnPlayerLives;
    }




}
