using System.Collections.Generic;

[System.Serializable]
public class GameResult
{
    public string playerName;
    public List<LevelStat> levelStats;

    public GameResult(string playerName)
    {
        this.playerName = playerName;
        levelStats = new List<LevelStat>();
    }

    public LevelStat GetLevelStat(int level)
    {
        return levelStats.Find(stat => stat.level == level);
    }

    public void UpdateLevelStat(int level, int moves, float time)
    {
        LevelStat stat = GetLevelStat(level);
        
        if (stat == null)
        {
            stat = new LevelStat
            {
                level = level,
                bestMoves = moves,
                bestTime = time
            };
            
            levelStats.Add(stat);
        }
        else
        {
            if (moves < stat.bestMoves)
                stat.bestMoves = moves;
            
            if (time < stat.bestTime)
                stat.bestTime = time;
        }
    }
}

[System.Serializable]
public class LevelStat
{
    public int level;
    public int bestMoves;
    public float bestTime;
}