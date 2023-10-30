using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    public class VolumeUI : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;

        public void ChangeVolume(float volume)
        {
            audioMixer.SetFloat("volume", volume);
        }
    }
}