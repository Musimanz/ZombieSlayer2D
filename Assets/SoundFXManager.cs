using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundFXObj;

    public static SoundFXManager instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObj, spawnTransform.position, Quaternion.identity, spawnTransform);

        audioSource.gameObject.AddComponent<AudioPosition2D>();

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }

    public AudioSource PlayRandSoundFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {
        int rand = Random.Range(0, audioClip.Length);

        AudioSource audioSource = Instantiate(soundFXObj, spawnTransform.position, Quaternion.identity, spawnTransform);

        audioSource.gameObject.AddComponent<AudioPosition2D>();

        audioSource.clip = audioClip[rand];

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);

        return audioSource;
    }

    public void PlaySeqSoundFXClips(AudioClip[] audioClips, Transform spawnTransform, float volume)
    {
        // 1. Spawn the audio source object
        AudioSource audioSource = Instantiate(soundFXObj, spawnTransform.position, Quaternion.identity, spawnTransform);

        audioSource.gameObject.AddComponent<AudioPosition2D>();

        // 2. Start the Coroutine to handle the sequential playing
        StartCoroutine(PlayAudioSequenceRoutine(audioSource, audioClips, volume));
    }

    private System.Collections.IEnumerator PlayAudioSequenceRoutine(AudioSource source, AudioClip[] clips, float volume)
    {
        source.volume = volume;

        // Loop through each clip sequentially
        for (int i = 0; i < clips.Length; i++)
        {
            // Set the current clip and play it
            source.clip = clips[i];
            source.Play();

            // WAIT right here in the code until the clip is completely finished playing
            yield return new WaitForSeconds(source.clip.length);
        }

        // Once the loop completely finishes playing all clips, destroy the object
        Destroy(source.gameObject);
    }

}

public class AudioPosition2D : MonoBehaviour
{
    private Transform cameraTransform;

    void Start()
    {
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return;
        // Keep the local X and Y tracking from the parent zombie, but forcefully snap the global Z position to the camera's Z line
        transform.position = new Vector3(transform.position.x, transform.position.y, cameraTransform.position.z);
    }
}
