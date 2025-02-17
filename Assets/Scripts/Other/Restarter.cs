using UnityEngine;


public class Restarter : MonoBehaviour
{
    [SerializeField] private Movement _playerMovement;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private VolumeWeightSetter _volumeWeightSetter;
    [SerializeField] private ObstacleSwitcher[] _obstacleSwitchers;
    [SerializeField] private PointSwitcher[] _pointSwitchers;


    private void DisableSwitcher()
    {
        foreach (ObstacleSwitcher switcher in _obstacleSwitchers)
        {
            switcher.DisableActiveObstacle();
        }
    }

    
    private void DisableExpPoints() 
    {
        foreach (PointSwitcher switcher in _pointSwitchers)
        {
            switcher.DisablePoint();
        }
    }


    //Invoked from anim
    public void Restart()
    {
        _playerMovement.Restart();
        _playerHealth.Restart();
        _volumeWeightSetter.Restart();
        DisableSwitcher();
        DisableExpPoints();
    }
}