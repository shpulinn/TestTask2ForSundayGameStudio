using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Character movement stats")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Button shootButton;

    [Header("Gravity handling")]
    private float _currentAttractionCharacter = 0;
    [SerializeField] private float _gravityForce = 20;

    [Header("Character components")]
    private CharacterController _characterController;
    private Animator _animator;

    [Header("Shooting")]
    [Space]
    [SerializeField] private Transform arrowSpawnPosition;
    [SerializeField] private Transform arrowPrefab;
    [SerializeField] private float reloadingTime = 1f;
    [SerializeField] private Image reloadingBar;

    [Header("Walking")]
    [SerializeField] private Transform footPrint_L;
    [SerializeField] private Transform footPrint_R;

    private float _reloadintTimer;
    private bool _isReloading = false;

    private ArrowPool _arrowPool;
    private FootprintsPool _footprintsPool;

    private void Start ()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _arrowPool = GetComponent<ArrowPool>();
        _footprintsPool = GetComponent<FootprintsPool>();
        shootButton.onClick.AddListener(HandleShooting);
    }

    private void Update ()
    {
        GravityHandling();

        if (joystick.Horizontal != 0 || joystick.Vertical != 0) {
            MoveCharacter(new Vector3(joystick.Horizontal, 0, joystick.Vertical));
            RotateCharacter(new Vector3(joystick.Horizontal, 0, joystick.Vertical));
            _animator.SetFloat("Velocity", 1);
        } else {
            _animator.SetFloat("Velocity", 0);
        }
    }

    private void HandleShooting()
    {
        if (!_isReloading) {
            _animator.SetTrigger("Shoot");
            _isReloading = true;
        }     
    }

    public void MoveCharacter (Vector3 moveDirection)
    {
        moveDirection = moveDirection * _moveSpeed;
        moveDirection.y = _currentAttractionCharacter;
        _characterController.Move(moveDirection * Time.deltaTime);
    }

    public void RotateCharacter (Vector3 moveDirection)
    {
        if (_characterController.isGrounded) {
            if (Vector3.Angle(transform.forward, moveDirection) > 0) {
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, _rotateSpeed, 0);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }
    }

    private void GravityHandling ()
    {
        if (!_characterController.isGrounded) {
            _currentAttractionCharacter -= _gravityForce * Time.deltaTime;
        }
        else {
            _currentAttractionCharacter = 0;
        }
    }

    private void Reload()
    {
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        while (_reloadintTimer < reloadingTime) {
            _reloadintTimer += Time.deltaTime;
            reloadingBar.fillAmount = Mathf.Clamp(_reloadintTimer, 0, 1);
            yield return null;
        }
        _isReloading = false;
        _reloadintTimer = 0f;
        reloadingBar.fillAmount = 0f;
        yield return null;
    }

    private void InitArrow()
    {
        ArrowProjectile arrow = _arrowPool.GetObject();
        arrow.transform.position = arrowSpawnPosition.position;
        arrow.transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
        arrow.Init();
    }

    // Shoot, spawn foorprint methods are called from animation event
    private void Shoot ()
    {
        InitArrow();
        Reload();
    }

    private void SpawnFootprintLeft()
    {
        GameObject leftFootPrint = _footprintsPool.GetLeftFootprint();
        leftFootPrint.transform.position = new Vector3(transform.position.x, 0.002f, transform.position.z);
        leftFootPrint.transform.rotation = transform.rotation;

    }

    private void SpawnFootprintRight()
    {
        GameObject rightFootPrint = _footprintsPool.GetRightFootprint();
        rightFootPrint.transform.position = new Vector3(transform.position.x, 0.002f, transform.position.z);
        rightFootPrint.transform.rotation = transform.rotation;
    }
}
