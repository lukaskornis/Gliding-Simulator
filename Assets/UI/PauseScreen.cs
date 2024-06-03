using System;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(10000)]
public class PauseScreen : MonoBehaviour
{
    [SerializeField]private Slider musicSlider;
    [SerializeField]private Slider effectsSlider;
    [SerializeField]private GameObject pauseScreen;
    public static bool paused = false;

    private void Start()
    {
        Audio.Instance.mixer.GetFloat("Music Volume",out float musicDb);
        musicSlider.SetValueWithoutNotify(Audio.MusicVolume);
        
        Audio.Instance.mixer.GetFloat("Effects Volume",out float effectsDb);
        effectsSlider.SetValueWithoutNotify(Audio.EffectsVolume);
        
        musicSlider.onValueChanged.AddListener( value => Audio.MusicVolume = value );
        effectsSlider.onValueChanged.AddListener( value => Audio.EffectsVolume = value );
        pauseScreen.SetActive(false);
        Inputs.OnPause.AddListener(ClickPause);
    }

    private void ClickPause()
    {
        paused = !paused;
        if (paused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    private void OnDestroy()
    {
        paused = false;
    }
    

    private bool cursorWasLocked;

    public void Pause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
        
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cursorWasLocked = true;
        }
    }
    
    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;

        if (cursorWasLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
