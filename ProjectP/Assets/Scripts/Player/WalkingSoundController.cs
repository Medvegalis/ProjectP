using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSoundController : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] walkingSoundClips;
    private AudioSource walkingSoundSource;

    private int clipIndex;
    // Start is called before the first frame update
    void Start()
    {
        clipIndex = 0;
        walkingSoundSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playStepSound() 
    {
        //set sound clip based on index
        walkingSoundSource.clip = walkingSoundClips[clipIndex];

        //play sound clip
        walkingSoundSource.Play();
        //increment clip loop index
        clipIndex++;
		if (clipIndex >= walkingSoundClips.Length)
		{
            clipIndex = 0;
		}
    }

    public bool stepSoundIsPlaying() 
    {
        return walkingSoundSource.isPlaying;
    }
}
