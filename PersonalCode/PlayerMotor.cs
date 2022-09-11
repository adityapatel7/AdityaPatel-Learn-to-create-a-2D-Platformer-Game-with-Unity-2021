using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    
    private PlayerStates[] _playerStates;


    void Start()
    {
        _playerStates = GetComponents<PlayerStates>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerStates.Length  != 0 )
        {
            foreach(PlayerStates state in _playerStates )
            {
                state.LocalInput();
                state.ExecuteState();
                state.SetAnimations();
            }
        }
    }

    public void SpawnPlayer(Transform newPosition)
    {
        transform.position = newPosition.position;
        
    }


}
