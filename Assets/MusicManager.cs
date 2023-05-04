using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicManager : MonoBehaviour {
    [SerializeField] List<AudioClip> playList;
    List<AudioClip> playedSongs = new List<AudioClip>();
    float delay = 0;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        delay -= Time.deltaTime;
        if (delay > 0 ) return;
        int songID = Random.Range(0, playList.Count);
        audioSource.clip = playList[songID];
        audioSource.PlayScheduled(delay);
        delay = playList[songID].length;
        playedSongs.Add(playList[songID]);
        playList.RemoveAt(songID);
        if (playList.Count == 0) {
               playList = playedSongs;
               playedSongs.Clear();
        }
    }
}
