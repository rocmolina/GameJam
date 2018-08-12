using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody2D playerRB;
    public float maxSpeed;
    Animator playerAnim;
    //Flip the player
    bool flipPlayer = true;
    public SpriteRenderer brazo, mano, hombro;
    public SpriteRenderer[] playerRender = new SpriteRenderer[6];

	// Use this for initialization
	void Start () {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponentInChildren<Animator>();		
	}
	
	// Update is called once per frames
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
        playerAnim.SetFloat("Speed", Mathf.Abs(movement));
     
    }
    void Flip()
    {
		
        flipPlayer = !flipPlayer;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

        
    }
}
