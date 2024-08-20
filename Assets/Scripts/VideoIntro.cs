using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoIntro : MonoBehaviour
{
    public SceneLoader myScene;

    void Awake()
    {
        myScene.LoadScene("IntroVideo");
    }

    void Start() {
        
    }
    void Update() {
        if(Input.GetKeyDown("E")){
            myScene.LoadScene("Title");
        }
    }
}
