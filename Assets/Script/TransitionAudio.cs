using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionAudio : MonoBehaviour
{
    private AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        audioData = gameObject.GetComponent<AudioSource>();
        LevelManager.Instance.PostActivateLeft += PlayAudio;
        LevelManager.Instance.PostActivateRight += PlayAudio;
    }

    void OnDestroy()
    {
        LevelManager.Instance.PostActivateLeft -= PlayAudio;
        LevelManager.Instance.PostActivateRight -= PlayAudio;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayAudio()
    {
        audioData.Play();
    }
}
