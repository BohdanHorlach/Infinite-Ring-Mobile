using UnityEngine;
using UnityEngine.Events;


public class ObjectFinderByMask : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    public UnityEvent OnObjectDetect;


    private void OnTriggerEnter(Collider other)
    {
        LayerMask maskFromObject = 1 << other.gameObject.layer;

        if (maskFromObject == _layerMask)
            OnObjectDetect.Invoke();
    }
}