using UnityEngine;
using UnityEngine.Audio;

public class OptionsUIController : MonoBehaviour
{
  
    [SerializeField] AudioMixer audioMixer;

    public void SliderChange(float volume){
        audioMixer.SetFloat("MasterVolume", volume);
    }

}
