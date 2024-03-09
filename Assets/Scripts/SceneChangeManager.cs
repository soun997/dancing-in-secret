using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    
    #region Singleton
    private static SceneChangeManager _sceneManager;

    public static SceneChangeManager SCENE
    {
        get { return _sceneManager; }
    }

    private void Awake()
    {
        _sceneManager = GetComponent<SceneChangeManager>();
        DontDestroyOnLoad(gameObject);
    }
    #endregion


    public void GameStart()
    {
        SoundManager.instance.Btn_Click_Open();
        SceneManager.LoadScene("GameScene");
    }
}
