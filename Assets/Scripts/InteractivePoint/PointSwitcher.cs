using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class PointSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _point;
    [SerializeField] private MoveOnRing _moveOnRing;
    [SerializeField] private Collider _collider;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField, Range(0.1f, 1)] private float _chanceOfEnable;
    [SerializeField] private float _delayOnEnable = 3f;

    public bool IsEnable { get; set; }


    private void Start()
    {
        DisablePoint();
    }


    private IEnumerator DelayedEnable()
    {
        yield return new WaitForSeconds(_delayOnEnable);

        _point.SetActive(true);
        IsEnable = true;
        _collider.enabled = true;
        _moveOnRing.enabled = false;
    }


    public void TryEnable()
    {
        if (IsEnable == true)
            return;

        if (Random.value <= _chanceOfEnable)
            StartCoroutine("DelayedEnable");
    }



    public void DestroyPoint()
    {
        if (IsEnable == false)
            return;

        _particleSystem.Play();
        _collider.enabled = false;
        DisablePoint();
    }


    public void DisablePoint() {
        IsEnable = false;
        _moveOnRing.enabled = true;
        _point.SetActive(false);
    }
}
