using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public ParticleSystem flyPartical;
    public GameManager gameManager;
    public bool isDead;
    void Start()
    {
        isDead = false;
        flyPartical.Stop();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameManager.isPause)
        {
            rb.gravityScale = -1;
            anim.SetBool("flying", true);
            flyPartical.Play();
        }
        if (Input.GetKeyUp(KeyCode.Space) && !gameManager.isPause)
        {
            rb.gravityScale = 1;
            anim.SetBool("flying", false);
            flyPartical.Stop();
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            anim.SetBool("touchingGround", true);
        }
        else if (other.gameObject.tag == "Laser")
        {
            isDead = true;
            gameObject.SetActive(false);
        }
    }
    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            anim.SetBool("touchingGround", false);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            gameManager.AddScore();
        }
    }
}