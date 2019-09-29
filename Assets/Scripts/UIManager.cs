using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Networking;
using LitJson;

public class UIManager : MonoBehaviour
{
    //edited
    public GameObject homePanel;
    public GameObject loginPanel;
    public GameObject registerPanel;
    public GameObject leaderboard;
    public GameObject landingPanel;
    public GameObject mainLandingPanel;
    public GameObject howtoPlayPanel;
    public GameObject aboutPanel;
    public GameObject SettingsPanel;
    public GameObject TutorialPanel;
    public GameObject mainContainerView;
    public GameObject contentView;
    public GameObject deathMenu;
    public GameObject startMenu;
    public Text loginSucessText;
    public Text invalidDataText;
    public Text email;
    public Text password;

    public InputField userName;
    public InputField userEmail;
    public InputField userNikName;
    public InputField passord;
    public InputField age;
    public InputField gender;
    public InputField experiance;

    public Text leaderBoardName;
    public Text leaderBoardPoints;
    public Text leaderBoardTutePoints;
    public Text titleText;
    public Text noRecordText;

    List<LeaderboardObject> winnerList;

    //public GameObject rowItemPrefab;
    //public int itemCount = 10, columnCount = 1;

    void Start()
    {
        homePanel = GameObject.Find(Constant.PANEL_HOME);
        loginPanel = GameObject.Find(Constant.PANEL_LOGIN);
        registerPanel = GameObject.Find(Constant.PANEL_REGISTER);
        loginSucessText.enabled = false;
        invalidDataText.gameObject.SetActive(false);
        noRecordText.gameObject.SetActive(false);

        homePanel.SetActive(true);
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
        leaderboard.SetActive(false);
        howtoPlayPanel.SetActive(false);
        aboutPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        TutorialPanel.SetActive(false);
        //deathMenu.SetActive(false);
        //startMenu.SetActive(false);
    }


    public void ActiveLogin()
    {
        PlayerObject player = PlayerPrefsUtil.LoadPLayer();
        if (player != null)
        {
            mainLandingPanel.SetActive(true);
            landingPanel.SetActive(true);
            titleText.text = "Hi " + player.Name.ToString();
        }
        else
        {
            //SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
            loginPanel.SetActive(true);
            homePanel.SetActive(true);
            registerPanel.SetActive(false);
        }
    }
    public void DeactiveLogin()
    {
        //SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
        homePanel.SetActive(true);
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
    }

    public void ActiveSignin()
    {
        //SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
        registerPanel.SetActive(true);
        homePanel.SetActive(true);
        loginPanel.SetActive(false);
    }

    public void DeactiveSignin()
    {
        //SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
        loginPanel.SetActive(true);
        homePanel.SetActive(true);
        registerPanel.SetActive(false);
    }
    public void LoadLevel(string levelName)
    {
        //SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
        SceneManager.LoadScene(levelName);
    }
    public void ActivateHowtoPlay()
    {
        //SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
        howtoPlayPanel.SetActive(true);
    }

    public void DeactivateHowtoPlay()
    {
        //SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
        howtoPlayPanel.SetActive(false);
    }

    public void ActivateSettings()
    {
        //SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
        SettingsPanel.SetActive(true);
    }

    public void DeactivateSettings()
    {
        
        SettingsPanel.SetActive(false);
        //SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
    }

    public void ActivateTutorial()
    {
       // SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
        TutorialPanel.SetActive(true);
    }

    public void DeactivateTutorial()
    {
        //SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
        TutorialPanel.SetActive(false);
    }

    public void ActivateAbout()
    {
        //SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
        aboutPanel.SetActive(true);
    }

    public void DeactivateAbout()
    {
        //SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
        aboutPanel.SetActive(false);
    }
    public void ActiveLeaderBord()
    {
        //SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
        leaderboard.SetActive(true);
        landingPanel.SetActive(false);
        StartCoroutine(LoadLeaderboard());
    }

    IEnumerator LoadLeaderboard()
    {
        //const string LOGIN_URL = "http://gamerdata.gear.host/api/Score";
        WWW www;
        www = new WWW(Constant.SCORE_URL);

        yield return www;
        if (www.error != null)
        {
            // alertText.text = "Check Your User Name Password";
        }
        else
        {
            Debug.Log("request success");
            var userData = JsonMapper.ToObject<List<LeaderboardObject>>(www.text);
            setLeaderboardData(userData);
            //foreach (var score in userData)
            //{
            //    leaderBoardName.text =score.PlayerName;
            //    leaderBoardPoints.text =score.Points.ToString();
            //    leaderBoardTutePoints.text =score.Pickups.ToString();
            //}
         
        }


    }

