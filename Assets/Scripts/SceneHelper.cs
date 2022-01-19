using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour
{
  public void Exit()
  {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
  }

  public void PlayGame()
  {
    SceneManager.LoadScene("Quiz");
  }

  public void Menu()
  {
    SceneManager.LoadScene("Menu");
  }
}