using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

   
    public float xMargin = 1.5f;            // Distance in the x axis the player can move before the camera follows. 
    public float yMargin = 1.5f;            // Distance in the y axis the player can move before the camera follows.  
    public float xSmooth = 1.5f;            // How smoothly the camera catches up with its target movement in the x axis.  
    public float ySmooth = 1.5f;            // How smoothly the camera catches up with its target movement in the y axis.  
    private Vector2 maxXAndY;               // The maximum x and y coordinates the camera can have.
    private Vector2 minXAndY;               // The minimum x and y coordinates the camera can have. 
    public Transform player;                // Reference to the player's transform. 

    public float maxZoom = 0.4f;
    public float minZoom = 5.0f;
    public float currentZoom;
    public float smooth = 10f;
    public float zoomValue = 0.01f;
    public float playerHittedValue = 0.5f;
    public float enemyDestroyedValue = 1f;
    public static bool activeConstantZoom = true;
    public static bool enemyDestroyed = false;
    public static bool playerHitted = false;
    private Camera cam;

    void Awake()
    {
        // Setting up the reference.

        cam = (Camera)GetComponent(typeof(Camera));
        player = GameObject.Find("Player").transform;
        currentZoom = cam.orthographicSize;

        if (player == null)
        {
            Debug.LogError("Player object not found");
        }

        // Get the bounds for the background texture - world size    
        var backgroundBounds = GameObject.Find("Fondo").GetComponent<Renderer>().bounds;


        // Get the viewable bounds of the camera in world     
        // coordinates    
        var camTopLeft = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0, 0, 0));
        var camBottomRight = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1, 1, 0));

        // Automatically set the min and max values   
        minXAndY.x = backgroundBounds.min.x - camTopLeft.x;
        maxXAndY.x = backgroundBounds.max.x - camBottomRight.x;

    }

    void FixedUpdate()
    {

        followPlayer();

       if (currentZoom > maxZoom && activeConstantZoom == true)
            constantZoom();

        if (playerHitted == true)
            playerHittedZoom();

        if (enemyDestroyed == true)
            enemyDestroyedZoom();

    }


    void followPlayer() {

        // By default the target x and y coordinates of the camera are it's current x and y coordinates.            
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        // If the player has moved beyond the x margin...       
        if (CheckXMargin())

            // the target x coordinate should be a Lerp between           
            // the camera's current x position and the player's 
            // current x position.            

            targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.fixedDeltaTime);

        // If the player has moved beyond the y margin...        
        if (CheckYMargin())

            // the target y coordinate should be a Lerp between            
            // the camera's current y position and the player's            
            // current y position.            
            targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.fixedDeltaTime);

            // The target x and y coordinates should not be larger        
            // than the maximum or smaller than the minimum.        
            targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
            targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

        // Set the camera's position to the target position with        
        // the same z component.        
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
    
    void constantZoom()
    {

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cam.orthographicSize - zoomValue, Time.fixedDeltaTime *smooth);
        currentZoom = cam.orthographicSize;


    }

    public void playerHittedZoom()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cam.orthographicSize - playerHittedValue, Time.fixedDeltaTime * smooth);
        currentZoom = cam.orthographicSize;
        playerHitted = false;
        activeConstantZoom = true;

    }

    public void enemyDestroyedZoom()
    {
        if (currentZoom + enemyDestroyedValue >= minZoom )
        {

            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, minZoom , 1);
            currentZoom = cam.orthographicSize;
            enemyDestroyed = false;
            activeConstantZoom = true;

        }
        else
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cam.orthographicSize + enemyDestroyedValue, 1);
            currentZoom = cam.orthographicSize;
            enemyDestroyed = false;
            activeConstantZoom = true;
        }


    }

    bool CheckXMargin()
    {

        // Returns true if the distance between the camera and the  
        // player in the x axis is greater than the x margin.
        return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
    }

    bool CheckYMargin()
    {

        // Returns true if the distance between the camera and the  
        // player in the y axis is greater than the y margin.        
        return Mathf.Abs(transform.position.y - player.position.y) > yMargin;

    }

}
