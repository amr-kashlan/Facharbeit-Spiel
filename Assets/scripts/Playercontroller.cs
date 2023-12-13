using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Playercontroller : MonoBehaviour
{
    public GameObject GameOver;
    public Rigidbody2D rb;
    public Animator anim;
    public bool isDead;
    public GameManager gameManager;
    public ParticleSystem flyPartical;
    public Button button;
    public bool isPause;
        // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        isPause=false;
        flyPartical.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&!isPause)
        {
            rb.gravityScale = -1;
            anim.SetBool("flying", true);
            flyPartical.Play();
        }
        if (Input.GetKeyUp(KeyCode.Space)&&!isPause)
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
            anim.SetBool("touching ground", true);
        } else if (other.gameObject.tag == "laser")
        {
            isDead = true;
            GameOver.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            anim.SetBool("touching ground", false);
        } 
    }
    public void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.gameObject.tag == "coin")
        {
            Destroy(other.gameObject);
            gameManager.AddScore();
        }
    }
    public void Pause()
    {
        isPause = true;
    }
    public void Resume()
    {
        isPause = false;
            rb.gravityScale = 1;
            anim.SetBool("flying", false);
            flyPartical.Stop();
    }
}