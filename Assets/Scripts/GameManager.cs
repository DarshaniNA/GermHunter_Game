using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.IO;

public class GameManager : MonoBehaviour {

	public static GameManager Instance { get; set; }

    public GameObject pauseButton;
    public bool isDead { get; set; }
    private bool isGameStarted = false;
    private PlayerController playerController;

    //UI and the UI fields
    public Animator gameCanvas, menuAnim, diamondAnim;
    public Text scoreText, coinText, modifierText, hiScoreText;
    private float score, coinScore, modifierScore, time;
    private int lastScore;
    private PlayerObject playerValues;
    //private JsonData userItem;

    //Death Menu
    public Animator deathMenuAnim;
    public Text deadScoreText, deadCoinText;

    private void Awake()
    {
        Instance = this;
        modifierScore = 1;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        modifierText.text = "x" + modifierScore.ToString("0.0");
        coinText.text = coinScore.ToString("0");
        scoreText.text = scoreText.text = score.ToString("0");

        hiScoreText.text = PlayerPrefs.GetInt("Hiscore").ToString();
    }

    private void Update()
    {
        if (MobileInput.Instance.Tap && !isGameStarted)
        {
            isGameStarted = true;
            playerController.StartRunning();
            FindObjectOfType<BackgroundSpawner>().IsScrolling = true;
            FindObjectOfType<CameraController>().IsMoving = true;
            gameCanvas.SetTrigger("Show");
            menuAnim.SetTrigger("Hide");

        }

        if (isGameStarted && !isDead)
        {
            //score up
            score += (Time.deltaTime * modifierScore);
            time += (Time.deltaTime);
            if (lastScore != (int)score)
            {
                lastScore = (int)score;
                scoreText.text = score.ToString("0");
            }
        }
    }

    public void GetCoin()
    {
        diamondAnim.SetTrigger("Collect");
        //SoundManager.Instance.PlaySound(Constant.SOUND_PICKUP);
        coinScore ++;
        coinText.text = coinScore.ToString("0");
        score += Constant.COIN_SCORE_AMOUNT;
        scoreText.text = scoreText.text = score.ToString("0");
    }

    //public void UpdateScores()
    //{
    //    scoreText.text = score.ToString();
    //    coinText.text = coinScore.ToString();
    //    modifierText.text = "x" + modifierScore.ToString("0.0");
    //}

    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");
    }

    public void OnPlayButton()
    {
        print("clicked");
        //SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GermHunter");
    
    }

    public void OnDeath()
    {
        isDead = true;
        FindObjectOfType<BackgroundSpawner>().IsScrolling = false;
        deadScoreText.text = score.ToString("0");
        deadCoinText.text = coinScore.ToString("0");
        pauseButton.SetActive(false);
        //SoundManager.Instance.PlaySound(Constant.SOUND_PLAYER_ATTACK);
        deathMenuAnim.SetTrigger("Dead");
        gameCanvas.SetTrigger("Hide");

        //check if this is a highscore
        if (score > PlayerPrefs.GetInt("Hiscore"))
        {
            float s = score;
            if (s % 1 == 0)
                s += 1;
            PlayerPrefs.SetInt("Hiscore", (int)s);
        }
        StartCoroutine(SendScore((int)score, (int)coinScore));
    }

    IEnumerator SendScore(int score, int coinScore)
    {
        WWW www;
       
        Hashtable postHeader = new Hashtable();
        postHeader.Add("Content-Type", "application/json");
        PlayerObject player = PlayerPrefsUtil.LoadPLayer();
        ScoreObject postScoreData = new ScoreObject();
        postScoreData.Id = 1;
        postScoreData.PlayerId = player.Id;
        postScoreData.PlayerName = player.Name;
        postScoreData.LevelId = 4;
        postScoreData.LevelName = "Basic";
        postScoreData.Time = "2019-05-19T02:37:36.725Z";
        postScoreData.Pickups = coinScore;
        postScoreData.Points = score;
        postScoreData.TimeRun = score;
       
        var jsonData = JsonMapper.ToJson(postScoreData);
        print("score data " + jsonData.ToString());
        //var formData = System.Text.Encoding.UTF8.GetBytes("{'Username':'" + userName.text
        //    + "', 'Password':'" + userPassword.text + "'}");
        if (Constant.CheckNetworkAvailability())
        {
            www = new WWW(Constant.SCORE_URL, System.Text.Encoding.UTF8.GetBytes(jsonData), postHeader);

            yield return www;
            if (www.text.Equals("null"))
            {
                print("error error !!!");
            }
            else
            {
                Debug.Log("request success");
                print(www.text);
            }
        }
        else
        {
            SSTools.ShowMessage("No Network Connection", SSTools.Position.bottom, SSTools.Time.oneSecond);
        }
    }

    [Serializable]
    public class ScoreObject
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int LevelId { get; set; }
        public string LevelName { get; set; }
        public string Time { get; set; }
        public int Pickups { get; set; }
        public int Points { get; set; }
        public int TimeRun { get; set; }
    }
    //{"Id":37,"Name":"dinushi","Email":"dinushi@gmail.com","GamerTag":"dinushi",
    //"Gender":"f","Age":"23","Password":null,"IsActive":false}
}
