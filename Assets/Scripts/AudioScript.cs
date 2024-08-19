using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioScript : MonoBehaviour
{
    [HideInInspector] public int layerIndex = 0; // layer that will next play when PlayNextLayer() is called
    [HideInInspector] public float volume = 0.7f; // volume setting of master music layer

    private StateScript state;
    private AudioSource[] tracks; // each individual audio clip

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

        // First track always playing
        tracks[0].mute = false;
        tracks[0].volume = volume;
    }

    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();
    }

    public void PlayNextLayer()
    {
        if (layerIndex >= state.layerCount) return;

        // unmute layer
        tracks[layerIndex + 1].mute = false;
        tracks[layerIndex + 1].volume = volume;

        // activate next chick in line
        state.chicks[layerIndex].GetComponent<ChickScript>().Activate();

        layerIndex++;
    }
}
