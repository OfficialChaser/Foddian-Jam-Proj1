using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;


    [SerializeField] private AudioSource _musicSource, _effectsSource;
	private AudioSource[] allAudioSources;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }


    public void PlaySound(AudioClip clip) {
        _effectsSource.PlayOneShot(clip);
    }
	public void PlayMusic(AudioClip clip) {
	   _musicSource.clip = clip;
	   _musicSource.Play();
	}
	public void StopMusic(AudioClip clip) {
		_musicSource.clip = clip;
		_musicSource.Stop();
	}
	
	//Stop all sounds


	public void StopAllAudio() {
		allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
		foreach( AudioSource audioS in allAudioSources) {
			audioS.Stop();
		}
	}
}

