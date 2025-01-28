using System.Collections;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private float _editingSpeed;
    private AudioSource audioSource;
    
    private Coroutine coroutine;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

    private IEnumerator EditPitchSmoothly(float pitch)
    {
        float progress = 0f;

        while (progress < 1)
        {
            progress += Time.deltaTime * _editingSpeed;
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, pitch, progress);
            yield return null;
        }
        StopCoroutine(coroutine);
        coroutine = null;
    }
}
