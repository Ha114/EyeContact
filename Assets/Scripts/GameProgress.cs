using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameProgress;

public class GameProgress : MonoBehaviour
{
    private Coroutine _gameProgress;

    public delegate void OnDialogSend(string text);
    public static event OnDialogSend onDialogSend;

    private bool _isPlayerRead = false;

    public List<string> story1 = new List<string>();
    public List<string> story2 = new List<string>();
    public List<string> story3 = new List<string>();
    public List<string> story4 = new List<string>();
    public List<string> story5 = new List<string>();
    public List<string> story6 = new List<string>();
    public List<string> story7 = new List<string>();
    public List<string> story8 = new List<string>();


    public delegate void OnStartDecay();
    public static event OnStartDecay onStartDecay;

    public delegate void OnStartDie();
    public static event OnStartDie onStartDie;

    [SerializeField]
    public GameObject BinocleMode;
    void Start()
    {
        _gameProgress = StartCoroutine(Progress());
        ChatBoxMenu.onDialogRead += PlayerClicked;
    }

    private void PlayerClicked(bool iAmRead)
    {
        _isPlayerRead=iAmRead;
    }

    IEnumerator Progress()
    {
        for(int i = 0; i < story1.Count; i++)
        {
            onDialogSend?.Invoke(story1[i]);
            yield return new WaitUntil(() => PlayerRead());
            _isPlayerRead = false;
        }
        //article1


        for (int i = 0; i < story2.Count; i++)
        {
            onDialogSend?.Invoke(story2[i]);
            yield return new WaitUntil(() => PlayerRead());
            _isPlayerRead = false;
        }

        //article2
        for (int i = 0; i < story3.Count; i++)
        {
            onDialogSend?.Invoke(story3[i]);
            yield return new WaitUntil(() => PlayerRead());
            _isPlayerRead = false;
        }

        //delegate
        yield return new WaitForSeconds(3);
        yield return new WaitUntil(() => BinocleMode.activeInHierarchy);
        onStartDecay?.Invoke();
        yield return new WaitForSeconds(3);

        //story

        for (int i = 0; i < story4.Count; i++)
        {
            onDialogSend?.Invoke(story4[i]);
            yield return new WaitUntil(() => PlayerRead());
            _isPlayerRead = false;
        }

        //story

        for (int i = 0; i < story5.Count; i++)
        {
            onDialogSend?.Invoke(story5[i]);
            yield return new WaitUntil(() => PlayerRead());
            _isPlayerRead = false;
        }

        for (int i = 0; i < story6.Count; i++)
        {
            onDialogSend?.Invoke(story6[i]);
            yield return new WaitUntil(() => PlayerRead());
            _isPlayerRead = false;
        }

        //stop
        yield return new WaitUntil(() => BinocleMode.activeInHierarchy);
        onStartDie?.Invoke();
        yield return new WaitForSeconds(10);

        for (int i = 0; i < story7.Count; i++)
        {
            onDialogSend?.Invoke(story7[i]);
            yield return new WaitUntil(() => PlayerRead());
            _isPlayerRead = false;
        }

        //more whales dying
        for (int i = 0; i < story8.Count; i++)
        {
            onDialogSend?.Invoke(story8[i]);
            yield return new WaitUntil(() => PlayerRead());
            _isPlayerRead = false;
        }

        yield return new WaitUntil(() => PlayerRead());

        SceneManager.LoadScene(2);


    }

    private bool PlayerRead()
    {
        return _isPlayerRead;
    }

    private void OnDisable()
    {
        if(_gameProgress != null)
            StopCoroutine(_gameProgress);
    }

}
