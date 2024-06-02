using System;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : UnitySingleton<Audio>
{
    [HideInInspector]public AudioMixerGroup audioMixerGroup;
    [HideInInspector]public AudioSource globalSource;
    public AudioMixer mixer;

    public static float MusicVolume
    {
         get { return Instance.mixer.GetFloat("Music Volume", out float value) ? FromDb( value) : 0; }
         set { Instance.mixer.SetFloat("Music Volume", ToDb( value)); }
    } 
    
    public static float EffectsVolume
    {
         get { return Instance.mixer.GetFloat("Effects Volume", out float value) ?  FromDb( value) : 0; }
         set { Instance.mixer.SetFloat("Effects Volume",  ToDb( value)); }
    }

    public static float LowPassFreq
    {
        get { return Instance.mixer.GetFloat("Lowpass Freq", out float value) ? value : 0; }
        set { Instance.mixer.SetFloat("Lowpass Freq", value); }
    }
    
    private void Awake()
    {
        globalSource = gameObject.AddComponent<AudioSource>();
        //mixer = Resources.Load<AudioMixer>("Master");
        audioMixerGroup = mixer.FindMatchingGroups("Effects")[0];
        globalSource.outputAudioMixerGroup = audioMixerGroup;
        //print ( "!" );
        LoadPrefs();
    }

    private void OnDestroy()
    {
        SavePrefs();
    }


    public static void PlayClip(AudioClip clip, float volume = 1f)
    {
          Instance.globalSource.PlayOneShot(clip, volume);
    }
    
    public static void PlayClipAt(AudioClip clip, Vector3 position,float volume = 1f, float pitch = 1f,float maxDistance = 5)
    {
          var source = new GameObject($"{clip.name} 3D Sound").AddComponent<AudioSource>();
          source.outputAudioMixerGroup = Instance.audioMixerGroup;
          source.transform.position = position;
          source.spatialBlend = 1f;
          source.dopplerLevel = 5f;
          source.rolloffMode = AudioRolloffMode.Linear;
          source.maxDistance = maxDistance;
          source.clip = clip;
          source.volume = volume;
          source.pitch = pitch;
          source.Play();
          Destroy(source.gameObject, clip.length);
    }
    
    
    public static float ToDb(float value)
    {
        if(value == 0) return -80;
        return 20 * Mathf.Log10( value );
    }


    public static float FromDb(float value)
    {
        return Mathf.Pow(10, value / 20);
    }

    void LoadPrefs()
    {
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
        EffectsVolume = PlayerPrefs.GetFloat("EffectsVolume", 1);
    }

    void SavePrefs()
    {
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        PlayerPrefs.SetFloat("EffectsVolume", EffectsVolume);
    }
}