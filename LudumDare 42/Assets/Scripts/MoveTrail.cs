using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour {

    public int moveSpeed = 230;
    
    GameObject player;
    PlayerMovement playerMovement;	

    void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

	void Update () {

        //if (playerMovement.flipPlayer == true)
           //transform.Translate(Vector3.right * Time.deltaTime * moveSpeed,Space.Self);

        /*else
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.Self);
        }
       

        this.enabled = false;*/
        //Destroy(gameObject,0.25f);

	}
}
