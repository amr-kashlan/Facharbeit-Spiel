using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public void Play()
    {
        Debug.Log(1);
        SceneManager.LoadScene(1);
    }
}
