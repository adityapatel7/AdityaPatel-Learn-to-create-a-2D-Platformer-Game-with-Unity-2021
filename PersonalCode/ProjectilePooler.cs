using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private LayerMask collideWith;    



    private Projectile _projectile;



    // Start is called before the first frame update
    void Start()
    {
        _projectile = GetComponent<Projectile>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollision();
    }


    private void CheckCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _projectile.ShootDirection , _projectile.Speed * Time.deltaTime + 0.1f , collideWith);
        if(hit)
        {

            gameObject.SetActive(false);

        }
    }
}
