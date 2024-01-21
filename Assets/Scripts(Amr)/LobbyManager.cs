using UnityEngine;
using UnityEngine.SceneManagement;

//Dieses Script steuert alles im Haupt Menu
public class LobbyManager : MonoBehaviour
{
    //Durch einen Knopf druck wird diese Method aktiviert und schickt einen in das Spiel
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}