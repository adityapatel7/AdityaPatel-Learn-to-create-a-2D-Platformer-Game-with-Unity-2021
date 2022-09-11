using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Jumper : MonoBehaviour
{
   public static Action<float> OnJump;
    
    [Header("Settings")]
    [SerializeField] private float jumpHeight = 5f;

    private Animator _animator;
    private int _jumperParameter = Animator.StringToHash("Jumper");
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Jump Trigger !");
        if (other.gameObject.GetComponent<PlayerJump>() != null)
        {
            OnJump?.Invoke(jumpHeight);
            _animator.SetTrigger(_jumperParameter);
        }
    }
}
