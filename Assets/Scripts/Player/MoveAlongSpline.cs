using UnityEngine;
using UnityEngine.Splines;


//Used due to strange behavior, or rather too fast movement when changing
//SplineAnimate.MaxSpeed, regardless of whether it decreases or increases
public class MoveAlongeSpline : MonoBehaviour
{
    [SerializeField] private SplineContainer _spline;
    
    private const float SPLINE_LOOK_AHEAD_STEP = 0.001f;

    private Vector3 _startPosition;
    private float _distancePercentage = 0f;
    private float _splineLength;
    
    public bool IsPaused { get; set; }
    public float Speed { get; set; }


    private void Start()
    {
        IsPaused = false;
        _startPosition = transform.position;
        _splineLength = _spline.CalculateLength();
    }


    private void Update()
    {
        if (IsPaused)
            return;

        Move();
    }


    private void Move()
    {
        _distancePercentage += Speed * Time.deltaTime / _splineLength;

        Vector3 currentPosition = _spline.EvaluatePosition(_distancePercentage);
        transform.position = currentPosition;

        if (_distancePercentage > 1f)
        {
            _distancePercentage = 0f;
        }

        Vector3 nextPosition = _spline.EvaluatePosition(_distancePercentage + SPLINE_LOOK_AHEAD_STEP);
        Vector3 direction = nextPosition - currentPosition;
        transform.rotation = Quaternion.LookRotation(direction, transform.up);
    }


    public void Pause()
    {
        IsPaused = true;
    }


    public void Restart()
    {
        transform.position = _startPosition;
        IsPaused = false;
    }
}