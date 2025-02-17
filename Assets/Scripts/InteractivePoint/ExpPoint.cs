using UnityEngine;


public class ExpPoint : MonoBehaviour
{
    [SerializeField] private float _expValue;
    [SerializeField] private PointSwitcher _pointSwitcher;


    public float GetExpAndDestroyPoint()
    {
        if (_pointSwitcher.IsEnable == false)
            return 0f;

        _pointSwitcher.DestroyPoint();
        _pointSwitcher.TryEnable();

        return _expValue;
    }
}