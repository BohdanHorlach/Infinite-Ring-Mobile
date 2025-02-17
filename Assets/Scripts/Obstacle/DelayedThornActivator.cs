using System.Collections;
using UnityEngine;


public class DelayedThornActivator : MonoBehaviour
{
#nullable enable
    [SerializeField] private DelayedThornActivator? _thornActivator;
#nullable disable

    [SerializeField] private Thorn[] _activatedOnStartThorns;
    [SerializeField] private Thorn[] _delayedActivatedThorns;
    [SerializeField] private float _delay = 2f;
    [SerializeField] private bool _isActivateOnStart = false;
    [SerializeField] private LayerMask _playerMask;


    private void Start()
    {
        if (_isActivateOnStart)
            Run();        
    }


    private void OnTriggerEnter(Collider other)
    {
        LayerMask maskFromObject = 1 << other.gameObject.layer;

        if (maskFromObject == _playerMask)
            Run();
    }


    private void Run()
    {
        ActivateThorn(_activatedOnStartThorns);
        StartCoroutine("DelayActivate", _delayedActivatedThorns);
    }


    private void ActivateThorn(Thorn[] thorns)
    {
        foreach (Thorn thorn in thorns)
            thorn.Activate();
    }


    private void ActivateOtherThorns()
    {
        _thornActivator?.Run();
    }


    private IEnumerator DelayActivate(Thorn[] thorns)
    {
        yield return new WaitForSeconds(_delay);

        ActivateThorn(thorns);
        ActivateOtherThorns();
    }
}
