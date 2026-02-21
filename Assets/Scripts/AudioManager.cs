using UnityEngine;
using static Unity.VisualScripting.Member;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Clips")]
    public AudioClip launchClip;
    public AudioClip mergeClip;
    public AudioClip winClip;

    private AudioSource audioSource;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayLaunch()
    {
        audioSource.PlayOneShot(launchClip);
    }

    public void PlayMerge()
    {
        //audioSource.PlayOneShot(mergeClip);
        audioSource.pitch = Random.Range(0.1f, 1.03f); // чуть живее
        audioSource.PlayOneShot(mergeClip);
        audioSource.pitch = 1f;
    }

    public void PlayWin()
    {
        audioSource.PlayOneShot(winClip);
    }
}
