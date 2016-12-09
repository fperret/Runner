using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Drop on any object in the scene
// Need an active audio source on the main camera

public class BGM : MonoBehaviour {

    public List<AudioClip> songs;

    private AudioSource audioSource;
    private int currentMusicIndex;

    void Awake()
    {
        currentMusicIndex = -1;
        audioSource = Camera.main.GetComponent<AudioSource>();
        shuffleSongs();
        // Use Invoke with time in case game is paused at launch
        Invoke("newTrack", 1);
    }

	// Use this for initialization
	void Start ()
    {
        audioSource.volume = Game.instance.musicVolume;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.N))
        {
            CancelInvoke("newTrack");
            newTrack();
        }
	}

    private void newTrack()
    {
        audioSource.Stop();
        currentMusicIndex++;
        if (currentMusicIndex == songs.Count)
        {
            currentMusicIndex = 0;
        }
        audioSource.clip = songs[currentMusicIndex];
        audioSource.Play();
        Invoke("newTrack", songs[currentMusicIndex].length);
    }

    private void shuffleSongs()
    {
        int n = songs.Count;
        while (n > 1)
        {
            int k = (Random.Range(0, n) % n);
            n--;
            AudioClip tmp = songs[k];
            songs[k] = songs[n];
            songs[n] = tmp;
        }
    }
}
