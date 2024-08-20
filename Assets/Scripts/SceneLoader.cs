using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private AudioSource src;
    public AudioClip click;

    private void Awake()
    {
        src = GetComponent<AudioSource>();
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void PlayClick()
    {
        src.PlayOneShot(click);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
