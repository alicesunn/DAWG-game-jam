using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioScript : MonoBehaviour
{
    [HideInInspector] public int layerIndex = 0; // layer that will next play when PlayNextLayer() is called
    [HideInInspector] public float volume = 0.5f; // volume setting of master music layer

    private StateScript state;
    private AudioSource[] tracks; // each individual audio clip
    private AudioSource momTrack = null; // the layer that's always playing

    void Start()
    {
        state = GameObject.Find("State").GetComponent<StateScript>();

        // Set all music layers to start playing at same time while muted
        AudioSource[] allTracks = GetComponentsInChildren<AudioSource>();
        tracks = new AudioSource[state.layerCount];
        double curTime = AudioSettings.dspTime;
        for (int i = 0; i < allTracks.Length; i++)
        {
            allTracks[i].PlayScheduled(curTime + 1.0f);
            allTracks[i].loop = true;
            if (!momTrack)
            {
                // First track always playing
                momTrack = allTracks[i];
                momTrack.mute = false;
                momTrack.volume = volume - 0.2f;
            }
            else
            {
                allTracks[i].mute = true;
                tracks[i - 1] = allTracks[i];
            }
        }

        layerIndex = 0;
    }

    public void PlayNextLayer()
    {
        if (layerIndex >= state.layerCount) return;

        // unmute layer
        tracks[layerIndex].mute = false;
        tracks[layerIndex].volume = volume;

        // activate next chick in line
        state.chicks[layerIndex].GetComponent<ChickScript>().Activate();

        layerIndex++;
    }
}
