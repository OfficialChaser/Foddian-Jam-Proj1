using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private float elapsedTime;
    [SerializeField] private TextMeshProUGUI timerText; 
    private TimeSpan timePlaying;
    private bool timerGoing;

    [SerializeField] private PlayerMovement playerMovement;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        PlayerPrefs.GetFloat("Recent Time", 0f);
        timerText.text = "Time: 00:00.00";
        timerGoing = false;
        BeginTimer();
    }


    public void StartEndSequence()
    {
        Debug.Log("You win");
        timerGoing = false;
        PlayerPrefs.SetFloat("Recent Time", elapsedTime);
        playerMovement.enabled = false;
    }

    // Timer Stuff
    private void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }
    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = timePlaying.ToString("m':'s'.'ff");
            timerText.text = timePlayingStr;

            yield return null;
        }
    }
}
