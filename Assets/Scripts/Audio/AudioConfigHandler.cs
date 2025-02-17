using UnityEngine;
using UnityEngine.Events;


public class AudioConfigHandler : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audioSources;
    [SerializeField] private string _name;


    ///<returns>AudioSource is enabled</returns>
    public UnityEvent<bool> OnConfigLoaded;


    private void Awake()
    {
        int toggleButtonValue = PlayerPrefs.GetInt(_name, 1);

        bool isEnabled = toggleButtonValue == 1;
        OnConfigLoaded.Invoke(isEnabled);
        SetActiveToAudioSource(isEnabled);
    }


    private void SetActiveToAudioSource(bool isEnabled)
    {
        foreach (AudioSource source in _audioSources) 
            source.mute = isEnabled;
    }


    private void SetConfigValue(bool isEnabled)
    {
        PlayerPrefs.SetInt(_name, isEnabled ? 1 : 0);
        PlayerPrefs.Save();
    }


    public void SetNewValue(bool isEnabled)
    {
        SetActiveToAudioSource(isEnabled);
        SetConfigValue(isEnabled);
    }
}