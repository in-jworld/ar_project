using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
   public void ChangeScene()
    {
        switch(this.gameObject.name)
        {
            case "Start":
                SceneManager.LoadScene("Stage1");
                break;
            case "Settings":
                SceneManager.LoadScene("Intro");
                break;
        }
    }
}
