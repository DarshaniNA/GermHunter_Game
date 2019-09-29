using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using System.Text;

public class XMLReader : MonoBehaviour
{
    private Toggle toggle;
    public TextAsset dictionary;
    public string languageName;
    public int currentLanguage;

    string button1;
    string button2;
    string button3;
    string button4;
    string button5;
    string button6;
    string button7;
    string button8;
    string button9;
    string button10;
    string button11;
    string button12;
    string button13;
    string button14;
    string button15;
    public Text qwe1;
    public Text qwe2;
    public Text qwe3;
    public Text qwe4;
    public Text qwe5;
    public Text qwe6;
    public Text qwe7;
    public Text qwe8;
    public Text qwe9;
    public Text qwe10;
    public Text qwe11;
    public Text qwe12;
    public Text qwe13;
    public Text qwe14;
    public Text qwe15;
    public Text qwe16;
    public Text qwe17;
    public Text qwe18;

    List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();
    Dictionary<string, string> obj;

    private void Awake()
    {
        Reader();
    }

    private void Start()
    {
        toggle = GetComponent<UnityEngine.UI.Toggle>();
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            SoundManager.Instance.PlaySound(Constant.SOUND_CLICK);
            currentLanguage = 1;
        }
        else { currentLanguage = 0; }
        
    }

    private void Update()
    {
        languages[currentLanguage].TryGetValue("Name", out languageName);
        languages[currentLanguage].TryGetValue("button1", out button1);
        languages[currentLanguage].TryGetValue("button2", out button2);
        languages[currentLanguage].TryGetValue("button3", out button3);
        languages[currentLanguage].TryGetValue("button4", out button4);
        languages[currentLanguage].TryGetValue("button5", out button5);
        languages[currentLanguage].TryGetValue("button6", out button6);
        languages[currentLanguage].TryGetValue("button7", out button7);
        languages[currentLanguage].TryGetValue("button8", out button8);
        languages[currentLanguage].TryGetValue("button9", out button9);
        languages[currentLanguage].TryGetValue("button10", out button10);
        languages[currentLanguage].TryGetValue("button11", out button11);
        languages[currentLanguage].TryGetValue("button12", out button12);
        languages[currentLanguage].TryGetValue("button13", out button13);
        languages[currentLanguage].TryGetValue("button14", out button14);
        languages[currentLanguage].TryGetValue("button15", out button15);

        qwe1.text = button1;
        qwe2.text = button2;
        qwe3.text = button3;
        qwe4.text = button4;
        qwe5.text = button5;
        qwe6.text = button6;
        qwe7.text = button7;
        qwe8.text = button8;
        qwe9.text = button9;
        qwe10.text = button10;
        qwe11.text = button11;
        qwe12.text = button12;
        qwe13.text = button13;
        qwe14.text = button14;
        qwe15.text = button7;
        qwe16.text = button8;
        qwe17.text = button6;
        qwe18.text = button15;
    }

    void Reader ()
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(dictionary.text);
        XmlNodeList landuageList = xmlDocument.GetElementsByTagName("language");

        foreach(XmlNode languageValue in landuageList)
        {
            XmlNodeList languageContent = languageValue.ChildNodes;
            obj = new Dictionary<string, string>();

            foreach(XmlNode value in languageContent)
            {
                if (value.Name == "Name")
                    obj.Add(value.Name, value.InnerText);
                if (value.Name == "button1")
                    obj.Add(value.Name, value.InnerText);
                if (value.Name == "button2")
                    obj.Add(value.Name, value.InnerText);
                if (value.Name == "button3")
                    obj.Add(value.Name, value.InnerText);
                if (value.Name == "button4")
                    obj.Add(value.Name, value.InnerText);
                if (value.Name == "button5")
                    obj.Add(value.Name, value.InnerText);
                if (value.Name == "button6")
                    obj.Add(value.Name, value.InnerText);
                if (value.Name == "button7")
                    obj.Add(value.Name, value.InnerText);
                if (value.Name == "button8")
                    obj.Add(value.Name, value.InnerText);
                if (value.Name == "button9")
                    obj.Add(value.Name, value.InnerText);
                if (value.Name == "button10")
                    obj.Add(value.Name, value.InnerText);
                if (value.Name == "button11")
                    obj.Add(value.Name, value.InnerText);
                if (value.Name == "button12")
                    obj.Add(value.Name, value.InnerText);
                if (value.Name == "button13")
                    obj.Add(value.Name, value.InnerText);
                if (value.Name == "button14")
                    obj.Add(value.Name, value.InnerText);
                if (value.Name == "button15")
                    obj.Add(value.Name, value.InnerText);
            }
            languages.Add(obj);
        }
    }
}
