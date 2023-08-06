using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WinScreenUI : MonoBehaviour
{
    // References
    [SerializeField] private TextMeshProUGUI timeText;
    private TimeSpan timePlaying;

    private void Start()
    {
        ShowFinalTime();
    }

    private void ShowFinalTime()
    {
        float elapsedTime = PlayerPrefs.GetFloat("Recent Time", 0f);
        timePlaying = TimeSpan.FromSeconds(elapsedTime);
        string timePlayingStr = timePlaying.ToString("m':'s'.'ff");
        timeText.text = timePlayingStr; 
    }

}
