using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private float speed = 50f;

    /// <summary>
    /// Reference of the Gun owner of this projectile
    /// </summary>
    public Gun GunEquipped { get; set; }
    
    /// <summary>
    /// Returns the shoot direction
    /// </summary>
    public Vector3 ShootDirection => _shootDirection;
    
    /// <summary>
    /// Controls the speed of this projectile
    /// </summary>
    public float Speed =>speed;
    
    private Vector3 _shootDirection;


    private void Start()
    {
        _shootDirection = GunEquipped.gunController.PlayerController.FacingRight ? Vector3.right:Vector3.left;
    }

    private void Awake()
    {
        //Speed = speed;
    }

    private void Update() 
    {
        transform.Translate(_shootDirection * Speed * Time.deltaTime);
    }
    
    /// <summary>
    /// Set the projectile direction
    /// </summary>
    /// <param name="newDirection"></param>
    public void SetDirection(Vector3 newDirection)
    {
        _shootDirection = newDirection;
    }

    /// <summary>
    /// Enables the projectile speed
    /// </summary>
    public void EnableProjectile()
    {
        //Speed = speed;
    }

    /// <summary>
    /// Disbale the projectile speed
    /// </summary>
    public void DisableProjectile()
    {
        //Speed = 0f;
    }
}
