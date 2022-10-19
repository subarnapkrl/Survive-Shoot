using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillManager : MonoBehaviour
{
    public static KillManager instance;
    [SerializeField] Text scoreText;
    [SerializeField] Text highScoreText;

    int score=0;
    int highScore=0;
     void Awake() {
        instance=this;
    }
    // Start is called before the first frame update
    void Start()
    {
        highScore=PlayerPrefs.GetInt("HighScore",0);
        scoreText.text="Spookies Killed: "+score.ToString();
        highScoreText.text="Highest  Killed: "+highScore.ToString();
    }

   public void AddPoints()
   {
    score++;
    scoreText.text="Spookies Killed: "+score.ToString();
    if(highScore<score)
    {
        PlayerPrefs.SetInt("HighScore",score);
    }
   }
}
