using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

//Dieses Skript kümmert sich um alles, was im Spiel organisatorisch ist:
public class GameManager : MonoBehaviour
{
    //Variablen:

    //score steht für gesammelte Anzahl an Coins.
    public int score;
    //highScore steht für den eigenen Rekord an gesamelten Coins.
    public int highScore;
    //scoreText ist der Text wo die anzahl an Coins angezeigt wird.
    public TextMeshProUGUI scoreText;

    //gameOver ist ein Objekt mit den Elementen die aktiviert werden müssen, damit der Game Over Screen erscheint.
    public GameObject gameOver;
    //gameOverText ist der Text wo die Anzahl an gesamelten Coins und der eigene Rekord angezeigt wird.
    public TextMeshProUGUI gameOverText;

    //isPause sagt ob das Spiel angehalten ist.
    public bool isPause = false;
    //pauseButton ist der Knopf der das Spiel Pausiert.
    public Button pauseButton;

    //player ist die Script vom Player um Informationen zu bekommen.
    public PlayerController player;
    //bludParticle sind die Partikel, die erscheinen, wenn man stirbt.
    //Warum diese Variable nicht in PlayerController ist: Ich habe die Blutpartikel hier reingemacht anstatt im Playercontroller, da die nachdem Tod gebraucht werden und die Playercontroller Script nach dem Tod nicht mehr Funktioniert.
    public ParticleSystem bludParticle;

    void Start()
    {
        //Die Blutparikel werden gestoppt:
        bludParticle.Stop();

        //Der Score wird resetet und der Highscore wird geholt durch PlayerPrefs.GetInt():
        highScore = PlayerPrefs.GetInt("record", 0);
        score = 0;
    }

    void Update()
    {
        //Hier wird geguckt ob man das Spiel noch nie gespielt hat, indem überprüft wird ob der Score und Highscore gleich 0 sind, damit "Press Space" angezeigt wird:
        if (highScore == 0 && score == 0)
        {
            scoreText.text = "0\n Press Space";
        }
        //Ansonsten steht da nur der jetzige Score:
        else
        {
            scoreText.text = score.ToString();
        }

        //Wenn der Spieler tod ist passiert folgendes:
        if (player.isDead == true)
        {
            //Hier wird geguckt ob man den Highscore gebrochen hat:
            if (highScore < score)
            {
                //Falls es so ist wird der Wert vom Highscore zum Wert des jetzigen Scores:
                highScore = score;
                PlayerPrefs.SetInt("record", highScore);
            }

            //Der Text vom Game Over Screen wird erstellt:
            gameOverText.text = "Game Over \n" + "Score: " + score.ToString() + "\nHighscore: " + highScore.ToString();

            //und es würde wie folgt aussehen:

            //Game Over
            //Score: score.ToString()
            //Highscore: highScore.ToString()

            //Der Pause Knopf wird deaktiviert da man sonst ins Pause Menü kann, obwohl man Tod ist:
            pauseButton.gameObject.SetActive(false);

            //Der Game Over Screen wird deaktiviert:
            gameOver.SetActive(true);

            //Die Blutpartikel werden zum Spieler transportiert und abgespielt:
            bludParticle.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            bludParticle.Play();
        }
    }

    //Der Pause Knopf aktiviert diese Method, damit wird das Spiel pausiert und stellt isPause auf true:
    public void Pause()
    {
        isPause = true;
    }

    //Ein weiterer Knopf aktiviert diese Method:
    public void Resume()
    {
        //Das Spiel geht weiter und isPause ist wieder auf false:
        isPause = false;

        //Hier werden für den Spieler eine folgende Sache eingestellt, die man einmal machen muss, darum kann man das nicht in einer if clause in Update() rein machen:
        //Man aktiviert die Gravity,
        player.rb.gravityScale = 1;
        //Die Flug Animation wird gestoppt, falls sie aktiviert ist,
        player.anim.SetBool("flying", false);
        //Die Flug Partikel werden gestoppt, falls sie aktiviert sind.
        player.flyPartical.Stop();
    }

    //Mit einem Knopfdruck wird diese Method aktiviert, die dafür da ist das Spiel neuzustarten:
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    //Mit einem Knopfdruck wird diese Method aktiviert, die vom Spiel zum Menü wechselt:
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    //Wenn man dise Method aktiviert, wird der Score um eins erhöht:
    public void AddScore()
    {
        score++;
    }
}