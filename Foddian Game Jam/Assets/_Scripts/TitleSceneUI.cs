using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneUI : MonoBehaviour
{
    [SerializeField] private AudioClip menuMusic;

    private void Start()
    {
        SoundManager.Instance.PlayMusic(menuMusic);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void LoadLeaderboardScene()
    {
        SceneManager.LoadScene("Game Win Scene");
    }
}
