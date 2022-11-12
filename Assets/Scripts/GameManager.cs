using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public UIManager _uiManager;
    public Timer _timer;
    public HealthBar _healthBar;
    public PlayerMovement _playerMovement;

    private static AudioSource _audioSource;
    public AudioClip _menuMusic;
    public AudioClip[] _actionMusic;
    public AudioClip _gameOverMusic;
    public static AudioClip _zombiePunch;

    public static GameState _gameState;
    public static int _score;    
    public static int _maxHealth = 200;
    public static int _currentHealth;
    public enum GameState
    {
        start,
        running,
        paused,
        gameover
    }
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        MainMenu();
    }
    
    public void MainMenu()
    {
        _audioSource.clip = _menuMusic;
        _audioSource.Play();
        _timer._isRunning = false;
        _currentHealth = _maxHealth;
        _timer._timeRemaining = _timer._time;
        _playerMovement.ResetPosition();
        _gameState = GameState.start;        
        _uiManager.MainMenu();        
    }
    public void HelpMenu()
    {
        _uiManager.HelpMenu();
    }
    public void PlayGame()
    {
        _audioSource.clip = GetAudioClip(_actionMusic);
        _audioSource.Play();
        Cursor.lockState = CursorLockMode.Locked;
        _score = 0;
        _uiManager.SetScore(_score);        
        _healthBar.SetMaxHealth(_currentHealth);        
        _timer._isRunning = true;        
        _gameState = GameState.running;
        _uiManager.Playing();
    }
    public void PauseGame()
    {
        _audioSource.Pause();
        Cursor.lockState = CursorLockMode.Confined;
        _timer._isRunning = false;
        _gameState = GameState.paused;
        _uiManager.PausedGame();
    }
    public void ContinueGame()
    {
        _audioSource.Play();
        Cursor.lockState = CursorLockMode.Locked;
        _timer._isRunning = true;        
        _gameState = GameState.running;
        _uiManager.Playing();
    }
    
    public void GameOver()
    {
        _audioSource.clip = _gameOverMusic;
        _audioSource.Play();
        Cursor.lockState = CursorLockMode.Confined;
        _timer._isRunning = false;
        _gameState = GameState.gameover;
        _uiManager.GameOver(_score);
    }
    public void Quit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_gameState);
        _healthBar.SetHealth(_currentHealth);
        _uiManager.SetScore(_score);

        if (_currentHealth <= 0 || _timer._timeRemaining<=0)
        {
            GameOver();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _gameState == GameState.paused)
        {
            ContinueGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _gameState==GameState.running)
        {
            PauseGame();
        }
    }
    public static void DetectHit()
    {
        _currentHealth -= 10;
        _audioSource.PlayOneShot(_zombiePunch);
    }
    private AudioClip GetAudioClip(AudioClip[] clips)
    {
        int n = Random.Range(1, clips.Length);
        AudioClip clip = clips[n];
        clips[n] = clips[0];
        clips[0] = clip;
        return clip;
    }

}
