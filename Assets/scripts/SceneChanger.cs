using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

  [SerializeField] AudioSource buttonSound;
  public void endScene()
  {
    SceneManager.LoadScene(6);

  }

  public void LoadLevel(int SceneId)
  {
    buttonSound.Play();
    StartCoroutine(changeSceneWithDelay(1, SceneId));

  }
  IEnumerator changeSceneWithDelay(int delayInSeconds, int SceneId)
  {
    yield return new WaitForSeconds(delayInSeconds);
    SceneManager.LoadScene(SceneId);
  }
  public void exit()
  {
    Debug.Log("EXIT");
    Application.Quit();
  }

  public void changeSceneToStart()
  {
    SceneManager.LoadScene(0);
  }
}
