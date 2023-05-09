using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

  void endScene()
  {
    SceneManager.LoadScene(6);

  }

  void LoadLevel(int SceneId)
  {
    
    SceneManager.LoadScene(SceneId);
  }

  public void exit()
  {
    Application.Quit();
  }

  void changeSceneToStart()
  {
    SceneManager.LoadScene(0);
  }
}
