using UnityEngine;

public class TowerSetup : MonoBehaviour
{
    [SerializeField] private Peg[] pegs;
    [SerializeField] private Peg[] _targetPegs;
    [SerializeField] private Ring[] ringPrefabs;
    [SerializeField] private Transform ringSpawnPoint;
    
    public void InitializeLevel(LevelConfig config)
    {
        ClearRings();
        SpawnRings(config.ringColors, config.ringIDs, config.startState);
        SpawnTarget(config.ringColors, config.ringIDs, config.targetSequences);
    }

    private void SpawnTarget(Color[] colors, int[] ringIDs, IntListWrapper[] targetSequences)
    {
        if (colors == null || targetSequences == null)
        {
            Debug.LogError("Invalid input data for SpawnRings");
            return;
        }

        for (int i = 0; i < targetSequences.Length; i++)
        {
            if (targetSequences[i] == null)
                continue;

            for (int j = 0; j < targetSequences[i].sequence.Count; j++)
            {
                Ring newRing = Instantiate(ringPrefabs[0], ringSpawnPoint.position, Quaternion.identity);
                newRing.transform.localScale = new Vector3(
                    ringPrefabs[0].transform.localScale.x / 2f,
                    ringPrefabs[0].transform.localScale.y,
                    ringPrefabs[0].transform.localScale.z / 2f);
                newRing.Initialize(targetSequences[i].sequence[j]);
                newRing.SetColor(colors[targetSequences[i].sequence[j]]);
                
                int pegIndex = i;
                if (pegIndex < 0 || pegIndex >= _targetPegs.Length)
                {
                    Debug.LogError($"Invalid peg index: {pegIndex}");
                    continue;
                }

                Peg targetPeg = _targetPegs[pegIndex];
                targetPeg.AddRing(newRing);
                PositionRingOnPeg(newRing, targetPeg);
                newRing.transform.parent = targetPeg.transform;
                newRing.gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }

        
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
        
        foreach (Peg peg in _targetPegs)
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
