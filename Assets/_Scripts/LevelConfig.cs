using UnityEngine;

[CreateAssetMenu(menuName = "Tower Of London/Level Config")]
public class LevelConfig : ScriptableObject
{
    public int levelNumber;
    public int ringCount;
    public int maxMoves;
    public int[] ringIDs;
    public Color[] ringColors;
    public int[] startState;
    public IntListWrapper[] targetSequences;
}