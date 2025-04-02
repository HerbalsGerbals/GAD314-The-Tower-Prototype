using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    public float Score;
    [SerializeField] private TextMeshProUGUI scoreText;
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + Score;
    }

    public void IncreaseScore(float score)
    {
        Score += score;
    }

}
