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

    [Header("Walking")]
    [SerializeField] private Transform footPrint_L;
    [SerializeField] private Transform footPrint_R;

    private void Start ()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
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
        _animator.SetTrigger("Shoot");
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

    private void Shoot ()
    {
        Vector3 aimDirection = transform.forward;//(_mouseWorldPosition - bulletSpawnPosition.position).normalized;
        Instantiate(arrowPrefab, arrowSpawnPosition.position,
        Quaternion.LookRotation(aimDirection, Vector3.up));
    }

    private void SpawnFootprintLeft()
    {
        Instantiate(footPrint_L, new Vector3(transform.position.x, 0.002f, transform.position.z), transform.rotation);
    }

    private void SpawnFootprintRight()
    {
        Instantiate(footPrint_R, new Vector3(transform.position.x, 0.002f, transform.position.z), transform.rotation);
    }
}
