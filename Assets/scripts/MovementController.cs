using System.Collections;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public int speed = -2;
    public bool isCoin;
    public bool immortal = false;
    public Playercontroller player;
    public GameManager gameManager;
    void Start()
    {
        speed = -2;
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Playercontroller>();
        StartCoroutine(Immortality());
    }
    void Update()
    {
        transform.position = new UnityEngine.Vector3(transform.position.x + (-speed * Time.deltaTime), transform.position.y, 0);
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
        if (player.isDead || gameManager.isPause)
        {
            speed = 0;
        }
        if (transform.position.x <= -15)
        {
            Destroy(transform.gameObject);
        }
        if (gameObject.tag == "Coin" && immortal)
        {
            gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }
    IEnumerator Immortality()
    {
        yield return new WaitForSeconds(0.5f);
        immortal = true;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ceiling" || other.gameObject.tag == "Ground" || other.gameObject.tag == "Laser")
        {
            Destroy(transform.gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Laser" && !immortal && !isCoin)
        {
            Destroy(transform.gameObject);
        }
    }
}