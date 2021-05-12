using UnityEngine;
using System.Collections;

public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target; 
    public float rotSpeed = 15.0f;
    public float moveSpeed = 6.0f;
    private CharacterController _charController;
    private Animator _animator;
    
    public float jumpSpeed = 5.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    private float _vertSpeed;
    void Start() 
    {
        _animator = GetComponent<Animator>();
        _charController = GetComponent<CharacterController>(); 
        _vertSpeed = minFall;
    }

    void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        
        if (horInput != 0 || vertInput != 0)
        {
            if (_charController.isGrounded)
            {
                _animator.SetBool("isRunning", true);
            }
            
            movement.x = horInput;
            movement.z = vertInput;
            movement.x = horInput * moveSpeed; 
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);
            Quaternion tmp = target.rotation; 
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement); 
            target.rotation = tmp;
            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation,
                direction, rotSpeed * Time.deltaTime);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
        
        if (_charController.isGrounded)
        { 
            if (Input.GetButtonDown("Jump"))
            { 
                _animator.SetBool("isRunning", false);
                _vertSpeed = jumpSpeed;
            }
            else
            {
                _vertSpeed = minFall;
            }
        }
        else
        { 
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)
            {
                _vertSpeed = terminalVelocity;
            }
        }

        movement.y = _vertSpeed;
        movement *= Time.fixedDeltaTime; 
        _charController.Move(movement);
    }
}
