using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoIntro : MonoBehaviour
{
    void Update() {
        if(Input.GetKeyDown(KeyCode.E)){
            SceneManager.LoadScene("Title");
        }
    }
}
