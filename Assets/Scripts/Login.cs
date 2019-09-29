using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.IO;

public class Login : MonoBehaviour
{
    public InputField userName;
    public InputField userPassword;
    public Text alertText;
    public GameObject landingPanel;
    public GameObject loggingPanel;
    public string loggedUsername;
    public Text invalidDataText;
    public Text titleText;
    public Image loginPannel;
    // Start is called before the first frame update
    void Start()
    {
        landingPanel.SetActive(false);
        invalidDataText.gameObject.SetActive(false);
        userName.text = "";
        userPassword.text = "";
    }

    public void DoLogin()
    {
    
            if (userName.text == "" || userPassword.text == "")
            {
                invalidDataText.gameObject.SetActive(true);
                StartCoroutine(DeactivateTextl());
            }
            else
            {
                SubmitLogin();
            }
    }

    public void DoLogout()
    {
        PlayerPrefsUtil.SavePlayer(null);
        landingPanel.SetActive(false);
        invalidDataText.gameObject.SetActive(false);
    }

    public void SubmitLogin()
    {
        StartCoroutine(Submit());
    }
    IEnumerator DeactivateTextl()
    {
        yield return new WaitForSeconds(1);
        invalidDataText.gameObject.SetActive(false);

    }
    IEnumerator Submit()
    {
        WWW www;
        Hashtable postHeader = new Hashtable();
        postHeader.Add("Content-Type", "application/json");

        LoginObject postData = new LoginObject();
        postData.Username = userName.text;
        postData.Password = userPassword.text;

        var jsonData = JsonMapper.ToJson(postData);

        //var formData = System.Text.Encoding.UTF8.GetBytes("{'Username':'" + userName.text
        //    + "', 'Password':'" + userPassword.text + "'}");
        if (Constant.CheckNetworkAvailability())
        {
            www = new WWW(Constant.LOGIN_URL, System.Text.Encoding.UTF8.GetBytes(jsonData), postHeader);

            yield return www;
            if (www.text.Equals("null"))
            {
                alertText.text = "Check Your User Name Password";
            }
            //else if (www.text != null)
            //{
            //    string[] msg = www.text.Split(char.Parse(":"));
            //    if (msg[1].Equals("\"An error has occurred.\"}"))
            //    {
            //        print("An error has occurred!!!!!!!!!!!!!");
            //    }
            //}
            else
            {
                Debug.Log("request success");
                alertText.text = "Login Success";
                landingPanel.SetActive(true);

                //loginPannel.enabled = false;
                print(www.text);

                var userData = JsonMapper.ToObject<PlayerObject>(www.text);
                PlayerPrefsUtil.SavePlayer(userData);
                titleText.text = "Hi " + userData.Name.ToString();
                loggedUsername = userData.Name;

                StartCoroutine(Deactivatetext());
            }
        }
        else
        {
            SSTools.ShowMessage("No Network Connection", SSTools.Position.bottom, SSTools.Time.oneSecond);
        }
    }

    //public void SaveUser(PlayerObject player)
    //{
    //    XmlSerializer serializer = new XmlSerializer(typeof(PlayerObject));
    //    using (StringWriter sw = new StringWriter())
    //    {
    //        serializer.Serialize(sw, player);
    //        PlayerPrefs.SetString("player values", sw.ToString());
    //    }
    //}

    IEnumerator Deactivatetext()
    {
        yield return new WaitForSeconds(.5f);
        alertText.gameObject.SetActive(false);
        loggingPanel.SetActive(false);
    }
}

[Serializable]
public class LoginObject
{
    public string Username { get; set; }
    public string Password { get; set; }
}