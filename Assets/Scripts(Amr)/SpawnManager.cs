using System.Collections;
using UnityEngine;

//Dieses Skript ist für den Spawnmechanismus der Laser und Coins veranwortlich:
public class SpawnManager : MonoBehaviour
{
    //Variablen:

    //laser und coin sind die Prefabs die gespawnt werden, dabei ist laser ein Array für alle Möglichkeiten wie die Laser aussehen können.
    public GameObject[] laser;
    public GameObject coin;

    //player und gameManager sind die Scripts PlayerController und GameManager, die man braucht um Informationen zu bekommen.
    public PlayerController player;
    public GameManager gameManager;
    void Start()
    {
        //Die folgenden Scripts werden verbunden sobald man spawnt:
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();

        //Ein Timer wird gestartet um die Laser und Coins generation zu starten:
        StartCoroutine(WaitToSpawnLaser());
        StartCoroutine(WaitToSpawnCoin());
    }

    //Wenn diese Method aktiviert wird, werden 0.75 Sekunden gewartet, um dann den SpawnLaser zu aktiviern:
    private IEnumerator WaitToSpawnLaser()
    {
        yield return new WaitForSeconds(0.75f);
        SpawnLaser();
    }

    //Wenn diese Method aktiviert wird, werden 3 Sekunden gewartet, um dann den SpawnCoin zu aktiviern:
    private IEnumerator WaitToSpawnCoin()
    {
        yield return new WaitForSeconds(3);
        SpawnCoin();
    }

    //SpawnLaser spawnt ein Laser:
    private void SpawnLaser()
    {
        //Eine zufälige Laserwariante wird in einer zufäligen Höhe gespawnt, wenn das Spiel läuft:
        if (player.isDead == false && gameManager.isPause == false)
        {
            int prefabNumber = UnityEngine.Random.Range(0, 31);
            Instantiate(laser[prefabNumber], new Vector3(20, UnityEngine.Random.Range(4, -5), transform.position.z), laser[prefabNumber].gameObject.transform.rotation);
        }
        //Nachdem der Laser gespawnt ist, startet man die Coroutine WaitToSpawnLaser:
        StartCoroutine(WaitToSpawnLaser());
    }

    //SpawnCoin spawnt ein Coin:
    private void SpawnCoin()
    {
        //Ein Coin wird in einer zufäligen höhe gespawnt, wenn das Spiel läuft:
        if (player.isDead == false && gameManager.isPause == false)
        {
            Instantiate(coin, new Vector3(15, UnityEngine.Random.Range(4, -5), transform.position.z), Quaternion.identity);
        }
        //Nachdem der Laser gespawnt ist, startet man die Coroutine WaitToSpawnLaser:
        StartCoroutine(WaitToSpawnCoin());
    }
}