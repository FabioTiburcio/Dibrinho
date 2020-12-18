using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPointsUI : MonoBehaviour
{

    public Text pointsText;
    public PointCollider pointColliderScript;

    // Update is called once per frame
    void Update()
    {
        pointsText.text = pointColliderScript.playerPoints.ToString();
    }
}
