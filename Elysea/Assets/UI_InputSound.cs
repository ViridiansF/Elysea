using UnityEngine;
using UnityEngine.EventSystems;

public class UIInputSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip validateSound;
    public AudioClip backSound;

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            audioSource.PlayOneShot(validateSound);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            audioSource.PlayOneShot(backSound);
        }
    }
}