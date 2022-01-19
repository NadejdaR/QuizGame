using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(QuestionSO), menuName = "SO/Qustion")]
public class QuestionSO : ScriptableObject
{
  [TextArea(6, 10)] public string QuestionTxt;
  public Sprite QuestionImg;
  public List<string> Answers;
  public string CorrectAns;
}