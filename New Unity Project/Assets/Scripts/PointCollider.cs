using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCollider : MonoBehaviour
{
    float cooldownTime;
    public int playerPoints = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTime += Time.deltaTime;

    }

    public void AddPoints()
    {
        playerPoints += 10;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            if (cooldownTime > 1f)
            {
                cooldownTime = 0;
                AddPoints();
            }
        }
    }
}
