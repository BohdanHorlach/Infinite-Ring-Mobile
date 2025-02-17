using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class Movement : MonoBehaviour
{
    [SerializeField] private MoveAlongeSpline _splineAnimate;
    [SerializeField] private Transform _pivot;
    [SerializeField] private float _startSpeed = 10f;
    [SerializeField] private float _maxSpeed = 50f;
    [SerializeField] private float _maxHoldTime = 2f;
    [SerializeField] private float _durationOfReduceHoldForce = 1f;
    [SerializeField] private float _accelerationForce = 2f;
    [SerializeField] private float _durationOfReduceSpeed = 2f;
    [SerializeField] private float _rotateForce = 70f;
    [SerializeField] private float _maxShipRotateAngle = 30f;
    [SerializeField] private float _durationToReturnOriginalRotate = 5f;

    private float _holdStartTime;
    private float _holdForce = 0f;
    private float _currentYRotate = 0f;
    private float CurrentSpeed { get => _splineAnimate.Speed; set => _splineAnimate.Speed = value; }
    private bool _isInput = false;
    private bool _tapIsOnRight = false;

    public bool IsPlayed { get; set; }


    private void Start()
    {
        IsPlayed = false;
        CurrentSpeed = _startSpeed;
    }


    private void Update()
    {
        Rotate();

        if (IsPlayed == false)
            return;

        UpdateHoldForce();
        Acceleration();
    }


    private void UpdateHoldForce()
    {
        if (_isInput)
        {
            float holdDuration = Time.time - _holdStartTime;
            _holdForce = Mathf.Clamp01(holdDuration / _maxHoldTime);
        }
    }


    private void Acceleration()
    {
        if (CurrentSpeed >= _maxSpeed)
            return;

        CurrentSpeed = Mathf.Clamp(CurrentSpeed + _accelerationForce, _startSpeed, _maxSpeed);
    }


    private void RotatePivot(float rotate)
    {
        _pivot.RotateAround(_pivot.position, _pivot.forward, rotate);
    }


    private void RotateShip(float rotate)
    {
        if(_isInput == true)
        {
            _currentYRotate += rotate;
            _currentYRotate = Mathf.Clamp(_currentYRotate, -_maxShipRotateAngle, _maxShipRotateAngle);
        }
        else
        {
            _currentYRotate = Mathf.Lerp(_currentYRotate, 0f, Time.deltaTime * _durationToReturnOriginalRotate);
        }
        transform.localRotation = Quaternion.Euler(0, 0, -_currentYRotate);
    }


    private void Rotate()
    {
        float rotate = _holdForce * _rotateForce * Time.deltaTime;

        rotate *= _tapIsOnRight ? 1 : -1;

        RotatePivot(rotate);
        RotateShip(rotate);
    }


    private IEnumerator SmoothlyReduceHoldForce()
    {
        float startValue = _holdForce;
        float elapsedTime = 0;

        while (_holdForce > 0)
        {
            elapsedTime += Time.deltaTime;
            _holdForce = Mathf.Lerp(startValue, 0, elapsedTime / _durationOfReduceHoldForce);
            yield return null;
        }

        _holdForce = 0;
    }


    private IEnumerator ReduceSpeedToZero()
    {
        float initialSpeed = CurrentSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < _durationOfReduceSpeed)
        {
            elapsedTime += Time.deltaTime;
            CurrentSpeed = Mathf.Lerp(initialSpeed, 0f, elapsedTime / _durationOfReduceSpeed);
            yield return null;
        }

        CurrentSpeed = 0f;
        _splineAnimate.Pause();
    }


    public void ReadHold(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StopCoroutine("SmoothlyReduceHoldForce");
            _holdStartTime = Time.time;
            _isInput = true;
        }
        else if (context.canceled)
        {
            _isInput = false;
            StartCoroutine("SmoothlyReduceHoldForce");
        }
    }


    public void ReadPositionTap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 touchPosition = context.ReadValue<Vector2>();
            float screenWidth = Screen.width;

            _tapIsOnRight = touchPosition.x > screenWidth / 2;
        }
    }


    public void Restart()
    {
        _splineAnimate.Restart();
        CurrentSpeed = _startSpeed;
    }


    public void Stop()
    {
        StartCoroutine("ReduceSpeedToZero");
        IsPlayed = false;
    }
}
