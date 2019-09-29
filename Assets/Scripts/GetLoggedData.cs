using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetLoggedData : MonoBehaviour
{
   public Text userName;
   public Login login;
    // Start is called before the first frame update
    void Start()
    {
        userName.text = login.loggedUsername;
    }
}
