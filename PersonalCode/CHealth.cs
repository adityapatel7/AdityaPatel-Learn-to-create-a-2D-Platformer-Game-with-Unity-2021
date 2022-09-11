using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHealth : Collectable
{
    protected override void Collect()
    {
        AddLife();
    }
    private void AddLife()
    {
        if(_playerMotor.GetComponent<PlayerHealth>() == null)
        {
            return; 
        }
        PlayerHealth playerHealth = _playerMotor.GetComponent<PlayerHealth>();

        if(playerHealth.CurrentLives < playerHealth.MaxLives)
        {
            playerHealth.AddLife();
        }
    }
}
