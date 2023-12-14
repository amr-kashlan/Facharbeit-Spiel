using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
public class SpawnLaser : MonoBehaviour
{
    public GameObject[] laser;
    public GameObject player;
    public GameObject coin;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Spawn();
        StartCoroutine(WaitToSpawnCoin());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator WaitToSpawn()
    {
       
        yield return new WaitForSeconds(0.75f);
        Spawn();
        
    }

    private void Spawn()
    {
        if (player.GetComponent<Playercontroller>().isDead == false&&player.GetComponent<Playercontroller>().isPause == false)
        { 
        int prefabNumber = UnityEngine.Random.Range(0,31);
        Instantiate(laser[prefabNumber], new Vector3(20, UnityEngine.Random.Range(4, -5), transform.position.z), laser[prefabNumber].gameObject.transform.rotation);
        }
        StartCoroutine(WaitToSpawn());
    }
    private IEnumerator WaitToSpawnCoin()
    {
        yield return new WaitForSeconds(3);       
        SpawnCoin();
    }

    private void SpawnCoin()
    {
        if (player.GetComponent<Playercontroller>().isDead == false&&player.GetComponent<Playercontroller>().isPause == false)
        { 
        Instantiate(coin, new Vector3(15, UnityEngine.Random.Range(4, -5), transform.position.z), Quaternion.identity);
        }
        StartCoroutine(WaitToSpawnCoin());
    }
}
