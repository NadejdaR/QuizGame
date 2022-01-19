using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
  [SerializeField] private PlayerStat _playerStat;
  [SerializeField] private Text wrongTxt;
  [SerializeField] private Text correctTxt;

  private void Start()
  {
    wrongTxt.text = $"Wrong Answers: {_playerStat.wrongAns}";
    correctTxt.text = $"Correct Answers: {_playerStat.correctAns}";
  }
}