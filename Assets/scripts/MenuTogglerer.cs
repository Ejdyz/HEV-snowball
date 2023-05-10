using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuTogglerer : MonoBehaviour
{
  [SerializeField] GameObject PauseMenu;
  public void togglePauseMenu()
  {
    PauseMenu.SetActive(!PauseMenu.activeSelf);
  }
}
