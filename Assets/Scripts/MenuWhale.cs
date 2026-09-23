using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWhale : MonoBehaviour
{

    [SerializeField]
    public bool IsMyDestenyToBeSick;
    [SerializeField]
    Transform EyeParent;

    private List<GameObject> _myEyes = new List<GameObject>();
    private Coroutine _showEyes;
    private Coroutine _startDie;
    private Animator _animator;

    private void OnEnable()
    {
        SetUnique();
        HandleStartDecay();
        //GameProgress.onStartDecay += HandleStartDecay;
    }

    private void SetUnique()
    {
        var speed = Random.Range(0.22f, 0.35f);
        var scale = Random.Range(0.7f, 1.2f);

        _animator = GetComponent<Animator>();
        _animator.speed = speed;
        this.transform.localScale = new Vector3(scale, scale, scale);
        Debug.Log("SetUnique, speed: " + speed + ", scale = " + scale);

    }

    private void HandleStartDecay()
    {
        if (IsMyDestenyToBeSick)
        {
            CollectEyes();
            _showEyes = StartCoroutine(StartDecay());
        }
    }

    private void CollectEyes()
    {
        foreach (Transform child in EyeParent)
        {
            if (child.gameObject.tag == tag)
            {
                _myEyes.Add(child.gameObject);
            }
        }
    }

    private IEnumerator StartDecay()
    {
        yield return new WaitForSeconds(2);

        int howMuchEyes = Random.Range(5, 13);

        Debug.Log("DECAY Started, eyes to spawn: " + howMuchEyes + "!!!!");


        for (int i = 0; i < howMuchEyes; i++)
        {
            int randomEye = Random.Range(0, _myEyes.Count);

            _myEyes[randomEye].SetActive(true);
            _myEyes.Remove(_myEyes[randomEye]);
            yield return new WaitForSeconds(5);
        }

        yield return new WaitForSeconds(5);
        _animator.speed = 2f;
        _animator.SetBool("Die", true);
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;

        Debug.Log("DECAY FINISHED!!!!");
    }

    //private void Die()
    //{
    //    _startDie = StartCoroutine(StartDie());
    //}

    //private IEnumerator StartDie()
    //{
    //    int timeToDie = Random.Range(1, 15);
    //    yield return new WaitForSeconds(timeToDie);
    //    _animator.speed = 2f;
    //    _animator.SetBool("Die", true);
    //    this.gameObject.GetComponent<Rigidbody>().useGravity = true;
    //}

    private void OnDisable()
    {
        if (_showEyes != null)
            StopCoroutine(_showEyes);
        if (_startDie != null)
            StopCoroutine(_startDie);

        //GameProgress.onStartDecay -= HandleStartDecay;
        //GameProgress.onStartDie -= Die;
    }
}
