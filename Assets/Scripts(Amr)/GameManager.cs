using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

//Dieses Skript kümmert sich um alles im Spiel was organisatorisch ist
public class GameManager : MonoBehaviour
{
    //Variablen:

    //score steht für gesammelte anzahl an Coins
    public int score;
    //highScore steht für den eigenen Rekord an gesamelten Coins
    public int highScore;
    //scoreText ist der Text wo die anzahl an Coins angezeigt wird
    public TextMeshProUGUI scoreText;

    //gameOver ist ein Objekt mit den Elementen die aktiviert werden müssen damit der Game Over Screen erscheint
    public GameObject gameOver;
    //gameOverText ist der Text wo die anzahl an gesamelten Coins und der eigene Rekord angezeigt wird
    public TextMeshProUGUI gameOverText;

    //isPause sagt ob das Spiel angehalten ist
    public bool isPause = false;
    //pauseButton ist der Knopf der das Spiel Pausiert
    public Button pauseButton;

    //player ist die Script vom Player um informationen zu kriegen
    public PlayerController player;
    //bludParticle sind die Partikel die erscheinen wenn man stirbt
    //Warum diese Variable nicht in PlayerController ist:Ich habe die Blutpartikel hier rein gemacht anstatt im Playercontroller da die nachdem Tod gebraucht werden und die Playercontroller Script nach dem Tod nicht mehr Funktioniert
    public ParticleSystem bludParticle;

    void Start()
    {
        //Die Blutparikel werden gestoppt:
        bludParticle.Stop();

        //Der Score wird Resetet und der Highscore wird geholt durch PlayerPrefs.GetInt():
        highScore = PlayerPrefs.GetInt("record", 0);
        score = 0;
    }

    void Update()
    {
        //Hier wird geguckt ob man das Spiel noch nie Gespielt hat damit "Press space" da steht
        //In dem geguckt wird ob der Score und der Highscore 0 sind:
        if (highScore == 0 && score == 0)
        {
            scoreText.text = "0\n Press space";
        }
        //Ansonsten steht da nur der jetzige Score:
        else
        {
            scoreText.text = score.ToString();
        }

        //Wenn der Spieler tod ist passiert folgendes:
        if (player.isDead == true)
        {
            //Hier wird geguckt ob man den Highscore gebrochen hat :
            if (highScore < score)
            {
                //Falls es so ist wird der wert vom Highscore zum wert des jetzigen Scores:
                highScore = score;
                PlayerPrefs.SetInt("record", highScore);
            }

            //Der Text vom Game Over Screen wird erstellt
            gameOverText.text = "Game Over \n" + "Score: " + score.ToString() + "\nHighscore: " + highScore.ToString();

            //und es würde wie folgt aussehen:

            //Game Over
            //Score: score.ToString()
            //Highscore: highScore.ToString()

            //Der Pause Knopf wird Deaktiviert da man sonst ins Pause Menu kann obwohl man Tod ist:
            pauseButton.gameObject.SetActive(false);

            //Der Game Over Screen wird Deaktiviert:
            gameOver.SetActive(true);

            //Die Blutpartikel werden zum Spieler transportiert und Abgespielt:
            bludParticle.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            bludParticle.Play();
        }
    }

    //Der Pause Knopf aktiviert diese Methond Pausiert damit das Spiel und stellt isPause auf true:
    public void Pause()
    {
        isPause = true;
    }

    //Ein weiterer Knopf aktiviert diese Method:
    public void Resume()
    {
        //Das Spiel geht weiter und isPause ist wieder auf false:
        isPause = false;

        //Hier werden für den spieler ein folgende Sachen Eingestellt die man ein mal machen muss darum kann man das nicht in einer if clause in Update() rein machen;
        //Man Aktiviert die Gravity
        player.rb.gravityScale = 1;
        //Die Flug Animation wird gestoppt falls sie aktiviert sind
        player.anim.SetBool("flying", false);
        //Die Flug Partikel werden gestoppt falls sie aktiviert sind
        player.flyPartical.Stop();
    }

    //Mit einem Knopfdruck wird diese Method Aktiviert die ist dafür da das Spiel neu zu starten
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    //Mit einem Knopfdruck wird diese Method Aktiviert die ist um vom spiel zum Menu zu wechseln
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    //Wenn man dise Method aktiviert wird der Score um eins höher
    public void AddScore()
    {
        score++;
    }
}