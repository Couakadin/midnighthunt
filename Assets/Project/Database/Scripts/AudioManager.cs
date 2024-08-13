using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [Header("--------- Audio Source ---------")]

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--------- Audio Clip ---------")]

    public AudioClip background;
    public AudioClip wolfTrack;
    public AudioClip wolfKill;
    public AudioClip villagerCry;

    private void Start()
    {        
            musicSource.clip = background;
            musicSource.Play();
              
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    /* 
     * AudioManager audioManager; tout en haut du script
     * 
     * private void Awake() 
     * {
     * audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
     * }
     * 
     * audioManager.PlaySFX(audioManager.);
     * 
     */
}
