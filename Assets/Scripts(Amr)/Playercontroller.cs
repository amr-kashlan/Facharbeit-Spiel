using UnityEngine;

//Dieses Skript ist für den Spieler verantwortlich:
public class PlayerController : MonoBehaviour
{
    //Variablen:

    //rb ist die Physikkomponente.
    public Rigidbody2D rb;
    //anim ist die Animationskomponente, was wir brauchen, um Animationen zu starten.
    public Animator anim;
    //flyPartical sind die Partikel für das Fliegen.
    public ParticleSystem flyPartical;

    //isDead sagt an, ob der Spieler Tod ist oder nicht.
    public bool isDead = false;

    //gameManager ist das Skript GameManager, die man braucht, um Informationen zu bekommen.
    public GameManager gameManager;

    void Start()
    {
        //Flugpartikel werden gestoppt:
        flyPartical.Stop();
    }

    void Update()
    {
        //Hier wird geguckt, ob die Leertaste gedrückt wird, während das Spiel läuft:
        if (Input.GetKeyDown(KeyCode.Space) && !gameManager.isPause)
        {
            //Gravitation kehrt sich um:
            rb.gravityScale = -1;
            //Dem Animator wird die Nachricht gesendet, dass man fliegt:
            anim.SetBool("flying", true);
            //Flug Partikel werden gestartet:
            flyPartical.Play();
        }

        //Hier wird geguckt, ob die Leertaste nicht mehr gedrückt wird, während das Spiel läuft:
        if (Input.GetKeyUp(KeyCode.Space) && !gameManager.isPause)
        {
            //Gravitation kehrt sich um:
            rb.gravityScale = 1;
            //Dem Animator wird die Nachricht gesendet, dass man nicht mehr fliegt:
            anim.SetBool("flying", false);
            //Flug Partikel werden gestoppt:
            flyPartical.Stop();
        }
    }

    //Wenn der Spieler mit irgend etwas kollidiert, wird diese Method einmal aktiviert:
    public void OnCollisionEnter2D(Collision2D other)
    {
        //Wenn man mit dem Boden kollidiert, wird dem Animator die Nachricht gesendet, dass man den Boden berührt hat:
        if (other.gameObject.tag == "Ground")
        {
            anim.SetBool("touchingGround", true);
        }
        //Wenn man mit dem Laser kollidiert, wird der Spieler deaktiviert und isDead auf true gestellt:
        else if (other.gameObject.tag == "Laser")
        {
            isDead = true;
            gameObject.SetActive(false);
        }
    }

    //Wenn eine Kollision zu Ende ist wird diese Method einmal aktiviert:
    public void OnCollisionExit2D(Collision2D other)
    {
        //Wenn man nicht mehr mit dem Boden kollidiert, wird dem Animator die Nachricht gesendet, dass man den Boden nicht mehr berührt:
        if (other.gameObject.tag == "Ground")
        {
            anim.SetBool("touchingGround", false);
        }
    }

    //Wenn der Spieler ein Objekt trifft, was ein "Trigger" ist, wird diese Method einmal aktiviert:
    public void OnTriggerEnter2D(Collider2D other)
    {
        //Wenn es ein Coin ist, wird das Coin gelöcht und die Method AddScore in gameManager einmal aktiviert:
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            gameManager.AddScore();
        }
    }
}