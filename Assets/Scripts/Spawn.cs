using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private GameObject _platform;
    [SerializeField] private float _spawnDelay = 0.7f;
    [SerializeField] private float _minScale = 0.6f;
    [SerializeField] private float _maxScale = 1.2f;
    private float _timeFromLastSpawn = 0f;

    private void FixedUpdate()
    {
        _timeFromLastSpawn += Time.fixedDeltaTime;
        if (_timeFromLastSpawn >= _spawnDelay)
        {
            var platform = Instantiate(_platform, transform.position, Quaternion.identity, transform);
            float r = Random.Range(_minScale, _maxScale);
            platform.transform.localScale = new Vector3(platform.transform.localScale.x * r, platform.transform.localScale.y, platform.transform.localScale.z);
            _timeFromLastSpawn = 0f;
        }
    }
}
