using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button pauseButton;
    public GameObject gameOver;
    public Playercontroller player;
    public int highScore;
    public ParticleSystem bludParticle;
    public bool isPause;
    void Start()
    {
        isPause = false;
        bludParticle.Stop();
        highScore = PlayerPrefs.GetInt("record", 0);
        score = 0;
    }
    void Update()
    {
        if (highScore == 0 && score == 0)
        {
            scoreText.text = "0\n Press space";
        }
        else
        {
            scoreText.text = score.ToString();
        }
        if (player.isDead == true)
        {
            if (highScore < score)
            {
                highScore = score;
                PlayerPrefs.SetInt("record", highScore);
            }
            pauseButton.gameObject.SetActive(false);
            gameOver.SetActive(true);
            bludParticle.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            bludParticle.Play();
            gameOverText.text = "Game Over \n" + "Score: " + score.ToString() + "\n Highscore: " + highScore.ToString();
        }
    }
    public void Pause()
    {
        isPause = true;
    }
    public void Resume()
    {
        isPause = false;
        player.rb.gravityScale = 1;
        player.anim.SetBool("flying", false);
        player.flyPartical.Stop();
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