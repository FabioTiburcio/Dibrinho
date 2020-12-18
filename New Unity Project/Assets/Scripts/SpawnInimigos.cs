using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnInimigos : MonoBehaviour
{
    public Transform player;
    float Xmax;
    float Xmin;
    float Ymax;
    float Ymin;

    public GameObject normalEnemy;
    public GameObject fastEnemy;
    public GameObject fatEnemy;
    private GameObject enemyInstantiated;
    private int enemyToInstantiate;
    public List<GameObject> Inimigos;

    public int MaxIni;
    public float rateSpawn;

    private float currentRateSpawn;

    void Start()
    {
        for (int i = 0; i < MaxIni; i++)
        {
            enemyToInstantiate = Random.Range(1, 101);
            if (enemyToInstantiate <= 50)
            {
                enemyInstantiated = normalEnemy;
            }
            else if(enemyToInstantiate > 50 && enemyToInstantiate<=80)
            {
                enemyInstantiated = fatEnemy;
            }
            else if (enemyToInstantiate > 80)
            {
                enemyInstantiated = fastEnemy;
            }
            
            GameObject TempIni = Instantiate(enemyInstantiated) as GameObject;
            Inimigos.Add(TempIni);
            TempIni.SetActive(false);
        }
    }


    void Update()
    {
        Xmax = player.position.x + 20;
        Xmin = player.position.x - 20;
        Ymax = player.position.y + 30;
        Ymin = player.position.y - 30;
        currentRateSpawn += Time.deltaTime;
        if (currentRateSpawn > rateSpawn)
        {
            currentRateSpawn = 0;
            Respawn();

        }
    }
    private void Respawn()
    {

        float randPositionX = Random.Range(-40, 40);
        float randPositionY = Random.Range(-60, 60);
        
        GameObject TempIni = null;
        for (int i = 0; i < MaxIni; i++)
        {
            if (Inimigos[i].activeSelf == false)
            {
                TempIni = Inimigos[i];
                break;
            }
        }
        if (TempIni != null)
        {
            if (randPositionX < Xmin || randPositionX > Xmax && randPositionY < Ymin || randPositionY > Ymax)
            {
                TempIni.transform.position = new Vector3(randPositionX, randPositionY, transform.position.z);
                TempIni.SetActive(true);
            }
            
        }
    }

}