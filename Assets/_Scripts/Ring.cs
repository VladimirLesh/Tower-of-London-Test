using System;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public int RingID { get; private set; }
    [SerializeField] private float height;
    [SerializeField] private float scaleFactor;
    private int size;
    private Color color;
    private Material material;
    private Peg currentPeg;
    private AudioSource audioSource;

    public float Height => height;
    public int Size => size;
    public Peg CurrentPeg => currentPeg;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Initialize(int ringID)
    {
        RingID = ringID;
    }
    
    public void SetColor(Color newColor)
    {
        color = newColor;
        if (material == null)
        {
            Renderer renderer = GetComponent<Renderer>();
            material = new Material(renderer.material);
            renderer.material = material;
        }
        material.color = color;
    }

    public void SetSize(int newSize)
    {
        size = newSize;
        float scale = 1f + (size - 1) * scaleFactor;
        transform.localScale = new Vector3(scale, height, scale);
    }

    public void SetCurrentPeg(Peg peg)
    {
        currentPeg = peg;
    }

    public bool IsTopRing()
    {
        if (currentPeg == null) return false;
        return currentPeg.TopRing == this;
    }
    
    public void PlayClick() => audioSource.Play();
    
    public void SetHeight(float newHeight) => height = newHeight;
}