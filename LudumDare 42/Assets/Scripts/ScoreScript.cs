using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    public float multiplicador;
    Text score;
    public bool gameover = false;
    public bool iniciarContador = false;
    float tiempoInicial, tiempoTranscurrido;
 

	// Use this for initialization
	void Start () {

        StartCoroutine("Inicio");
        score = GetComponent<Text>();
        score.text = "SCORE: 0";
        tiempoInicial = Time.time;
        

	}

    // Update is called once per frame
    void FixedUpdate() {

        tiempoTranscurrido = Time.time - tiempoInicial;
        /*if(iniciarContador == true && gameover == false)
        {
            multiplicador += multiplicador * Time.deltaTime;
            score.text = multiplicador.ToString();
        }

        if(gameover == true)
        {
            score.text = "GAME OVER, press R to Retry";
        }*/

        
	}

    IEnumerator Inicio()
    {
        yield return new WaitForSeconds(1f);
        iniciarContador = true;
        StartCoroutine("Score");
    }

    IEnumerator Score()
    {
        if (gameover == false)
            score.text = (tiempoTranscurrido * multiplicador).ToString("0");
        else
        {
            score.text = "GAME OVER, press R to Retry";
            tiempoTranscurrido = 0f;
        }


        yield return new WaitForSeconds(0.1f);s
        StartCoroutine("Score");      
    }
}
