using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public PlayerController playerControllerScript;
    public int maxHP;
    public GameObject diedScreen;

    public Image[] heartsImage;

    // Update is called once per frame
    void Update()
    {
        switch (playerControllerScript.playerHP)
        {
            case 3:
                heartsImage[0].enabled = true;
                heartsImage[1].enabled = true;
                heartsImage[2].enabled = true;
                break;
            case 2:
                heartsImage[0].enabled = false;
                heartsImage[1].enabled = true;
                heartsImage[2].enabled = true;
                break;
            case 1:
                heartsImage[0].enabled = false;
                heartsImage[1].enabled = false;
                heartsImage[2].enabled = true;
                break;
            case 0:
                heartsImage[0].enabled = false;
                heartsImage[1].enabled = false;
                heartsImage[2].enabled = false;
                diedScreen.SetActive(true);
                break;
        }
        if (playerControllerScript.playerHP < 0)
        {
            heartsImage[0].enabled = false;
            heartsImage[1].enabled = false;
            heartsImage[2].enabled = false;
            diedScreen.SetActive(true);
        }
    }
}
