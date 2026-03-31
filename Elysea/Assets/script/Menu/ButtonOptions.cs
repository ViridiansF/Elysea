using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class OptionsButton : MonoBehaviour
{

    public Slider volumeSlider;


    public void Start()
    {
        // Charger les valeurs
        float volume = PlayerPrefs.GetFloat("Volume", 1f);
        // Appliquer
        AudioListener.volume = volume;
        // Mettre à jour les sliders
        volumeSlider.value = volume;
        // Ajouter les listeners
        volumeSlider.onValueChanged.AddListener(SetVolume);
        
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }
    
    public void Menu()
    {
        SceneManager.LoadScene("Menu Principal");
    }


}
