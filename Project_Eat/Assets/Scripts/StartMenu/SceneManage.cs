using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 切换到游戏场景
public class SceneManage : MonoBehaviour
{
    public string sceneName_Game;
    public GameObject main;
    public GameObject characterSelect;

    public void SwitchtoGameScene()
    {
        SceneManager.LoadScene(sceneName_Game);
    }

    public void SwitchtoMainMenu()
    {
        main.SetActive(true);
        characterSelect.SetActive(false);
    }
}
