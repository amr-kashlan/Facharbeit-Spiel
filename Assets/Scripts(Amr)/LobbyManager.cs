using UnityEngine;
using UnityEngine.SceneManagement;

//Dieses Skript steuert alles im Hauptmenü:
public class LobbyManager : MonoBehaviour
{
    //Durch einen Knopfdruck wird diese Method aktiviert und schickt einen in das Spiel:
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}