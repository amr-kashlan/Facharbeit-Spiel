using System.Collections;
using UnityEngine;

//Diese Script ist für den Spawn mechanismus der Laser und Coins da
public class SpawnManager : MonoBehaviour
{
    //Variablen:

    //laser und coin sind die Prefabs die gespawnt werden
    //Dabei ist laser ein Array für alle möglichkeiten wie die Laser aussehen können
    public GameObject[] laser;
    public GameObject coin;

    //player und gameManager sind die Scripts PlayerController und GameManager die man braucht um informationen zu kriegen
    public PlayerController player;
    public GameManager gameManager;
    void Start()
    {
        //Die folgenden Scripts werden verbunden sobald man spawnt:
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();

        //Ein Timer wird gestartet um Laser und Coins zu starten:
        StartCoroutine(WaitToSpawnLaser());
        StartCoroutine(WaitToSpawnCoin());
    }

    //Wenn diese Method Aktiviert wird werden 0.75 Sekunden gewartet um dan SpawnLaser zu Aktiviern:
    private IEnumerator WaitToSpawnLaser()
    {
        yield return new WaitForSeconds(0.75f);
        SpawnLaser();
    }

    //Wenn diese Method Aktiviert wird werden 3 Sekunden gewartet um dan SpawnCoin zu Aktiviern:
    private IEnumerator WaitToSpawnCoin()
    {
        yield return new WaitForSeconds(3);
        SpawnCoin();
    }

    //SpawnLaser spawnt ein Laser:
    private void SpawnLaser()
    {
        //Eine zufälige Laser wariante wird in einer zufäligen höhe gespawnt wenn das spiel läuft:
        if (player.isDead == false && gameManager.isPause == false)
        {
            int prefabNumber = UnityEngine.Random.Range(0, 31);
            Instantiate(laser[prefabNumber], new Vector3(20, UnityEngine.Random.Range(4, -5), transform.position.z), laser[prefabNumber].gameObject.transform.rotation);
        }
        //Nach dem das der Laser Gespawnt ist startet man die Coroutine WaitToSpawnLaser:
        StartCoroutine(WaitToSpawnLaser());
    }

    //SpawnCoin spawnt ein Coin:
    private void SpawnCoin()
    {
        //Ein Coin wird in einer zufäligen höhe gespawnt wenn das spiel läuft:
        if (player.isDead == false && gameManager.isPause == false)
        {
            Instantiate(coin, new Vector3(15, UnityEngine.Random.Range(4, -5), transform.position.z), Quaternion.identity);
        }
        //Nach dem das der Laser Gespawnt ist startet man die Coroutine WaitToSpawnLaser:
        StartCoroutine(WaitToSpawnCoin());
    }
}