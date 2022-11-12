using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _playingPanel;
    [SerializeField] private GameObject _helpPanel;

    [Header("Text")]
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _gameOverScoreText;
    // Start is called before the first frame update
    void Start()
    {
        MainMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void ShowPanel(GameObject panel)
    {
        _mainMenuPanel.SetActive(false);
        _gameOverPanel.SetActive(false);
        _pausePanel.SetActive(false);
        _playingPanel.SetActive(false);
        _helpPanel.SetActive(false);

        if (panel != null)
            panel.SetActive(panel);
    }
    public void SetScore(int n)
    {
        _scoreText.text = n.ToString();
    }
    public void MainMenu()
    {
        ShowPanel(_mainMenuPanel);
    }
    public void HelpMenu()
    {
        ShowPanel(_helpPanel);
    }
    public void Playing()
    {
        ShowPanel(_playingPanel);
    }
    public void PausedGame()
    {
        ShowPanel(_pausePanel);
    }
    public void ResetGame()
    {
        ShowPanel(_playingPanel);
    }
    public void GameOver(int n)
    {
        _gameOverScoreText.text = n.ToString();
        ShowPanel(_gameOverPanel);
    }
}
