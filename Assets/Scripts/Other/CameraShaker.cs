using UnityEngine;
using DG.Tweening;


public class CameraShaker : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _shakeDuration = 0.5f;
    [SerializeField] private float _strength = 1f;
    [SerializeField] private int _vibrato = 10;
    [SerializeField] private float _randomness = 90f;
    [SerializeField] private float _returnDuration = 1f;


    private Vector3 _initialPosition;
    private bool _isShaking = false;


    private void ReturnToDefaultPosition()
    {
        _mainCamera.transform
            .DOLocalMove(_initialPosition, _returnDuration)
            .OnComplete(() => { _isShaking = false; });
    }


    public void Shake()
    {
        if (_mainCamera != null && _isShaking == false)
        {
            _initialPosition = _mainCamera.transform.localPosition;
            _isShaking = true;

            _mainCamera.transform.DOShakePosition(_shakeDuration, _strength, _vibrato, _randomness).
                OnComplete(ReturnToDefaultPosition);
        }
    }
}
