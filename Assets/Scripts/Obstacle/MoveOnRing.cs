using UnityEngine;


public class MoveOnRing : MonoBehaviour
{
    [SerializeField] private Transform _pivot;
    [SerializeField] private float _minRotateForce = 1f;
    [SerializeField] private float _maxrotateForce = 1f;


    private float _rotateForce;


    private void Start()
    {
        _rotateForce = Random.Range(_minRotateForce, _maxrotateForce);
    }


    public void Update()
    {
        _pivot.RotateAround(_pivot.position, _pivot.forward, _rotateForce);
    }
}