using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioScript : MonoBehaviour
{
    [HideInInspector] public int layerIndex = 0; // layer that will next play when PlayNextLayer() is called
    [HideInInspector] public float volume = 0.3f; // volume setting of master music layer

    private StateScript state;
    private AudioSource[] tracks; // each individual audio clip

    /* order of activation:
     * drums
     * horns, bass
     * guitar, strings
     * keys
     * vocals
     * synth
     */
    private Dictionary<int, KeyValuePair<int, int>> layerMap = new();

    private bool pressedSpace = false;

    void Awake()
    {
        // Set all child tracks to play (muted) at same time
        tracks = GetComponentsInChildren<AudioSource>();
        foreach (var track in tracks)
        {
            track.PlayScheduled(AudioSettings.dspTime + 1.0f);
            track.loop = true;
            track.mute = true;
        }

        // Determine which track files correspond to which layer (scuffed hardcoding ftw)
        // Ideally proto's export will have each layer as 1 file instead of multiple files per layer
        layerMap[0] = new KeyValuePair<int, int>(1, 1);
        layerMap[1] = new KeyValuePair<int, int>(2, 3);
        layerMap[2] = new KeyValuePair<int, int>(3, 4);
        layerMap[3] = new KeyValuePair<int, int>(5, 5);
        layerMap[4] = new KeyValuePair<int, int>(6, 6);
        layerMap[5] = new KeyValuePair<int, int>(7, 7);
    }

    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
    }

    void Update()
    {
        if (!pressedSpace && Input.GetKey(KeyCode.Space)) PlayNextLayer();
        pressedSpace = Input.GetKey(KeyCode.Space);
    }

    public void PlayNextLayer()
    {
        if (layerIndex >= state.layerCount) return;

        // unmute layer
        int start = layerMap[layerIndex].Key;
        int end = layerMap[layerIndex].Value;
        for (int i = start; i <= end; i++)
        {
            tracks[i - 1].mute = false;
            tracks[i - 1].volume = volume;
        }

        // activate next chick in line
        state.chicks[layerIndex].GetComponent<ChickScript>().Activate();

        layerIndex++;
    }
}
