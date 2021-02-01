using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x - 0.1f * _speed, transform.position.y, transform.position.z);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlatformDestroyer"))
        {
            if (gameObject.CompareTag("FirstPlatform"))
                gameObject.SetActive(false);
            else
                Destroy(gameObject);
        }
    }
}
