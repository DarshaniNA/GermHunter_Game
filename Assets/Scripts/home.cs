using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class home : MonoBehaviour {

    public GameObject howToPlayPanel;

    // Use this for initialization
    void Start () {
        howToPlayPanel = GameObject.Find("HowPanel");
        howToPlayPanel.SetActive(false);
    }

    public void LoadHowToPlay()
    {
        howToPlayPanel.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        howToPlayPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("game");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
