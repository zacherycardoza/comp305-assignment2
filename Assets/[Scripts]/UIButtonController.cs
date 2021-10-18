using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class UIButtonController : MonoBehaviour
{
    public void OnStartButton_Clicked()
    {
        SceneManager.LoadScene("Main");
    }
}
