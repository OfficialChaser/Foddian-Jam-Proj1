using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;


    [SerializeField] private AudioSource _musicSource, _effectsSource;


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
}