    private void setLeaderboardData(List<LeaderboardObject> userData)
    {
        if (userData.Count > 0) {
            int space = 0;
            noRecordText.gameObject.SetActive(false);
            RectTransform rt = (RectTransform)mainContainerView.transform;
            foreach (var score in userData)
            //for (int i = 0; i < userData.Count; i++)
            {
                int totalscore = score.Points + score.Pickups;
                int totaltime = score.Points;
                space += 100;
                GameObject listItem = (GameObject)Instantiate(contentView);
                //RectTransform rowRectTransform = listItem.GetComponent<RectTransform>();
                listItem.transform.SetParent(mainContainerView.transform);
                listItem.transform.localScale = new Vector3(1, 1, 1);
                listItem.transform.localPosition = new Vector3(0, -space, 0);
                listItem.transform.Find("NameText").GetComponent<Text>().text = score.Pickups.ToString();
                listItem.transform.Find("ScoreText").GetComponent<Text>().text = totalscore.ToString();
                listItem.transform.Find("TuteScoreText").GetComponent<Text>().text = score.PlayerName;
                listItem.transform.Find("TimeText").GetComponent<Text>().text = totaltime.ToString();
            }

        }
        else
        {
            noRecordText.gameObject.SetActive(true);
        }
    }

    public void DeactiveLeaderBord()
    {
        //SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
        leaderboard.SetActive(false);
        landingPanel.SetActive(true);
    }
    public void LoadAR()
    {
        //SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
        SceneManager.LoadScene("AR");
    }
    public void LoadHome()
    {
        //SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
        mainLandingPanel.SetActive(false);
        loginPanel.SetActive(false);
    }

    public void POST()
    {
        if (userName.text == "" || userEmail.text == "" || userNikName.text == "" || passord.text == "" || gender.text == "")
        {
            invalidDataText.gameObject.SetActive(true);
            StartCoroutine(DeactivateTextl());
        }
        else
        {

            StartCoroutine(SendData());
        }
    }


    IEnumerator SendData()
    {
        //const string POST_DATA_URL = "http://gamerdata.gear.host/api/Players";
        WWW www;
        Hashtable postHeader = new Hashtable();
        postHeader.Add("Content-Type", "application/json");

        var newPlayer = new PlayerObject();
        newPlayer.Name = userName.text;
        newPlayer.Email = userEmail.text;
        newPlayer.GamerTag = userNikName.text;
        newPlayer.Password = passord.text;
        newPlayer.Age = age.text;
        newPlayer.Gender = gender.text;

        // var jsonData = JsonUtility.ToJson(newPlayer);
        // JsonMapper.ToObject<>

        var jsonData = JsonMapper.ToJson(newPlayer);

        //var formData = System.Text.Encoding.UTF8.GetBytes("{'Name':'" + userName.text
        //    + "', 'Email':'" + userEmail.text
        //    + "', 'GamerTag':'" + userNikName.text
        //    + "', 'Age':'" + age.text
        //    + "', 'Gender':'" + gender.text
        //    + "', 'Password':'" + passord.text + "'}");

        if (Constant.CheckNetworkAvailability())
        {
            www = new WWW(Constant.POST_DATA_URL, System.Text.Encoding.UTF8.GetBytes(jsonData), postHeader);

            yield return www;
            if (www.error != null)
            {
                Debug.Log("request error: " + www.error);
            }
            else
            {
                Debug.Log("request success");
                loginSucessText.enabled = true;
                loginSucessText.text = "Registration Sucess";

                userName.gameObject.SetActive(false);
                userEmail.gameObject.SetActive(false);
                userNikName.gameObject.SetActive(false);
                passord.gameObject.SetActive(false);
                age.gameObject.SetActive(false);
                gender.gameObject.SetActive(false);
                experiance.gameObject.SetActive(false);
                StartCoroutine(DeactivatePanel());
                StartCoroutine(ActiveFeilds());
            }
        }
        else
        {
            SSTools.ShowMessage("No Network Connection", SSTools.Position.bottom, SSTools.Time.oneSecond);
        }
       
    }

    IEnumerator DeactivatePanel()
    {
        yield return new WaitForSeconds(1);
        registerPanel.gameObject.SetActive(false);
        invalidDataText.gameObject.SetActive(false);
    }

    IEnumerator DeactivateTextl()
    {
        yield return new WaitForSeconds(1);
        invalidDataText.gameObject.SetActive(false);

    }
    IEnumerator ActiveFeilds()
    {
        yield return new WaitForSeconds(.5f);

        userName.gameObject.SetActive(false);
        userEmail.gameObject.SetActive(false);
        userNikName.gameObject.SetActive(false);
        passord.gameObject.SetActive(false);
        age.gameObject.SetActive(false);
        gender.gameObject.SetActive(false);
        experiance.gameObject.SetActive(false);
        StartCoroutine(DeactivatePanel());
    }
}


public class PlayerObject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string GamerTag { get; set; }
    public string Gender { get; set; }
    public string Age { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; }
}


public class LeaderboardObject
{
    public int Id { get; set; }
    public string PlayerName { get; set; }
    public int PlayerId { get; set; }
    public int Points { get; set; }
    public int Pickups { get; set; }
    public int TimeRun { get; set; }
}