using UnityEngine;


public class ObstacleSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstacles;

#nullable enable
    private GameObject? _activeObstacle;
#nullable disable

    private void Start()
    {
        DisableAllObstacles();
    }


    private void DisableAllObstacles()
    {
        foreach (GameObject obstacle in _obstacles)
            obstacle.SetActive(false);
    }


    public void SwitchObstacle()
    {
        _activeObstacle?.SetActive(false);

        int index = UnityEngine.Random.Range(0, _obstacles.Length);
        GameObject obstacle = _obstacles[index];

        obstacle.SetActive(true);
        _activeObstacle = obstacle;
    }


    public void DisableActiveObstacle()
    {
        _activeObstacle?.SetActive(false);
    }
}