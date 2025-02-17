using System.Collections;
using UnityEngine;
using TMPro;


public class Counter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _output;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _startUpDelay = 10f;
    [SerializeField] private float _accumulationSpeed = 0.01f;

    private float _counter = 0;
    private bool _isRunning = false;


    private void Start()
    {
        ClearOutput();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ExpPoint expPoint))
        {
            float exp = expPoint.GetExpAndDestroyPoint();

            if (exp > 0)
            {
                _counter += exp;
                _audioSource.Play();
            }
        }
    }


    private void Update()
    {
        if (_isRunning == false)
            return;

        _counter = _counter + _accumulationSpeed;
        _output.text = Mathf.RoundToInt(_counter).ToString();
    }


    private IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(_startUpDelay);

        _counter = 0;
        _isRunning = true;
    }


    private void SetScore()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        int currentScore = Mathf.RoundToInt(_counter);

        if (currentScore > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
            PlayerPrefs.Save();
        }
    }


    private void ClearOutput()
    {
        _output.text = "";
    }


    public void EnableCounter()
    {
        ClearOutput();
        StartCoroutine("DelayStart");
    }


    public void StopCount()
    {
        _isRunning = false;
        SetScore();
    }
}