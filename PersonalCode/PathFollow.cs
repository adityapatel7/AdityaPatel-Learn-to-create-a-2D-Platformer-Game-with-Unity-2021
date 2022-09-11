using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour
{

    public enum MoveDirection
    {
        LEFT , RIGHT
    }


    [Header("Settings")]
    [SerializeField] private float moveSpeed = 6; 
    [SerializeField] private float minDistanceToPoint = 0.1f;


    public MoveDirection Direction { get; set; }



    public float MoveSpeed => moveSpeed;

    private bool _playing ;
  
    public List<Vector3> points = new List<Vector3>();

    private bool _moved;

    private int _currentPoint = 0;


    private Vector3 _currentPosition;
    private Vector3 _previousPosition;

    public void Start()
    {
        _playing = true;
        _previousPosition = transform.position;
        _currentPosition = transform.position;
        transform.position = _currentPosition + points[0];
    }

    public void Update()
    {
        Move();
    }

    public void Move()
    {
        //Set first Position
        if(! _moved)
        {
            transform.position =_currentPosition + points[0];
            _currentPoint++;
            _moved=true;
        }

        //Move To Next Point
        transform.position = Vector3.MoveTowards(transform.position , _currentPosition + points[_currentPoint] , Time.deltaTime * moveSpeed);

        // Move to next point 

        float distanceToNextPoint = Vector3.Distance(_currentPosition + points[_currentPoint], transform.position);

        if(distanceToNextPoint < minDistanceToPoint)
        {
            _previousPosition = transform.position;
            _currentPoint++;
        }

        // Get MoveDirection

        if(_previousPosition  != Vector3.zero)
        {
            if(transform.position.x > _previousPosition.x)
            {
                Direction = MoveDirection.RIGHT;
            }
            else if(transform.position.x < _previousPosition.x)
            {
                Direction = MoveDirection.LEFT;
            }
        }

        // Last point check condition
        if(_currentPoint  == points.Count)
        {
            _currentPoint = 0;
        }

    }


    public void OnDrawGizmos()
    {
        if(transform.hasChanged && !_playing)
        {
            _currentPosition = transform.position;
        }

        if(points != null)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if(i<points.Count)
                {
                    //Draw Points
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(_currentPosition + points[i] , 0.4f);

                    // Draw Lines


                    Gizmos.color = Color.black;
                    if(i<points.Count -1)
                    {
                        Gizmos.DrawLine(_currentPosition + points[i],_currentPosition + points[i+1]);
                    }
                    // Draw Line from Last point to first Point

                    if(i == points.Count -1 )
                    {
                        Gizmos.DrawLine(_currentPosition + points[i] , _currentPosition + points[0]);
                    }
                }
            }
        }
    }

}
