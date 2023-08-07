using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WinScreenUI : MonoBehaviour
{
    // References
    [SerializeField] private Leaderboard leaderboard;
    [SerializeField] private TextMeshProUGUI timeText;
    private TimeSpan timePlaying;

    private void Start()
    {
        ShowFinalTime();
    }

    [ContextMenu("test")]
    private void ShowFinalTime()
    {
        float elapsedTime = PlayerPrefs.GetFloat("Recent Time", 0f);
        Debug.Log(elapsedTime);
        int roundedElapsedTime = Mathf.RoundToInt(elapsedTime);
        timePlaying = TimeSpan.FromSeconds(elapsedTime);
        string timePlayingStr = timePlaying.ToString("m':'s'.'ff");
        timeText.text = timePlayingStr;
        leaderboard.SubmitScoreRoutine(roundedElapsedTime);
    }

}
