using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealthPoint;
    [SerializeField] private float _cooldown = 3f;
    [SerializeField] private LayerMask _healthPointMask;

    private int _currentHealth;
    private bool _isDeath = false;
    private bool _isRecharging = false;

    public UnityEvent OnAddHealth;
    public UnityEvent OnTakeDamage;
    public UnityEvent OnDeath;


    private void Start()
    {
        _currentHealth = _maxHealthPoint;
    }


    private void OnTriggerEnter(Collider other)
    {
        LayerMask maskFromObject = 1 << other.gameObject.layer;

        if (maskFromObject == _healthPointMask 
            && other.TryGetComponent(out PointSwitcher healthPointSwitcher) 
            && healthPointSwitcher.IsEnable 
            && _isDeath == false)
        {
            AddHealth();
            healthPointSwitcher.DestroyPoint();
            healthPointSwitcher.TryEnable();
            OnAddHealth.Invoke();
        }
    }


    private void AddHealth()
    {
        if (_currentHealth >= _maxHealthPoint)
            return;

        _currentHealth++;
    }


    private IEnumerator Recharge()
    {
        _isRecharging = true;
        yield return new WaitForSeconds(_cooldown);
        _isRecharging = false;
    }


    public void TakeDamage()
    {
        if (_isDeath || _isRecharging)
            return;

        _currentHealth--;

        OnTakeDamage.Invoke();
        StartCoroutine("Recharge");

        if (_currentHealth == 0)
        {
            _isDeath = true;
            OnDeath.Invoke();
        }
    }


    public void Restart()
    {
        _currentHealth = _maxHealthPoint;
        _isDeath = false;
    }
}