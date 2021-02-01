using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Button _pauseButton;
    [SerializeField]
    private Sprite _pauseSprite;
    [SerializeField]
    private Sprite _playSprite;
    [SerializeField]
    private TextMeshProUGUI _tmpScore;
    [SerializeField]
    private PlayerController _playerController;
    [SerializeField]
    private TextMeshProUGUI _tmpHighScore;
    [SerializeField]
    private TextMeshProUGUI _tmpFinalScore;
    [SerializeField]
    private Button _btnPlayAgain;
    [SerializeField]
    private GameObject _deathUI;
    [SerializeField]
    private GameObject _playUI;
    [SerializeField]
    private Transform _playerStartPoint;
    [SerializeField]
    private Spawn _spawn;
    [SerializeField]
    private GameObject _firstPlatform;

    private int _score;
    private int _highScore;
    private Vector3 _firstPlatformStartPosition;

    private bool _isPaused = true;

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnPauseButtonClick);
        _playerController.PlayerLoseEvent.AddListener(OnPlayerLose);
        _playerController.PlayerScoreEvent.AddListener(OnPlayerScore);
        _btnPlayAgain.onClick.AddListener(OnPlayAgainClick);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OnPauseButtonClick);
        _playerController.PlayerLoseEvent.RemoveListener(OnPlayerLose);
        _playerController.PlayerScoreEvent.RemoveListener(OnPlayerScore);
        _btnPlayAgain.onClick.RemoveListener(OnPlayAgainClick);
    }

    private void Start()
    {
        Time.timeScale = 0;
        _pauseButton.image.sprite = _playSprite;
        _score = 0;
        _highScore = PlayerPrefs.GetInt("highScore", 0);
        _tmpScore.text = "Score: 0";
        _deathUI.SetActive(false);
        _playUI.SetActive(true);
        _playUI.SetActive(true);
        _firstPlatformStartPosition = _firstPlatform.transform.position;
    }

    private void Update()
    {
        if (!_isPaused && Input.GetMouseButtonDown(0))
            _playerController.Jump();
    }

    private void OnPlayAgainClick()
    {
        _deathUI.SetActive(false);
        _playUI.SetActive(true);
        Time.timeScale = 1;
        _score = 0;
        _isPaused = false;
        _pauseButton.image.sprite = _pauseSprite;
        _tmpScore.text = "Score: 0";
        _playerController.transform.position = _playerStartPoint.position;
        foreach (Transform child in _spawn.transform)
            Destroy(child.gameObject);
        _firstPlatform.SetActive(true);
        _firstPlatform.transform.position = _firstPlatformStartPosition;
    }

    private void OnPauseButtonClick()
    {
        _isPaused = !_isPaused;
        if (_isPaused)
        {
            _pauseButton.image.sprite = _playSprite;
            Time.timeScale = 0;
        } 
        else
        {
            _pauseButton.image.sprite = _pauseSprite;
            Time.timeScale = 1;
        }
    }

    private void OnPlayerScore()
    {
        _score++;
        _tmpScore.text = "Score: " + _score;
    }

    private void OnPlayerLose()
    {
        Time.timeScale = 0;
        if (_score > _highScore)
        {
            _highScore = _score;
            PlayerPrefs.SetInt("highScore", _score);
            PlayerPrefs.Save();
        }
        _playUI.SetActive(false);
        _deathUI.SetActive(true);
        _tmpHighScore.text = "High score: " + _highScore;
        _tmpFinalScore.text = "Score: " + _score;
        _isPaused = true;
    }
}
