using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody2D playerRB;
    public float maxSpeed;
    //Animator playerAnim;

    //Flip the player
    bool flipPlayer = true;
    SpriteRenderer playerRender;

	// Use this for initialization
	void Start () {
        playerRB = GetComponent<Rigidbody2D>();
        playerRender = GetComponent<SpriteRenderer>();
        //playerAnim = GetComponent<Animator>();		
	}
	
	// Update is called once per frame
	void Update () {

        float movement = Input.GetAxis("Horizontal");

        if (movement > 0 && !flipPlayer)
        {
            Flip();
        }
        else if (movement < 0 && flipPlayer)
        {
            Flip();
        }


        playerRB.velocity = new Vector2(movement * maxSpeed, playerRB.velocity.y);

        //Player  Running
        //playerAnim.SetFloat("SpeedMovement", Mathf.Abs(movement));
     
    }
    void Flip()
    {
        flipPlayer = !flipPlayer;
        playerRender.flipX = !playerRender.flipX; 
    }
}
