using UnityEngine;


public class Thorn : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isActivateOnStart = false;


    private void Start()
    {
        if(_isActivateOnStart)
            Activate();
    }


    public void Activate()
    {
        _animator.SetBool("isActivated", true);
    }
}
