using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class MouvementController : MonoBehaviour
{
    public int speed = -2;
    public bool isCoin;
    public bool invinsib = false;
    public GameObject player;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        speed = -2;
        gameManager = GameObject.FindGameObjectWithTag("Spawner").GetComponent<GameManager>();
        StartCoroutine(Invinsibility());
    }

    IEnumerator Invinsibility()
    {
        yield return new WaitForSeconds(0.5f);
        invinsib = true;
    }

    // Update is called once per frame
    [Obsolete]
    void Update()
    {   
        if (gameManager.score >= 0)
        {
            speed = -2;
        }
        if (gameManager.score >= 5)
        {
            speed = -3;
        } 
         if (gameManager.score >= 10)
        {
            speed = -4;
        } 
         if (gameManager.score >= 20)
        {
            speed = -5;
        } 
         if (gameManager.score >= 30)
        {
            speed = -6;
        } 
         if (gameManager.score >= 45)
        {
            speed = -7;
        } 
         if (gameManager.score >= 60)
        {
            speed = -8;
        } 
         if (gameManager.score >= 80)
        {
            speed = -9;
        } 
         if (gameManager.score >= 100)
        {
            speed = -10;
        } 
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<Playercontroller>().isDead||player.GetComponent<Playercontroller>().isPause == true)
        {
            speed = 0;
        }
        if (transform.position.x <= -15)
        {
            Destroy(transform.gameObject);
        }          
        transform.position = new UnityEngine.Vector3(transform.position.x+(speed*Time.deltaTime),transform.position.y,0);
        if (gameObject.tag == "coin"&& invinsib)
        {
            gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "decke"||other.gameObject.tag == "Ground"||other.gameObject.tag=="laser")
        {
            Destroy(transform.gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "laser"&&!invinsib&&!isCoin)
        {
            Destroy(transform.gameObject);
        }
    }
}
