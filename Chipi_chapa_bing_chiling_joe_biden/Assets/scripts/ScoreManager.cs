using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class ScoreManager : MonoBehaviour
{
    [SerializeField]    
    private Text scoreText;

    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        scoreText.text = score.ToString() + " POINTS";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoint() 
    {
        score++;
        scoreText.text = score.ToString() + "POINTS";
    }
}
