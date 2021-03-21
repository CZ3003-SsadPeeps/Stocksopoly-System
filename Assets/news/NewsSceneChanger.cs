using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewsSceneChanger : MonoBehaviour
{

    public void LoadScene(string sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
       
    }
}
