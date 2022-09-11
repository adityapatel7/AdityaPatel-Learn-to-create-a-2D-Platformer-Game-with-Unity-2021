using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Camera2D : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool horizontalFollow = true;
    [SerializeField] private bool verticalFollow = true;

    [Header("Horizontal")] 
    [SerializeField] [Range(0, 1)] private float horizontalInfluence = 1f;
    [SerializeField] private float horizontalOffset = 0f;
    [SerializeField] private float horizontalSmoothness = 3f;
    
    [Header("Vertical")] 
    [SerializeField] [Range(0, 1)] private float verticalInfluence = 1f;
    [SerializeField] private float verticalOffset = 0f;
    [SerializeField] private float verticalSmoothness = 3f;

    /// <summary>
    /// The target reference
    /// </summary>
    public PlayerMotor Target { get; set; }
    
    /// <summary>
    /// Position of the Target
    /// </summary>
    public Vector3 TargetPosition { get; set; }
    
    /// <summary>
    /// Reference of the Target Position known by the Camera
    /// </summary>
    public Vector3 CameraTargetPosition { get; set; }

    private float _targetHorizontalSmoothFollow;
    private float _targetVerticalSmoothFollow;
    
    private void Update()
    {
        MoveCamera();
    }

    /// <summary>
    /// Moves our Camera
    /// </summary>
    private void MoveCamera()
    {
        if (Target == null)
        {
            return;
        }
        
        // Calculate Position
        TargetPosition = GetTargetPosition(Target);
        CameraTargetPosition = new Vector3(TargetPosition.x, TargetPosition.y, 0f);
        
        // Follow on selected axis
        float xPos = horizontalFollow ? CameraTargetPosition.x : transform.localPosition.x;
        float yPos = verticalFollow ? CameraTargetPosition.y : transform.localPosition.y;
        
        // Set offset
        CameraTargetPosition += new Vector3(horizontalFollow ? horizontalOffset : 0f, verticalFollow ? verticalOffset : 0f, 0f);
        
        // Set smooth value
        _targetHorizontalSmoothFollow = Mathf.Lerp(_targetHorizontalSmoothFollow, CameraTargetPosition.x,
            horizontalSmoothness * Time.deltaTime);
        _targetVerticalSmoothFollow = Mathf.Lerp(_targetVerticalSmoothFollow, CameraTargetPosition.y,
            verticalSmoothness * Time.deltaTime);
        
        // Get direction towards target pos
        float xDirection = _targetHorizontalSmoothFollow - transform.localPosition.x;
        float yDirection = _targetVerticalSmoothFollow - transform.localPosition.y;
        Vector3 deltaDirection = new Vector3(xDirection, yDirection, 0f);
        
        // New position
        Vector3 newCameraPosition = transform.localPosition + deltaDirection;
        
        // Apply new position
        transform.localPosition = new Vector3(newCameraPosition.x, newCameraPosition.y, transform.localPosition.z);
    }
    
    /// <summary>
    /// Returns the position of out target
    /// </summary>
    /// <param name="player">Player reference</param>
    /// <returns></returns>
    private Vector3 GetTargetPosition(PlayerMotor player)
    {
        float xPos = 0f;
        float yPos = 0f;

        xPos += (player.transform.position.x + horizontalOffset) * horizontalInfluence;
        yPos += (player.transform.position.y + verticalOffset) * verticalInfluence;
        
        Vector3 positionTarget = new Vector3(xPos, yPos, transform.position.z);
        return positionTarget;
    }

    /// <summary>
    /// Centers our camera in the target position
    /// </summary>
    /// <param name="player">Player position</param>
    private void CenterOnTarget(PlayerMotor player)
    {
        Target = player;

        Vector3 targetPos = GetTargetPosition(Target);
        _targetHorizontalSmoothFollow = targetPos.x;
        _targetVerticalSmoothFollow = targetPos.y;
        
        transform.localPosition = targetPos;
    }

    /// <summary>
    /// Reset the target reference
    /// </summary>
    /// <param name="player"></param>
    private void StopFollow(PlayerMotor player)
    {
        Target = null;
    }

    /// <summary>
    /// Gets Target reference and center our camera
    /// </summary>
    /// <param name="player"></param>
    private void StartFollowing(PlayerMotor player)
    {
        Target = player;
        CenterOnTarget(Target);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 camPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 2f);
        Gizmos.DrawWireSphere(camPosition, 0.5f);
    }

    private void OnEnable()
    {
        LevelManager.OnPlayerSpawn += CenterOnTarget;
        PlayerHealth.OnDeath += StopFollow;
        PlayerHealth.OnRevive += StartFollowing;
    }

    private void OnDisable()
    {
        LevelManager.OnPlayerSpawn -= CenterOnTarget;
        PlayerHealth.OnDeath -= StopFollow;
        PlayerHealth.OnRevive -= StartFollowing;
    }
}
