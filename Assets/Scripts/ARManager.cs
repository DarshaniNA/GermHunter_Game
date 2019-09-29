using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARManager : MonoBehaviour
{
    public GameObject model1;
    public GameObject model2;
    public GameObject model3;
    public GameObject model4;
    public GameObject model5;
    public GameObject model6;
    // Start is called before the first frame update
    void Start()
    {
        model1.SetActive(true);
        model2.SetActive(false);
        model3.SetActive(false);
        model4.SetActive(false);
        model5.SetActive(false);
        model6.SetActive(false);
    }

    public void ActiveAR1()
    {
        model1.SetActive(true);
        model2.SetActive(false);
        model3.SetActive(false);
        model4.SetActive(false);
        model5.SetActive(false);
        model6.SetActive(false);
    }
    public void ActiveAR2()
    {
        model2.SetActive(true);
        model1.SetActive(false);
        model3.SetActive(false);
        model4.SetActive(false);
        model5.SetActive(false);
        model6.SetActive(false);
    }
    public void ActiveAR3()
    {
        model3.SetActive(true);
        model1.SetActive(false);
        model2.SetActive(false);
        model4.SetActive(false);
        model5.SetActive(false);
        model6.SetActive(false);
    }
    public void ActiveAR4()
    {
        model4.SetActive(true);
        model1.SetActive(false);
        model3.SetActive(false);
        model2.SetActive(false);
        model5.SetActive(false);
        model6.SetActive(false);
    }

    public void ActiveAR5()
    {
        model5.SetActive(true);
        model1.SetActive(false);
        model3.SetActive(false);
        model2.SetActive(false);
        model4.SetActive(false);
        model6.SetActive(false);
    }

    public void ActiveAR6()
    {
        model6.SetActive(true);
        model1.SetActive(false);
        model3.SetActive(false);
        model2.SetActive(false);
        model5.SetActive(false);
        model4.SetActive(false);
    }

    public void ExitAR ()
    {
        SceneManager.LoadScene("Start");
    }
}
