using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DigitalRubyShared
{
    public class DribleController : MonoBehaviour
    {
        public bool canDrible = true;
        bool isDribling;
        private float dribleCooldown = 3f;
        public HabilityUI habilityUi;
        private PlayerController playerControllerScript;
        Touch touch;
        EnemyController enemyDribled;
        Rigidbody2D dribleRb;

        public FingersImageGestureHelperComponentScript ImageScript;
        // Start is called before the first frame update
        void Start()
        {
            playerControllerScript = GetComponentInParent<PlayerController>();
            dribleRb = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {


        }
        // Update is called once per frame
        void FixedUpdate()
        {
            touch = Input.GetTouch(0);
            if (isDribling)
            {
                switch (touch.phase)
                {
                    case UnityEngine.TouchPhase.Stationary:
                        break;
                    case UnityEngine.TouchPhase.Moved:
                        if (isDribling)
                        {
                            ImageGestureImage match = ImageScript.CheckForImageMatch();
                            if (match != null)
                            {
                                switch (match.Name)
                                {
                                    case "ZigZag":
                                        Debug.Log(match.Name);
                                        break;
                                    case "HorizontalLine":
                                        Debug.Log(match.Name);
                                        break;
                                    case "VerticalLine":
                                        Debug.Log(match.Name);
                                        break;
                                }
                            }
                        }
                        break;
                }
                if (touch.tapCount == 0 && Input.GetMouseButtonDown(0))
                {
                    ImageGestureImage match = ImageScript.CheckForImageMatch();
                    if (match != null)
                    {
                        switch (match.Name)
                        {
                            case "ZigZag":
                                Debug.Log(match.Name);
                                break;
                            case "HorizontalLine":
                                Debug.Log(match.Name);
                                break;
                            case "VerticalLine":
                                Debug.Log(match.Name);
                                break;
                        }
                    }
                }
            }
        }

        IEnumerator driblingTime()
        {
            yield return new WaitForSeconds(2f);
            playerControllerScript.gameObject.GetComponent<CircleCollider2D>().enabled = true;
            playerControllerScript.currentState = PlayerController.playerState.NORMAL;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Enemy")
            {
                if (canDrible)
                {
                    enemyDribled = collision.GetComponent<EnemyController>();
                    isDribling = true;
                    canDrible = false;
                    StartCoroutine(DribleCooldownRoutine(dribleCooldown));
                    playerControllerScript.currentState = PlayerController.playerState.DRIBLING;
                }
            }
        }
        IEnumerator DribleCooldownRoutine(float cooldown)
        {
            yield return new WaitForSeconds(cooldown);
            canDrible = true;
        }

    }
}

