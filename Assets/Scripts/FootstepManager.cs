using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    [Header("Footstep Clips")]
    public AudioClip[] footstepClips;

    [Header("Settings")]
    [Range(0, 1f)] public float volume = 0.7f;
    [Range(0, 0.2f)] public float pitchVariation = 0.1f;
    public float stepInterval = 0.35f;

    private AudioSource audioSource;
    private float stepTimer;
    private int lastIndex = -1;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        stepTimer -= Time.deltaTime;
    }

    public void TriggerFootstep(bool isMoving)
    {
        if (!isMoving || footstepClips.Length == 0) return;

        if (stepTimer > 0) return;

        stepTimer = stepInterval;
        PlayRandomClip();
    }

    void PlayRandomClip()
    {
        if (footstepClips.Length == 1)
        {
            audioSource.PlayOneShot(footstepClips[0], volume);
            return;
        }

        int index;
        do { index = Random.Range(0, footstepClips.Length); }
        while (index == lastIndex);

        lastIndex = index;

        audioSource.pitch = 1f + Random.Range(-pitchVariation, pitchVariation);
        audioSource.PlayOneShot(footstepClips[index], volume);
    }


}
