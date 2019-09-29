using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class player : MonoBehaviour
{

    public Animator anim;
    public AudioSource aud;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public Text scoreText;
    public Text timmerVal;
    // reference to panel
    public GameObject GameOverPanel;
    public GameObject GameWinPanel;
    public Text gameOverGems;
    public Text gameWinGems;


    private float lane = 0;
    private float curretSec = 0f;
    private float minutes;
    private float seconds;

    public static int score = 0;

    bool Jumping = false;


    public float speed = 0.1f;
    public GameObject animatorObject;
    Animator animator;

    // Use this for initialization
    void Start()
    {
        speed = 0.15f;
        anim.speed = 1;
        score = 0;
        GameOverPanel = GameObject.Find("GameOverPanel");
        GameWinPanel = GameObject.Find("GameWinPanel");
        GameOverPanel.SetActive(false);
        GameWinPanel.SetActive(false);
        Time.timeScale = 1;
        animator = animatorObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float xDiff = lane - transform.position.x;
        transform.Translate(new Vector3(xDiff, 0, 1) * speed);
        ProcessInput();
        curretSec += Time.deltaTime;
        minutes = Mathf.Floor(curretSec / 60);
        seconds = Mathf.RoundToInt(curretSec % 60);
        timmerVal.text = minutes + ":" + seconds;
        if (score == 20)
        {
            ActivateGameWin();
        }
    }

    private void ActivateGameWin()
    {
        Time.timeScale = 0;
        gameWinGems.text = score.ToString();
        GameWinPanel.SetActive(true);
    }

    public void StartGame()
    {
        speed = 0.3f;
        anim.speed = 1;
    }


    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("Left");
            if (lane == 0 || lane == 2)
            {
                //transform.Translate (new Vector3 (-2, 0, 0));
                lane = lane - 2;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (lane == 0 || lane == -2)
            {
                //transform.Translate (new Vector3 (2, 0, 0));
                lane = lane + 2;
            }
            Debug.Log("Right");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // anim.SetTrigger("JumpTrigger");
            animator.SetInteger("State", 1);

            Debug.Log("Jump");
            aud.PlayOneShot(jumpSound);
            //StartCoroutine (JumpWait (0.5f));
        }
    }

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Gem")
        {
            score++;
            scoreText.text = score.ToString();
            StartCoroutine(HideWait(5f, col.gameObject));
            Debug.Log("Gem Collected" + score);
        }

        if (Jumping)
        {
            return;
        }

        if (col.gameObject.tag == "Obstacle")
        {
            Debug.Log("Collided");
            aud.PlayOneShot(crashSound);
            anim.SetBool("Crashed", true);
            speed = 0;
            ActivatedGameOver();
        }
    }

    private void ActivatedGameOver()
    {
        Time.timeScale = 0;
        gameOverGems.text = score.ToString();
        GameOverPanel.SetActive(true);
    }

    public void loadHome()
    {
        SceneManager.LoadScene("home");
        score = 0;
        GameOverPanel.SetActive(false);
        GameWinPanel.SetActive(false);
    }

    public void loadThis()
    {
        SceneManager.LoadScene("game");
        score = 0;
        GameOverPanel.SetActive(false);
        GameWinPanel.SetActive(false);
    }
    //	public void Retry(){
    //		ScreenManager.LoadScreen("Game");
    //	}

    IEnumerator JumpWait(float time)
    {
        Jumping = true;
        yield return new WaitForSeconds(time);
        Jumping = false;
    }

    IEnumerator HideWait(float time, GameObject obj)
    {
        obj.SetActive(false);
        yield return new WaitForSeconds(time);
        obj.SetActive(true);

    }
}
