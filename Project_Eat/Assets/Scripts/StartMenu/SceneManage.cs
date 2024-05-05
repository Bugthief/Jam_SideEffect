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
    void Start()
    {
        // 初始化：隐藏角色选择，显示主菜单
        main.SetActive(true);
        characterSelect.SetActive(false);
        Time.timeScale = 1f;
    }

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
