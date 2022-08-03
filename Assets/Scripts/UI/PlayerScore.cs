using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    TextMeshProUGUI playerScoreTMP;
    // Start is called before the first frame update
    void Start()
    {
        playerScoreTMP = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        BodyHandler.onBodyCountChange += UpdateScore;
    }

    private void OnDisable()
    {
        BodyHandler.onBodyCountChange -= UpdateScore;
    }

    private void UpdateScore(int bodyCount)
    {
        playerScoreTMP.text = $"Score {bodyCount}";
    }
}
