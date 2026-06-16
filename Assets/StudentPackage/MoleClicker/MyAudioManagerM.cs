using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MyAudioManagerM : MonoBehaviour, IAudioServiceM
{
    [SerializeField] private AudioSource m_AudioSource;
    public void PlaySoundEffect(AudioClip clip)
    {
        m_AudioSource.clip = clip;
        m_AudioSource.Stop();
        m_AudioSource.Play();
    }
}
