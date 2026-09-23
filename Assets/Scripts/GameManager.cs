using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[SerializeField]
    //List<AudioClip> normalWhaleSongs = new List<AudioClip>();
    //[SerializeField]
    //List<AudioClip> tempSongs = new List<AudioClip>();

    //private Coroutine _soundPlay;
    //private AudioSource _audioSource;

    //private void OnEnable()
    //{
    //    _audioSource = GetComponent<AudioSource>();
    //    BinocularsController.onViewFieldChoosed += SoundPlay;

    //    for (int i = 0; i < normalWhaleSongs.Count; i++)
    //    {
    //        tempSongs.Add(normalWhaleSongs[i]);
    //    }
    //}

    //private void SoundPlay(int value)
    //{
    //    if (_soundPlay != null)
    //        StopCoroutine(PlayNormalSongs());
    //    _soundPlay = StartCoroutine(PlayNormalSongs());
    //}

    //private IEnumerator PlayNormalSongs()
    //{

    //    int houMuch = Random.Range(1, 3);

    //    for (int i = 0; i < houMuch; i++)
    //    {
    //        int randomSound = Random.Range(0, tempSongs.Count);

    //        //_audioSource.clip = tempSongs[i];
    //        _audioSource.PlayOneShot(_audioSource.clip);    
    //        tempSongs.Remove(tempSongs[randomSound]);
    //        yield return new WaitForSeconds(5);
    //    }

    //    for (int i = 0; i < normalWhaleSongs.Count; i++)
    //    {
    //        tempSongs.Add(normalWhaleSongs[i]);
    //    }
    //}

    //private void Start()
    //{
    //    Cursor.visible = true;
    //}

    //private void OnDisable()
    //{
    //    if(_soundPlay != null)
    //        StopCoroutine(PlayNormalSongs());

    //    BinocularsController.onViewFieldChoosed -= SoundPlay;
    //}
}
