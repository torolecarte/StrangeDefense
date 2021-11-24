using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Inspector Properties.
    public float FullHealth = 100F;
    public GameObject DeathFx;
    public AudioClip PlayerDamagedAudio;
    public Image DamageIndicatorImage;
    public float DamageIndicatorSpeed = 5;
    public Image HealthSlider;
    public CanvasGroup EndGameCanvasGroup;
    public Text EndGameText;


    // Fields.
    private float _currentHealth;
    private AudioSource _playerAudioSource;
    private Color _damageFlashColour = new Color(255f, 255f, 255f, 0.5f);
    private bool _isDamaged;

    // Life Cycle.
    void Start()
    {
        _currentHealth = FullHealth;
        _playerAudioSource = GetComponent<AudioSource>();
        UpdateHealthSlider();
    }
    void Update()
    {
        HandleDamagedFlash();
    }


    // Public Methods.
    public void AddDamage(float damage)
    {
        if (damage <= 0)
            return;

        _currentHealth -= damage;
        if(_currentHealth < 0)
            _currentHealth = 0;

        UpdateHealthSlider();
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
        if(_currentHealth > FullHealth)
            _currentHealth = FullHealth;

        UpdateHealthSlider();
        Debug.Log($"AddHealth: Current health = {_currentHealth}");
    }
    public void MakeDead()
    {
        _playerAudioSource.PlayOneShot(PlayerDamagedAudio);
        Instantiate(DeathFx, transform.position, Quaternion.identity);
        EndGame("You lose!");
        Debug.Log("You are dead!");
        Destroy(gameObject);
    }

    // Private Methods.
    private void UpdateHealthSlider()
    {
        HealthSlider.fillAmount = _currentHealth / FullHealth;
    }
    private void HandleDamagedFlash()
    {
        if (_isDamaged)
        {
            DamageIndicatorImage.color = _damageFlashColour;
        }
        else
        {
            DamageIndicatorImage.color = Color.Lerp(DamageIndicatorImage.color, Color.clear, DamageIndicatorSpeed * Time.deltaTime);
        }

        _isDamaged = false;
    }
    private void EndGame(string message)
    {
        EndGameCanvasGroup.interactable = true;
        EndGameCanvasGroup.alpha = 1;
        EndGameText.text = message;
        DamageIndicatorImage.color = Color.white;
    }
}
