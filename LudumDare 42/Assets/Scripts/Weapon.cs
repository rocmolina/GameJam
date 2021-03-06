﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public PlayerMovement playerMovement;
    public AudioSource audioSource;

    public float fireRate = 0;
    public int Damage = 10;
    public LayerMask whatToHit;
    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;

    public float rotationOffset = 0f;
    public Transform BulletTrailPrefab;

    float timeToFire = 0;
    Transform firePoint;
    public Transform PartToRotate;

	// Use this for initialization
	void Awake () {

        firePoint = transform.Find("FirePoint"); 
        if(firePoint == null)
        {
            Debug.LogError("No firePoint? WHAT?!");

        }

             
	}
	
	// Update is called once per frame
	void Update () {
        
        if(fireRate == 0)
        {
            if(Input.GetKey(KeyCode.Mouse0))
            {
                Shoot();
            }

        }
        else
        {
            if(Input.GetKey(KeyCode.Mouse0) && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
		
	}

    void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x,firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition-firePointPosition, 50, whatToHit);

        if(Time.time >= timeToSpawnEffect)
        {
            Effect();
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
        
        Debug.DrawLine(firePointPosition,(mousePosition-firePointPosition)*100,Color.green);
        if(hit.collider != null)
        {
            Debug.DrawLine(firePointPosition,hit.point,Color.red);            
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.DamageEnemy(Damage);
                Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage.");
            }
        }

        audioSource.Play();
    }

    void Effect()
    {
        Debug.Log("Rotacion Firepoint:" + firePoint.rotation.ToString());

        if (playerMovement.flipPlayer == true)
            Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
        else
        {
            Quaternion newRotation = firePoint.rotation;
            newRotation.z += rotationOffset;
            //newRotation.z += rotationOffset;
            //newRotation.z *= -1;
            //newRotation.w *= -1;
            newRotation.Normalize();
            Debug.Log("New Rotation:" + newRotation.ToString());
            Instantiate(BulletTrailPrefab, firePoint.position,newRotation);
        }

    }

    public static void KillEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
        CameraControl.enemyDestroyed = true;
        CameraControl.activeConstantZoom = false;

    }
}
