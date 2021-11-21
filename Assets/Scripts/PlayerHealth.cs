using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Inspector Properties.
    public float FullHealth = 100F;
    public AudioClip PlayerDamagedAudio;
    public Image DamageIndicatorImage;


    // Fields.
    private float _currentHealth;
    private AudioSource _playerAudioSource;
    private Color _damageFlashColour;
    private float _damageIndicatorSpeed = 5;
    private bool _isDamaged;

    void Start()
    {
        _currentHealth = FullHealth;
        _playerAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //HandleDamaged();
    }

    private void HandleDamaged()
    {
        if (_isDamaged)
        {
            DamageIndicatorImage.color = _damageFlashColour;
            //DamageIndicatorImage.
        }
        else
        {
            DamageIndicatorImage.color = Color.Lerp(DamageIndicatorImage.color, Color.clear, _damageIndicatorSpeed * Time.deltaTime);
        }

        _isDamaged = false;
    }

    // Public Methods.
    public void AddDamage(float damage)
    {
        if (damage <= 0)
            return;

        _currentHealth -= damage;
        _playerAudioSource.PlayOneShot(PlayerDamagedAudio);
        _isDamaged = true;
        Debug.Log($"Current health: {_currentHealth}");

        if (_currentHealth <= 0)
            MakeDead();
    }
    public void AddHealth(float health)
    {
        Debug.Log("AddHealth: Activated");
        _currentHealth += health;
        Debug.Log($"AddHealth: Current health = {_currentHealth}");
    }

    // Private Methods.
    private void MakeDead()
    {
        Debug.Log("You are dead!");
    }
}
