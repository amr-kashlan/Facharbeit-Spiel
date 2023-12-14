using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI GameOverText;
    public Playercontroller player;
    public int highScore;
    public ParticleSystem blud;
    // Start is called before the first frame update
    void Start()
    {
        blud.Pause();
        highScore = PlayerPrefs.GetInt("record", 0);
        score = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (highScore == 0&&score == 0)
        {
            ScoreText.text = "0\n Press space";
        } else
        {
            ScoreText.text = score.ToString();
        }
        if (player.isDead == true)
        {
            if (highScore<score)
            {
                highScore = score;
                PlayerPrefs.SetInt("record", highScore);
            }
            blud.transform.position=new Vector3(player.transform.position.x,player.transform.position.y,player.transform.position.z);
            blud.Play();
            GameOverText.text = "Game Over \n"+"Score: "+score.ToString() +"\n Highscore: " + highScore.ToString();
        }
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void AddScore()
    {
        score++;
    }
}