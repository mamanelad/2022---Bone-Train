using System.Collections.Generic;
using UnityEngine;

public class TrainAudio : MonoBehaviour
{
    private AudioSource _audioSource;

    public Queue<AudioClip> _AudioQueue;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_AudioQueue.Count > 0 && !_audioSource.isPlaying)
        {
            _audioSource.clip = _AudioQueue.Dequeue(); 
            _audioSource.Play(0);
        }
    }

    public void AddClip(AudioClip clip)
    {
        _AudioQueue.Enqueue(clip);
    }

    public bool IsPlayingSound()
    {
        return _audioSource.isPlaying;
    }
}
