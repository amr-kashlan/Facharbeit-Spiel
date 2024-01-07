using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] laser;
    public GameObject coin;
    public Playercontroller player;
    public GameManager gameManager;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Playercontroller>();
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        Spawn();
        StartCoroutine(WaitToSpawnCoin());
    }
    private IEnumerator WaitToSpawn()
    {

        yield return new WaitForSeconds(0.75f);
        Spawn();

    }
    private IEnumerator WaitToSpawnCoin()
    {
        yield return new WaitForSeconds(3);
        SpawnCoin();
    }
    private void Spawn()
    {
        if (player.isDead == false && gameManager.isPause == false)
        {
            int prefabNumber = UnityEngine.Random.Range(0, 31);
            Instantiate(laser[prefabNumber], new Vector3(20, UnityEngine.Random.Range(4, -5), transform.position.z), laser[prefabNumber].gameObject.transform.rotation);
        }
        StartCoroutine(WaitToSpawn());
    }
    private void SpawnCoin()
    {
        if (player.isDead == false && gameManager.isPause == false)
        {
            Instantiate(coin, new Vector3(15, UnityEngine.Random.Range(4, -5), transform.position.z), Quaternion.identity);
        }
        StartCoroutine(WaitToSpawnCoin());
    }
}