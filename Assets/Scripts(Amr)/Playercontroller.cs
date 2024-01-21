using UnityEngine;

//Diese Script ist für den Spieler verantwortlich
public class PlayerController : MonoBehaviour
{
    //Variablen:

    //rb ist das Physik Komponent
    public Rigidbody2D rb;
    //anim ist das Animations Komponent was wir brauchen um Animationen zu starten
    public Animator anim;
    //flyPartical sind die Partikel für das Fliegen
    public ParticleSystem flyPartical;

    //isDead sagt an ob der Spieler Tod ist oder nicht
    public bool isDead = false;

    //gameManager ist die Script GameManager die man braucht um informationen zu kriegen
    public GameManager gameManager;

    void Start()
    {
        //Flug Partikel werden gestoppt:
        flyPartical.Stop();
    }

    void Update()
    {
        //Hier wird geguckt ob die Leertaste Taste gedrückt wird während dass das Spiel läuft:
        if (Input.GetKeyDown(KeyCode.Space) && !gameManager.isPause)
        {
            //Gravitation kehrt sich um:
            rb.gravityScale = -1;
            //Dem Animator wird die nachricht gegeben das man Fliegt:
            anim.SetBool("flying", true);
            //Flug Partikel werden gestartet:
            flyPartical.Play();
        }

        //Hier wird geguckt ob die Leertaste Taste nicht mehr gedrückt wird während dass das Spiel läuft:
        if (Input.GetKeyUp(KeyCode.Space) && !gameManager.isPause)
        {
            //Gravitation kehrt sich um:
            rb.gravityScale = 1;
            //Dem Animator wird die nachricht gegeben das man nicht mehr Fliegt:
            anim.SetBool("flying", false);
            //Flug Partikel werden gestoppt:
            flyPartical.Stop();
        }
    }

    //Wenn der Spieler mit irgend etwas Kollidiert wird diese Method einmal Aktiviert:
    public void OnCollisionEnter2D(Collision2D other)
    {
        //Wenn man mit dem boden Kollidiert wird dem Animator die nachricht gegeben das man den boden Berührt:
        if (other.gameObject.tag == "Ground")
        {
            anim.SetBool("touchingGround", true);
        }
        //Wenn man mit Laser Kollidiert wird der Spieler Deaktiviert und isDead auf true gestellt:
        else if (other.gameObject.tag == "Laser")
        {
            isDead = true;
            gameObject.SetActive(false);
        }
    }

    //Wenn eine Kollision zu ende ist wird diese Method einmal Aktiviert:
    public void OnCollisionExit2D(Collision2D other)
    {
        //Wenn man nicht mehr mit dem boden Kollidiert wird dem Animator die nachricht gegeben das man den boden nicht mehr Berührt:
        if (other.gameObject.tag == "Ground")
        {
            anim.SetBool("touchingGround", false);
        }
    }

    //Wenn der Spieler ein Objekt trifft was ein "Trigger" ist wird diese Method einmal Aktiviert:
    public void OnTriggerEnter2D(Collider2D other)
    {
        //Wenn es ein Coin ist wird das Coin Gelöcht und die Method AddScore in gameManager einmal Aktiviert:
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            gameManager.AddScore();
        }
    }
}