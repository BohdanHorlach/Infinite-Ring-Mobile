using UnityEngine;
using TMPro;


public class BestScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _output;

    private void Update()
    {
        int score = PlayerPrefs.GetInt("BestScore", 0);
        _output.text = $"Best score: {score}";
    }
}