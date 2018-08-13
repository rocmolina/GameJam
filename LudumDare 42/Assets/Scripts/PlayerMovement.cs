using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody2D playerRB;
    public float maxSpeed;
    Animator playerAnim;
    //Flip the player
    public bool flipPlayer = true;
    public SpriteRenderer brazo, mano, hombro;
    public SpriteRenderer[] playerRender = new SpriteRenderer[6];

    bool canMove = true;
    //Jump
    bool onFLoor = false;
    float checkFloorRadius = 0.2f;
    public LayerMask floorMask;
    public Transform checkFloor;
    public float jumpPower;

	// Use this for initialization
	void Start () {

        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponentInChildren<Animator>();		
	}
	
	// Update is called once per frames
	void Update () {

        if (canMove && onFLoor && Input.GetAxis("Jump") > 0)
        {
            playerAnim.SetBool("InFloor",false);
            playerRB.velocity = new Vector2(playerRB.velocity.x,0f);
            playerRB.AddForce(new Vector2 (0,jumpPower),ForceMode2D.Impulse);
            onFLoor = false;
        }

        onFLoor = Physics2D.OverlapCircle(checkFloor.position, checkFloorRadius, floorMask);
        playerAnim.SetBool("InFloor",onFLoor);

        float movement = Input.GetAxis("Horizontal");

        if (canMove)
        {
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
        else
        {
            playerRB.velocity = new Vector2(0,playerRB.velocity.y);

           
            playerAnim.SetFloat("Speed", 0);
        }           

       
     
    }
    void Flip()
    {
		
        flipPlayer = !flipPlayer;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

        
    }

    
}
