using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource _selectedSound;
    
    public void PlaySelectedSound()
    {
        _selectedSound.Play();
    }
}
