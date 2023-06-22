using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    [SerializeField] private float arrowSpeed = 30f;
    [SerializeField] private float arrowDamage = 20f;

    private Rigidbody _arrowRigidbody;

    public float ArrowDamage
    {
        get => arrowDamage;
    }

    private void Awake ()
    {
        _arrowRigidbody = GetComponent<Rigidbody>();
    }

    private void Start ()
    {
        _arrowRigidbody.velocity = transform.forward * arrowSpeed;
    }

    private void OnTriggerEnter (Collider other)
    {
        Destroy(gameObject);
    }
}
