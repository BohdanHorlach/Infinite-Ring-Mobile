using System.Collections;
using UnityEngine;
using static System.TimeZoneInfo;


public class AudioTransition : MonoBehaviour
{
    [SerializeField] private AudioLowPassFilter[] _audioLowPassFilters;
    [SerializeField] private float _min = 550f;
    [SerializeField] private float _max = 22000f;
    [SerializeField] private float _speedTransition = 5f;

    private bool _currentValueIsMin = true;


    private IEnumerator Transition(AudioLowPassFilter audioLowPassFilter, float start, float end)
    {
        float elapsedTime = 0;

        while (elapsedTime < _speedTransition)
        {
            elapsedTime += Time.deltaTime;
            float newValue = Mathf.Lerp(start, end, elapsedTime / _speedTransition);

            audioLowPassFilter.cutoffFrequency = newValue;
            yield return null;
        }
    }


    public void Transition()
    {
        float min = _currentValueIsMin ? _min : _max;
        float max = _currentValueIsMin ? _max : _min;

        foreach (AudioLowPassFilter filter in _audioLowPassFilters)
        {
            StartCoroutine(Transition(filter, min, max));
        }

        _currentValueIsMin = !_currentValueIsMin;
    }
}
