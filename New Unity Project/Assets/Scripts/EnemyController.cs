using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Pathfinding;

public class EnemyController : MonoBehaviour {

    public enum enemyState { CHASING, FURIOUS, ATTACKING, STUN, DYING }

    private Rigidbody2D enemyRb;

    public int enemyDamage;
    public int enemyAttackSpeed;
    private int enemyStamina = 100;
    private AIPath aIPath;
    public enemyState currentState = enemyState.CHASING;
    private float timeChasing = 0f;

    private float initialSpeed;
    private bool hitPlayer;
    private float attackAnimTime = 0.5f;
    private bool attackAnimEnded;


    private void Start()
    {
        aIPath = GetComponent<AIPath>();
        enemyRb = GetComponent<Rigidbody2D>();
        initialSpeed = aIPath.maxSpeed;
    }
    private void Update()
    {
        if (enemyStamina <= 0)
        {
            currentState = enemyState.DYING;
        }
        switch (currentState)
        {
            case enemyState.CHASING:
                timeChasing += Time.deltaTime;
                if(timeChasing > 10)
                {
                    currentState = enemyState.FURIOUS;
                    aIPath.maxSpeed *= 2f;
                }
                break;
            case enemyState.FURIOUS:
                if (aIPath.remainingDistance < 4)
                {
                    currentState = enemyState.ATTACKING;
                    enemyRb.AddForce(transform.up * enemyAttackSpeed, ForceMode2D.Impulse);
                    aIPath.canMove = false;
                    StartCoroutine(AttackCooldownCounter(attackAnimTime));
                }
                break;
            case enemyState.ATTACKING:
                if (attackAnimEnded)
                {
                    if (hitPlayer)
                    {
                        StartCoroutine(hitPlayerCooldown(2f)); 
                    }
                    else
                    {
                        currentState = enemyState.STUN;
                    }
                    attackAnimEnded = false;
                }
                break;
            case enemyState.STUN:
                aIPath.canMove = false;
                StartCoroutine(stunCooldown(4f));
                //anim.setBool("Stun",true);
                break;
            case enemyState.DYING:
                break;

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            hitPlayer = true;
        }
    }

    private IEnumerator AttackCooldownCounter(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        enemyRb.velocity = Vector3.zero;
        enemyRb.rotation = 0;
        attackAnimEnded = true;
    }
    private IEnumerator hitPlayerCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        resetPlayer();
    }

    private IEnumerator stunCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        //anim.setBool("Stun",false);
        resetPlayer();
    }

    private void resetPlayer()
    {
        aIPath.maxSpeed = initialSpeed;
        timeChasing = 0;
        aIPath.canMove = true;
        currentState = enemyState.CHASING;
    }



}
