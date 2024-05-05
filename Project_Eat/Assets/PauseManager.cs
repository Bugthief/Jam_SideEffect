using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public string sceneName_MainMenu;

    void Start()
    {
        // 初始化: 隐藏暂停菜单
        pauseMenu.SetActive(false);
    }


    public void Settings()
    {
        //显示暂停的设置菜单
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        // 从暂停菜单返回游戏
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        // 返回开始菜单
        //GameManager.Instance.DestroyInstance();
        SceneManager.LoadScene(sceneName_MainMenu);
    }
}
