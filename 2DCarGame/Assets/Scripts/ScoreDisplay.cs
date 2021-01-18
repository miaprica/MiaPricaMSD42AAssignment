using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    Text scoreText;
    GameSession gameSession;

    //Start is called before the first frame update
    void Start()
    {
        scoreText.GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
    }

    //Update is called once per frame
    public void Update()
    {
        scoreText.text = gameSession.GetScore().ToString();        //i was getting a null reference error, in update the error is updating so i commented it out, its easier for the eye to see 1 error than "Error: 999+"
    }
}
