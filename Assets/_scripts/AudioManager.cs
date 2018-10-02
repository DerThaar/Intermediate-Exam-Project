using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
	public AudioClip[] audioClips;
	[HideInInspector] public AudioSource musicSource;
	[HideInInspector] public int musicState;
	private static AudioManager instance = null;
	public static AudioManager Instance { get { return instance; } }
	[SerializeField] float fadeOutTime = 1.5f;


	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
			return;
		}
		else
		{
			instance = this;
		}

		musicSource = GetComponentInChildren<AudioSource>();
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		musicState = 1;
		musicSource.clip = audioClips[musicState - 1];
		musicSource.Play();
	}

	public void TriggerMusicChange()
	{		
		StartCoroutine(MusicChangeCO());
	}

	public IEnumerator MusicChangeCO()
	{
		float timer = 0f;

		while (timer < fadeOutTime)
		{
			float t = timer / fadeOutTime;
			float vol = Mathf.Lerp(1f, 0f, t);
			musicSource.volume = vol;
			timer += Time.unscaledDeltaTime;		
			yield return null;
		}

		timer = 0f;
		musicSource.Stop();
		musicSource.clip = audioClips[musicState - 1];
		musicSource.Play();

		while (timer < fadeOutTime)
		{
			float t = timer / fadeOutTime;
			float vol = Mathf.Lerp(0f, 1f, t);
			musicSource.volume = vol;
			timer += Time.unscaledDeltaTime;			
			yield return null;
		}
	}
}

