using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 50;
    [SerializeField] private float timeBeforeDisappear = 5f;

    private BoxCollider _collider;
    private Animator _animator;
    private float _currentHealth = 0;

    private void Awake ()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider>();

        _currentHealth = maxHealth;
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.TryGetComponent(out ArrowProjectile arrow)) {
            HitWithDamage(arrow.ArrowDamage);
        }
    }

    private void HitWithDamage(float damage)
    {
        if (damage < 0) {
            throw new System.Exception("Damage can't be a negative number");
        }
        _currentHealth -= damage;
        if (_currentHealth <= 0) {
            Death();
        } else
            _animator.SetTrigger("Hit");
    }

    private void Death()
    {
        _animator.SetTrigger("Death");
        _collider.enabled = false;
        Destroy(gameObject, timeBeforeDisappear);
    }
}
