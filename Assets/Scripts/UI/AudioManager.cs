using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource m_AudioSource;
    private void Awake()
    {
        m_AudioSource = transform.GetComponent<AudioSource>();
        EventCenter.AddListener<AudioClip>(EventDefine.PlayAudio, PlayAudio);
        EventCenter.AddListener<bool>(EventDefine.SwitchMusic, SwitchAudio);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<AudioClip>(EventDefine.PlayAudio, PlayAudio);
        EventCenter.RemoveListener<bool>(EventDefine.SwitchMusic, SwitchAudio);
    }

    private void PlayAudio(AudioClip audioClip)
    {
        m_AudioSource.PlayOneShot(audioClip);
    }
    private void SwitchAudio(bool value)
    {
        m_AudioSource.mute = !value;
    }
}
