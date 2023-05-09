using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

  public void endScene()
  {
    SceneManager.LoadScene(6);

  }

  public void LoadLevel(int SceneId)
  {
    
    SceneManager.LoadScene(SceneId);
  }

  public void exit()
  {
    Application.Quit();
  }

  public void changeSceneToStart()
  {
    SceneManager.LoadScene(0);
  }
}
