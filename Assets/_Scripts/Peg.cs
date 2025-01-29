using System.Collections.Generic;
using UnityEngine;

public class Peg : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Stack<Ring> rings = new Stack<Ring>();

    public int RingCount => rings.Count;
    public Ring TopRing => rings.Count > 0 ? rings.Peek() : null;

    public void AddRing(Ring ring)
    {
        if (ring == null) return;

        rings.Push(ring);
        ring.SetCurrentPeg(this);

        PositionRingOnPeg(ring);
    }
    
    private void PositionRingOnPeg(Ring ring)
    {
        float yOffset = rings.Count * ring.Height;
        Vector3 position = target.position + Vector3.up * yOffset;

        ring.transform.position = position;
    }

    public Ring RemoveTopRing()
    {
        if (rings.Count == 0)
        {
            Debug.LogWarning("No rings to remove from peg");
            return null;
        }

        Ring removedRing = rings.Pop();
        removedRing.SetCurrentPeg(null);
        return removedRing;
    }

    public void ClearRings()
    {
        while (rings.Count > 0)
        {
            Ring ring = rings.Pop();
            Destroy(ring.gameObject);
        }
    }
    
    public List<int> GetRingSequence()
    {
        List<int> sequence = new List<int>();
        foreach (var ring in rings)
        {
            sequence.Add(ring.RingID);
        }
        sequence.Reverse();
        return sequence;
    }

    public Vector3 GetSpawnPointPosition() => target.position;
}