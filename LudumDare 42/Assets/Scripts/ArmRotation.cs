using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour {

    public PlayerMovement playerMovement;
    public Weapon weapon;

    public int rotationoffset = 90;
    Quaternion initialRotation = new Quaternion();

    void Start()
    {
        initialRotation = transform.rotation;
    }
    void Update() {

        rotation();
    }

    void rotation()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;     //subtracting the position of the player form the mouse position
            difference.Normalize();      //Normalizing the vector. This Mean that all the sum of the vector will be equal to 1

            if(playerMovement.flipPlayer == false)
            {
                difference = -(difference);
            }

            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;  //find the angle in degrees
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationoffset);

            weapon.rotationOffset = (180 - transform.rotation.z) - transform.rotation.z; 
        }

        else
        {
            transform.rotation = initialRotation;
        }
    }
}
