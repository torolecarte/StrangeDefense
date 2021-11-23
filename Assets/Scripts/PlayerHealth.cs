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
    public Image LoseScreenBackgroundImage;
    public Text HealthText;
    public CanvasGroup EndGameCanvasGroup;
    public Text EndGameText;


    // Fields.
    private float _currentHealth;
    private AudioSource _playerAudioSource;
    private Color _damageFlashColour;
    private float _damageIndicatorSpeed = 5;
    private bool _isDamaged;

    // Life Cycle.
    void Start()
    {
        _currentHealth = FullHealth;
        _playerAudioSource = GetComponent<AudioSource>();
        HealthText.text = _currentHealth.ToString();
    }
    void Update()
    {
        //HandleDamaged();
    }


    // Public Methods.
    public void AddDamage(float damage)
    {
        if (damage <= 0)
            return;

        _currentHealth -= damage;
        UpdateHealthText();
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
        UpdateHealthText();
        Debug.Log($"AddHealth: Current health = {_currentHealth}");
    }
    public void MakeDead()
    {
        Destroy(gameObject);
        EndGame("You lose!");
        Debug.Log("You are dead!");
    }

    // Private Methods.
    private void UpdateHealthText()
    {
        HealthText.text = _currentHealth.ToString();
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
    private void EndGame(string message)
    {
        EndGameCanvasGroup.interactable = true;
        EndGameCanvasGroup.alpha = 1;
        EndGameText.text = message;
        LoseScreenBackgroundImage.color = Color.white;
    }

}
