using UnityEngine;

public class LevelComponent : MonoBehaviour, IDamageable
{
    [Header("Settings")] 
    [SerializeField] protected bool instantKill;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="player"></param>
    public virtual void Damage(PlayerMotor player)
    {
        if (player != null)
        {
            if (instantKill)
            {
                player.GetComponent<Health>().KillPlayer();
            }
            else
            {
                player.GetComponent<Health>().LoseLife();
            }
        }
    }
}