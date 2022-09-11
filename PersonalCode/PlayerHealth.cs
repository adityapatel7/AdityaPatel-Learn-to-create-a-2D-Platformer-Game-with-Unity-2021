using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int lives = 3;

    private int _maxLives;
    private int _currentLives;

    public int MaxLives => _maxLives;

    public int CurrentLives => _currentLives;

    public static Action<int> OnLivesChanged;

    public static Action<PlayerMotor> OnRevive;
    public static Action<PlayerMotor> OnDeath;

    private void Start()
    {
        ResetLife();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            LoseLife();
        }

        // if(Input.GetKeyDown(KeyCode.R))
        // {
        //     AddLife();
        // }
    }

    private void Awake()
    {
        _maxLives = lives;
    }

    public void AddLife()
    {
        
        _currentLives +=1;
        _maxLives = lives;
        if(_currentLives  > _maxLives)
        {
            _currentLives = _maxLives;
        }
        UpdateLifeUI();
    }
    public void LoseLife()
    {
        _currentLives -= 1;
        if(_currentLives <= 0)
        {
            _currentLives = 0;

            OnDeath?.Invoke(gameObject.GetComponent<PlayerMotor>());
        }
        UpdateLifeUI();

    }
    public void ResetLife()
    {
        _currentLives = lives;
        UpdateLifeUI();
    }

    public void Revive()
    {
        OnRevive?.Invoke(gameObject.GetComponent<PlayerMotor>());

    }

    public void KillPlayer()
    {
        _currentLives = 0;
        UpdateLifeUI();
        OnDeath?.Invoke(gameObject.GetComponent<PlayerMotor>());
    }

    private void UpdateLifeUI()
    {
        OnLivesChanged?.Invoke(_currentLives);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().Damage(gameObject.GetComponent<PlayerMotor>());
        }
    }

}
