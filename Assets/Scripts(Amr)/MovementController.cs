using System.Collections;
using UnityEngine;

//Diese Script ist dafür da die Objekte zu bewegen
public class MovementController : MonoBehaviour
{
    //Variablen:

    //speed ist die Geschwindigkeit vom Objekt
    public int speed = 2;
    //immortal ist immer Aktiv wenn das Objekt nicht falsch bzw. unpassend gespawnt ist
    public bool immortal = false;
    //isCoin sagt ob es ein Coin ist oder nicht
    public bool isCoin;

    //player und gameManager sind die Scripts PlayerController und GameManager die man braucht um informationen zu kriegen
    public PlayerController player;
    public GameManager gameManager;

    void Start()
    {
        //Die folgenden Scripts werden verbunden sobald man spawnt:
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //Die Coroutine wird gestartetum zu überprüfen ob das Objekt nicht falsch bzw. unpassend gespawnt ist:
        StartCoroutine(Immortality());
    }

    void Update()
    {
        //Mit der folgenden Zeile wird das Objekt bewegt:
        transform.position = new UnityEngine.Vector3(transform.position.x + (-speed * Time.deltaTime), transform.position.y, 0);

        //Hier wird Die Geschwindigkeit eingestellt jenachdem was der Score ist:
        if (gameManager.score >= 0)
        {
            speed = 2;
        }
        if (gameManager.score >= 5)
        {
            speed = 3;
        }
        if (gameManager.score >= 10)
        {
            speed = 4;
        }
        if (gameManager.score >= 20)
        {
            speed = 5;
        }
        if (gameManager.score >= 30)
        {
            speed = 6;
        }
        if (gameManager.score >= 45)
        {
            speed = 7;
        }
        if (gameManager.score >= 60)
        {
            speed = 8;
        }
        if (gameManager.score >= 80)
        {
            speed = 9;
        }
        if (gameManager.score >= 100)
        {
            speed = 10;
        }
        //Falls man Tod ist oder das Spiel pausiert ist wird das objekt gestoppt:
        //Das muss nach ganz unten damit es nicht überschrieben wird
        if (player.isDead || gameManager.isPause)
        {
            speed = 0;
        }

        //Wenn das Objekt ausserhalb des Sichtfelds ist wird es gelöscht:
        if (transform.position.x <= -15)
        {
            Destroy(transform.gameObject);
        }

        //Falls das Coin richtig gespawnt ist wird es zu einem Trigger damit der Spieler nicht mit dem Coin kollidiert:
        if (isCoin && immortal)
        {
            gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }

    //Wenn das Objekt in der Decke oder im Boden oder im Laser drin Spawnt wird es gelöcht:
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ceiling" || other.gameObject.tag == "Ground" || other.gameObject.tag == "Laser")
        {
            Destroy(transform.gameObject);
        }
    }

    //Wenn das Laser inerhalb des Spieler großen Radius spawnt wird man gelöcht damit man theoretich unendlich lang spielen kann:
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Laser" && !immortal && !isCoin)
        {
            Destroy(transform.gameObject);
        }
    }

    //Wenn das Objekt nach 0.5 Sekunden nicht gelöcht wird heißt es das es richtig gespawnt ist:
    IEnumerator Immortality()
    {
        yield return new WaitForSeconds(0.5f);
        immortal = true;
    }
}