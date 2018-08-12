using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour {

    public int rotationoffset = 90;
	// Update is called once per frame
	void Update () {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;     //subtracting the position of the player form the mouse position
        difference.Normalize();      //Normalizing the vector. This Mean that all the sum of the vector will be equal to 1

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;  //find the angle in degrees
        transform.rotation = Quaternion.Euler(0f,0f,rotZ + rotationoffset);

	}
}
