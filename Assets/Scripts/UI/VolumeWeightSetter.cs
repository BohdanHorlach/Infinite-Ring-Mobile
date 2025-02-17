using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;


public class VolumeWeightSetter : MonoBehaviour
{
    [SerializeField] private Volume _volume;
    [SerializeField] private float[] _volumeWeightStates;
    [SerializeField] private float _speedTransition;

    private int _indexWeightState = 0;


    private void Start()
    {
        _volume.weight = _volumeWeightStates[_indexWeightState];
    }


    private IEnumerator Transition(float start, float end)
    {
        float elapsedTime = 0;

        while (elapsedTime < _speedTransition)
        {
            elapsedTime += Time.deltaTime;
            float newWeight = Mathf.Lerp(start, end, elapsedTime / _speedTransition);

            _volume.weight = newWeight;
            yield return null;
        }
    }


    private void MakeTransition(int index)
    {
        float newWeight = _volumeWeightStates[index];
        StartCoroutine(Transition(_volume.weight, newWeight));
    }


    public void GoNext()
    {
        if (_indexWeightState == _volumeWeightStates.Length - 1)
            return;

        _indexWeightState++;
        MakeTransition(_indexWeightState);
    }


    public void GoPrev()
    {
        if (_indexWeightState == 0)
            return;

        _indexWeightState--;
        MakeTransition(_indexWeightState);
    }


    public void Restart()
    {
        _indexWeightState = 0;
        _volume.weight = _volumeWeightStates[_indexWeightState];
    }
}