using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public enum playerState { NORMAL, DRIBLING, DYING }

    public playerState currentState = playerState.NORMAL;
    PlayerMovement playerMovementScript;
    public Rigidbody2D playerRb;

    public int playerHP;
    public int maxHP;
    public float damageCooldown;
    private bool canTakeDamege = true;

    Vector2 touch;
    Vector2 initialTouchPosition;
    Vector2 endTouchPosition;
    Vector2 direction;
    private void Start()
    {
        maxHP = playerHP;
        playerMovementScript = GetComponent<PlayerMovement>();
        playerRb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        switch (currentState)
        {
            case playerState.NORMAL:
                Time.timeScale = 1;
                break;
            case playerState.DRIBLING:
                Time.timeScale = 1;
                break;
            case playerState.DYING:
                Time.timeScale = 0;
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && canTakeDamege)
        {
            playerHP -= collision.gameObject.GetComponent<EnemyController>().enemyDamage;
            if (playerHP <= 0)
            {
                currentState = playerState.DYING;
            }
            canTakeDamege = false;
            StartCoroutine(CooldownCounter(damageCooldown));
        }
    }

    private IEnumerator CooldownCounter(float cooldown)
    {
        float counter = 0f;

        while(counter < cooldown)
        {
            counter += Time.deltaTime;
        }
        yield return null;
        canTakeDamege = true;
    }
}
