using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [System.Serializable]
    public class EnemyStats
    {
        public int Health = 100;
    }

    [Header("Enemy Stats")]
    public EnemyStats stats = new EnemyStats();

    [Header("Animacion")]
    public float timeForCalculation;
    public int timeMin, timeMax;
    public SpriteRenderer boca;
    public Sprite bocaAbierta;
    public GameObject alienGraphics;
    public ParticleSystem particulas;

    [Header("Audio")]
    public Animator animator;
    public AudioSource audioSource;    
    public AudioClip Hablado, Muerte;

    void Start()
    {
        StartCoroutine("RunOpenMouth");
    }

    void FixedUpdate()
    {
        
    }

    public void DamageEnemy (int damage)
    {
        stats.Health -= 10;
        if(stats.Health <= 0)
        {
            StopAllCoroutines();
            StartCoroutine("Death");
            Weapon.KillEnemy(this);
        }

    }

    IEnumerator RunOpenMouth()
    {
        timeForCalculation = Random.Range(timeMin, timeMax);
        yield return new WaitForSeconds(timeForCalculation);
        animator.SetBool("AbrirBoca", true);
        yield return new WaitForSeconds(0.3f);
        audioSource.Play();
        animator.SetBool("AbrirBoca", false);
        StartCoroutine("RunOpenMouth");
    }

    IEnumerator Death()
    {
        boca.sprite = bocaAbierta;
        audioSource.clip = Muerte;
        audioSource.Play();
        alienGraphics.SetActive(false);
        particulas.Play();
        yield return new WaitForSeconds(0.5f);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            CameraControl.playerHitted = true;

        }
    }


}
