using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    protected PlayerMotor _playerMotor;
    protected SpriteRenderer _spriteRenderer;
    protected Collider2D _collider2D;


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
    }

    private bool CanBePicked()
    {
        return _playerMotor != null;
    }

    private void CollectLogic()
    {
        if(!CanBePicked())
        {
            return;
        }
        Collect();
        DisableCollectable();
    }

    protected virtual void Collect()
    {
        Debug.Log("Collect Logic is Working!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerMotor>() != null)
        {
            _playerMotor = other.GetComponent<PlayerMotor>();
            CollectLogic();
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        _playerMotor = null;
    }


    private void DisableCollectable()
    {
        _collider2D.enabled = false;
        _spriteRenderer.enabled = false;
    }
}
