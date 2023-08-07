using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreenUI : MonoBehaviour
{
    // References
    [SerializeField] private Leaderboard leaderboard;
    [SerializeField] private TextMeshProUGUI timeText;
    private TimeSpan timePlaying;
    [SerializeField] private AudioClip winMusic;
    [SerializeField] private AudioClip menuMusic;

    private void Start()
    {
        StartCoroutine(ShowFinalTime());
        StartCoroutine(MusicEndSequence());
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

    private IEnumerator MusicEndSequence()
    {   
        SoundManager.Instance.PlaySound(winMusic);
        yield return new WaitForSeconds(8f);
        SoundManager.Instance.PlayMusic(menuMusic);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Title Scene");
    }

}
