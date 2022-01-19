using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlayerStat), menuName = "SO/PlayerStat")]
public class PlayerStat : ScriptableObject
{
  public int wrongAns;
  public int correctAns;
}