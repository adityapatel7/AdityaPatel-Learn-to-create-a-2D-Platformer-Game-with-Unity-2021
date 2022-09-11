using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;


    [Header("Gun Settings")]
    [SerializeField] private float msBetweenShots = 1000;

    public GunController gunController { get; set; }

    private float _nextShotTime;

    private ObjectPooler _pooler;



    // Start is called before the first frame update
    void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
        // gunController = GetComponent<GunController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FireProjectile()
    {
        GameObject newProjectile = _pooler.GetObjectToPool();
        newProjectile.transform.position = firePoint.position;
        newProjectile.SetActive(true);

        Projectile projectile = newProjectile.GetComponent<Projectile>();
        projectile.SetDirection(gunController.PlayerController.FacingRight? Vector3.right : Vector3.left);
        projectile.GunEquipped = this;
    }

    public void Shoot()
    {
        if (Time.time > _nextShotTime)
        {
            _nextShotTime = Time.time + msBetweenShots / 1000f;
            //Projectile projectile = Instantiate(projectilePrefab , firePoint.position , Quaternion.identity);
            //projectile.GunEquipped = this;



            Debug.Log("Shots Fired!");
            FireProjectile();
            //_projectilesRamaining--;
            
            //SoundManager.Instance.PlaySound(AudioLibrary.Instance.ProjectileClip);
        }
    }
}
