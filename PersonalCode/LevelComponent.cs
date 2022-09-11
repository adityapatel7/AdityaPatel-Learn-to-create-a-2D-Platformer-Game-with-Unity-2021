using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComponent : MonoBehaviour , IDamageable
{
    [Header("Settings")]
    [SerializeField] protected bool instantKill;
    public virtual void Damage(PlayerMotor player)
    {
        if(player != null)
        {
            if(instantKill)
            {
                //kill method
                player.GetComponent<PlayerHealth>().KillPlayer();
            }
            else 
            {
                // damage
                player.GetComponent<PlayerHealth>().LoseLife();
            }
        }
    }
}
