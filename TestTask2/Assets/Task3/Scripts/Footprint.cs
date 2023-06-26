using UnityEngine;

public class Footprint : MonoBehaviour
{
    private FootprintsPool _footprintsPool;
    private float lifeTimeMax = 10;

    private float _currentLifeTime = 0;

    private bool _spawned = false;
    private void Awake ()
    {
        _footprintsPool = FindObjectOfType<FootprintsPool>();
    }

    private void OnEnable ()
    {
        _currentLifeTime = 0;
        _spawned = true;
    }

    private void Update ()
    {
        if (_spawned) {
            _currentLifeTime += Time.deltaTime;
            if (_currentLifeTime >= lifeTimeMax) {
                _footprintsPool.ReleaseObject(gameObject);
            }
        }
    }
}
