using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    public GameObject pausePanel;

    private void Start()
    {
        pausePanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausePanel.activeInHierarchy)
            {
                PauseGame();
            }
            if (pausePanel.activeInHierarchy)
            {
                ContinueGame();
            }
        }
    }

    public void LoadStart() {
        //SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
        SceneManager.LoadScene("Start");
        print("clicked home");
    }

    public void PauseGame()
    {
        //SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }
    public void ContinueGame()
    {
       // SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        //enable the scripts again
    }
}
