using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance { get; private set; }
    
    [Header("Main Sound Settings")]
    [SerializeField] private float _editingSpeed;
    [SerializeField] private AudioSource _mainSound;
    [Header("FX")]
    [SerializeField] private AudioSource _move;
    [SerializeField] private AudioSource _error;
    
    private Coroutine coroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        _mainSound = GetComponent<AudioSource>();
    }

    public void SetPitch(float pitch)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        coroutine = StartCoroutine(EditPitchSmoothly(pitch));
    }
    public void PlayMove() => _move.Play();
    public void PlayError() => _error.Play();

    private IEnumerator EditPitchSmoothly(float pitch)
    {
        float progress = 0f;

        while (progress < 1)
        {
            progress += Time.deltaTime * _editingSpeed;
            _mainSound.pitch = Mathf.Lerp(_mainSound.pitch, pitch, progress);
            yield return null;
        }
        StopCoroutine(coroutine);
        coroutine = null;
    }
}
