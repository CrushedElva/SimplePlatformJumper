using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _jumpStrenght = 1;

    private UnityEvent _playerLoseEvent = null;
    private UnityEvent _playerScoreEvent = null;
    private int _jumpCount = 0;
    public UnityEvent PlayerLoseEvent
    {
        get
        {
            if (_playerLoseEvent == null)
                _playerLoseEvent = new UnityEvent();
            return _playerLoseEvent;
        }
        set
        {
            _playerLoseEvent = value;
        }
    }
    public UnityEvent PlayerScoreEvent
    {
        get
        {
            if (_playerScoreEvent == null)
                _playerScoreEvent = new UnityEvent();
            return _playerScoreEvent;
        }
        set
        {
            _playerScoreEvent = value;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Loser"))
            PlayerLoseEvent?.Invoke();
        if (collision.CompareTag("CountTrigger"))
            PlayerScoreEvent?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("FirstPlatform"))
            _jumpCount = 0;
    }

    public void Jump()
    {
        if (_jumpCount >= 2)
            return;
        var rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0, _jumpStrenght));
        _jumpCount++;
    }
}
