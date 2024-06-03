using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [Range( 0f,1f)]public float volume = 1f;
    public AudioClip clip;
    
    void Start()
    {
        Audio.PlayClipAt(clip, transform.position,volume, maxDistance:100);
    }
}