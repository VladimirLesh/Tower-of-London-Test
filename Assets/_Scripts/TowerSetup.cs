using UnityEngine;

public class TowerSetup : MonoBehaviour
{
    [SerializeField] private Peg[] pegs;
    [SerializeField] private Ring[] ringPrefabs;
    [SerializeField] private Transform ringSpawnPoint;
    
    public void InitializeLevel(LevelConfig config)
    {
        ClearRings();
        SpawnRings(config.ringColors, config.ringIDs, config.startState);
    }

    private void SpawnRings(Color[] colors, int[] ringIDs, int[] startPositions)
    {
        if (colors == null || startPositions == null || colors.Length != startPositions.Length)
        {
            Debug.LogError("Invalid input data for SpawnRings");
            return;
        }

        for (int i = 0; i < colors.Length; i++)
        {
            Ring newRing = Instantiate(ringPrefabs[0], ringSpawnPoint.position, Quaternion.identity);
            newRing.Initialize(ringIDs[i]);
            newRing.SetColor(colors[i]);
            // newRing.SetSize(i + 1);

            int pegIndex = startPositions[i];
            if (pegIndex < 0 || pegIndex >= pegs.Length)
            {
                Debug.LogError($"Invalid peg index: {pegIndex}");
                continue;
            }

            Peg targetPeg = pegs[pegIndex];
            targetPeg.AddRing(newRing);
            PositionRingOnPeg(newRing, targetPeg);
        }
    }
    
    public int GetPegState(int pegIndex)
    {
        if (pegIndex < 0 || pegIndex >= pegs.Length)
        {
            Debug.LogError($"Invalid peg index: {pegIndex}");
            return -1;
        }

        return pegs[pegIndex].RingCount;
    }
    
    private void ClearRings()
    {
        foreach (Peg peg in pegs)
        {
            peg.ClearRings();
        }
    }

    private void PositionRingOnPeg(Ring ring, Peg peg)
    {
        float yOffset = peg.RingCount * ring.Height;
        Vector3 position = peg.GetSpawnPointPosition() + Vector3.up * yOffset;
        ring.transform.position = position;
    }
    
    public Peg GetPeg(int index)
    {
        if (index < 0 || index >= pegs.Length)
        {
            Debug.LogError($"Invalid peg index: {index}");
            return null;
        }

        return pegs[index];
    }
}
