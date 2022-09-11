using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
   [Header("Settings")]
    [SerializeField] private Transform levelStartPoint;
    [SerializeField] private GameObject PlayerPrefab;


    public static Action<PlayerMotor> OnPlayerSpawn;
    private PlayerMotor _currentPlayer;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>

    private void Start()
    {
        SpawnPlayer(PlayerPrefab);
    }

    // private void Awake()
    // {
    //     SpawnPlayer(PlayerPrefab);
    // }

    private void Update()
    {
         if(Input.GetKeyDown(KeyCode.R))
        {
            RevivePlayer();
        }
    }
    
    private void SpawnPlayer(GameObject PlayerPrefab)
    {
        if(PlayerPrefab != null)
        {
            _currentPlayer = Instantiate(PlayerPrefab , levelStartPoint.position ,Quaternion.identity).GetComponent<PlayerMotor>();

            _currentPlayer.GetComponent<PlayerHealth>().ResetLife();

            OnPlayerSpawn?.Invoke(_currentPlayer);
        }
    }



    private void RevivePlayer()
    {
        if(_currentPlayer != null)
        {
            _currentPlayer.gameObject.SetActive(true);
            _currentPlayer.SpawnPlayer(levelStartPoint);
            _currentPlayer.GetComponent<PlayerHealth>().ResetLife();

            _currentPlayer.GetComponent<PlayerHealth>().Revive();
        }
        
        
    }
    private void PlayerDeath(PlayerMotor player)
    {
        // _currentPlayer = player;
        player.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        PlayerHealth.OnDeath += PlayerDeath;
    }
    
    private void OnDisable()
    {
        PlayerHealth.OnDeath -= PlayerDeath;
    }





}
