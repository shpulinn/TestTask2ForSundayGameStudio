using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    [SerializeField] private float arrowSpeed = 30f;
    [SerializeField] private float arrowDamage = 20f;

    private Rigidbody _arrowRigidbody;
    private ArrowPool _pool;

    public float ArrowDamage
    {
        get => arrowDamage;
    }

    private void Awake ()
    {
        _arrowRigidbody = GetComponent<Rigidbody>();
        _pool = FindObjectOfType<ArrowPool>();
    }

    public void Init ()
    {
        _arrowRigidbody.velocity = transform.forward * arrowSpeed;
    }

    private void OnTriggerEnter (Collider other)
    {
        _pool.ReleaseObject(gameObject);
    }
}
