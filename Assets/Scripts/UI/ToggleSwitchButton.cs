using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ToggleSwitchButton : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _background;
    [SerializeField] private Color _disableColor;
    [SerializeField] private Color _enableColor;
    [SerializeField] private float _duration;

    private int _currentState = 0;

    public UnityEvent<bool> OnStateChanged;


    private void Start()
    {
        _slider.value = _currentState;
    }


    public void Switch()
    {
        int nextState = _currentState == 0 ? 1 : 0;
        Color nextColor = _currentState == 0 ? _enableColor : _disableColor;

        DOVirtual.Float(_slider.value, nextState, _duration, (value) => { _slider.value = value; });
        DOVirtual.Color(_background.color, nextColor, _duration, (value) => { _background.color = value; });
        _currentState = nextState;

        OnStateChanged.Invoke(_currentState == 1);
    }


    public void OnConfigLoaded(bool isEnabled)
    {
        _currentState = isEnabled ? 1 : 0;
        _background.color = isEnabled ? _enableColor : _disableColor;
    }
}