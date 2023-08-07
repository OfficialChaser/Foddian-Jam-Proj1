using System;
using System.Collections;
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
        StartCoroutine(ShowFinalTime());
    }

    [ContextMenu("test")]
    private IEnumerator ShowFinalTime()
    {
        float elapsedTime = PlayerPrefs.GetFloat("Recent Time", 0f);
        Debug.Log(elapsedTime);
        int roundedElapsedTime = Mathf.RoundToInt(elapsedTime);
        timePlaying = TimeSpan.FromSeconds(elapsedTime);
        string timePlayingStr = timePlaying.ToString("m':'s'.'ff");
        timeText.text = timePlayingStr;
        yield return new WaitForSeconds(1f);
        StartCoroutine(leaderboard.SubmitScoreRoutine(roundedElapsedTime));
    }

}
