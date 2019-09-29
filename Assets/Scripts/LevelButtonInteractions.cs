using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtonInteractions : MonoBehaviour
{
    //This script controlls the level lock unlock

    [Header("Unlock Costs")]
    public int downsouthCost;
    public int upCountryCost;
    public int colomboCost;
    public int nightCityCost;

    [Header("")]
    public Text coinCountText;

    [Header("Text Feilds")]
    public Text downsouthCostCostText;
    public Text upCointryButtonCostText;
    public Text colomboButtonCostText;
    public Text level5CostText;


    [Header("Level Button Set")]
    public Button downsouthButton;
    public Button upCointryButton;
    public Button colomboButton;
    public Button nightCityButton;



    //L2=downsouthCost
    //L3=colomboCost
    //L4=upCountryCost
    //L5=nightCityCost

    // Use this for initialization
    void Start()
    {
        int coinCount = PlayerPrefs.GetInt("CoinCount");

        int L2_count = PlayerPrefs.GetInt("Level_2_Purchased");
        int L3_count = PlayerPrefs.GetInt("Level_3_Purchased");
        int L4_count = PlayerPrefs.GetInt("Level_4_Purchased");
        int L5_count = PlayerPrefs.GetInt("Level_5_Purchased");


        downsouthCostCostText.text = String.Format("{0:#,###,###}", downsouthCost);
        upCointryButtonCostText.text = String.Format("{0:#,###,###}", colomboCost);
        colomboButtonCostText.text = String.Format("{0:#,###,###}", upCountryCost);
        level5CostText.text = String.Format("{0:#,###,###}", nightCityCost);


        if (L2_count == 1)
        {
            downsouthCostCostText.gameObject.SetActive(false);
        }
        if (L3_count == 1)
        {
            upCointryButtonCostText.gameObject.SetActive(false);
        }
        if (L4_count == 1)
        {
            colomboButtonCostText.gameObject.SetActive(false);
        }
        if (L5_count == 1)
        {
            level5CostText.gameObject.SetActive(false);
        }

    }

    private void Update()
    {
        int coinCount = PlayerPrefs.GetInt("CoinCount");
        coinCountText.text = coinCount.ToString();

        int L2_count = PlayerPrefs.GetInt("Level_2_Purchased");
        int L3_count = PlayerPrefs.GetInt("Level_3_Purchased");
        int L4_count = PlayerPrefs.GetInt("Level_4_Purchased");
        int L5_count = PlayerPrefs.GetInt("Level_5_Purchased");



        if (L2_count == 0 && downsouthCost > coinCount)
        {
            downsouthButton.interactable = false;
        }

        if (L3_count == 0 && upCountryCost > coinCount)
        {
            upCointryButton.interactable = false;
        }

        if (L4_count == 0 && colomboCost > coinCount)
        {
            colomboButton.interactable = false;
        }

        if (L5_count == 0 && nightCityCost > coinCount)
        {
            nightCityButton.interactable = false;
        }

        if (L2_count == 1)
        {
            // downsouthButton.interactable = true;
            downsouthCostCostText.fontSize = 100;
            downsouthCostCostText.text = "Purchased";
            StartCoroutine(DeactivateText());
        }
        if (L3_count == 1)
        {
            upCointryButtonCostText.fontSize = 100;
            upCointryButtonCostText.text = "Purchased";
            StartCoroutine(DeactivateText());
        }
        if (L4_count == 1)
        {
            colomboButtonCostText.fontSize = 100;
            colomboButtonCostText.text = "Purchased";
            StartCoroutine(DeactivateText());
        }
        if (L5_count == 1)
        {
            level5CostText.fontSize = 100;
            level5CostText.text = "Purchased";
            StartCoroutine(DeactivateText());
        }
    }

    IEnumerator DeactivateText()
    {
        int L2_count = PlayerPrefs.GetInt("Level_2_Purchased");
        int L3_count = PlayerPrefs.GetInt("Level_3_Purchased");
        int L4_count = PlayerPrefs.GetInt("Level_4_Purchased");
        int L5_count = PlayerPrefs.GetInt("Level_5_Purchased");

        if (L2_count == 1)
        {
            yield return new WaitForSeconds(0.5f);
            downsouthCostCostText.gameObject.SetActive(false);
        }
        if (L3_count == 1)
        {
            yield return new WaitForSeconds(0.5f);
            upCointryButtonCostText.gameObject.SetActive(false);
        }
        if (L4_count == 1)
        {
            yield return new WaitForSeconds(0.5f);
            colomboButtonCostText.gameObject.SetActive(false);
        }
        if (L5_count == 1)
        {
            yield return new WaitForSeconds(0.5f);
            level5CostText.gameObject.SetActive(false);
        }
    }
    public void levelToLoad(int level)
    {
        SceneManager.LoadScene(level);

    }

    public void PurchaseLevel2()
    {
        //downsouth


        int L2_count = PlayerPrefs.GetInt("Level_2_Purchased");
        if (L2_count == 1)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            PlayerPrefs.SetInt("Level_2_Purchased", 1);
            int coinCount = PlayerPrefs.GetInt("CoinCount");
            PlayerPrefs.SetInt("CoinCount", coinCount - downsouthCost);
            PlayerPrefs.Save();
        }

    }
    public void PurchaseLevel3()
    {
        //upcountry


        int L3_count = PlayerPrefs.GetInt("Level_3_Purchased");
        if (L3_count == 1)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            PlayerPrefs.SetInt("Level_3_Purchased", 1);
            int coinCount = PlayerPrefs.GetInt("CoinCount");
            PlayerPrefs.SetInt("CoinCount", coinCount - upCountryCost);
            PlayerPrefs.Save();
        }

    }
    public void PurchaseLevel4()
    {
        //colombo


        int L4_count = PlayerPrefs.GetInt("Level_4_Purchased");
        if (L4_count == 1)
        {
            SceneManager.LoadScene(4);
        }
        else
        {
            PlayerPrefs.SetInt("Level_4_Purchased", 1);
            int coinCount = PlayerPrefs.GetInt("CoinCount");
            PlayerPrefs.SetInt("CoinCount", coinCount - colomboCost);
            PlayerPrefs.Save();
        }

    }
    public void PurchaseLevel5()
    {
        //colombo


        int L5_count = PlayerPrefs.GetInt("Level_5_Purchased");
        if (L5_count == 1)
        {
            SceneManager.LoadScene(5);
        }
        else
        {
            PlayerPrefs.SetInt("Level_5_Purchased", 1);
            int coinCount = PlayerPrefs.GetInt("CoinCount");
            PlayerPrefs.SetInt("CoinCount", coinCount - colomboCost);
            PlayerPrefs.Save();
        }

    }
}
