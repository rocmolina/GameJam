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

    [Header("Para el random de la animacion")]
    public float timeForCalculation;
    public int timeMin, timeMax;

    public Animator animator;
    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine("RunOpenMouth");
    }


    public void DamageEnemy (int damage)
    {
        stats.Health -= damage;
        if(stats.Health <= 0)
        {
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
 

}
