using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_seguimiento : MonoBehaviour {

    public float visionRadius;
    public float speed;

    //variable para guardar al jugador
    GameObject player;

    //variable para guardar la ubicacion inicial
    Vector3 initialPosition;

	// Use this for initialization
	void Start () {
        //recuperamos al jugador por el tag
        player = GameObject.FindGameObjectWithTag("Player");

        //Guardamos nuestra posicion inicial
        initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //por defecto nuestro objetivo siempre sera nuestra posicion inicial
        Vector3 target = initialPosition;

        //pero si la distancia hasta el jugador es menor que el radio de vision el objetivo sera el
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < visionRadius) target = player.transform.position;

        //Finalmente movemos al enemigo en direccion a su target
        float fixedSpeed = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position,target,fixedSpeed);

        //y podemos debugearlo con una linea
        Debug.DrawLine(transform.position, target, Color.green);		
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        
    }

}
