using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMAnager : MonoBehaviour
{
    public Text scoreText;

    public static int scoreCount;

    private void Update()
    {
        scoreText.text = "Score: " + Mathf.Round(scoreCount);
    }
}
