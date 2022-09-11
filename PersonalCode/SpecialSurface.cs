using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSurface : MonoBehaviour
{

    [Header("Settings")] 
    [SerializeField] private float friction = 0.1f;

    [Header("Movement")] 
    [SerializeField] private float horizontalMovement = 4f;

    /// <summary>
    /// 
    /// </summary>
    public float Friction => friction;

    private PlayerController _playerController;

    private void Update()
    {
        if (_playerController == null)
        {
            return;
        }
        
        _playerController.AddHorizontalMovement(horizontalMovement);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            _playerController = other.gameObject.GetComponent<PlayerController>();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _playerController = null;
    }  


    // [Header("Settings")]
    
    // [SerializeField] private float friction = 0.1f;
    
    // [Header("Movement")]
    // [SerializeField] private float horizontalMovement = 5f;

    // public float Friction => friction;
    // private PlayerController _playerController;


    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(other.gameObject.GetComponent<PlayerController>() != null)
    //     {
    //         _playerController = other.gameObject.GetComponent<PlayerController>();
    //     }
    // }

    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     _playerController = null;
    // }



    // private void Update()
    // {
    //     if(_playerController  == null)
    //     {
    //         return;
    //     }
    //     _playerController.AddHorizontalMovement(horizontalMovement);  
    // }

}
