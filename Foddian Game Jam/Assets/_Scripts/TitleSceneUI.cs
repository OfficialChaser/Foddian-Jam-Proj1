using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneUI : MonoBehaviour
{
    public void LoadGame()
    {
        Transition.Instance.PlayTransition("Start Transition");
        SceneManager.LoadScene("Game Scene");
    }

    public void LoadLeaderboardScene()
    {
        SceneManager.LoadScene("Game Win Scene");
    }
}
