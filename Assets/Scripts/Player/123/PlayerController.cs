using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private CharacterController _characterController;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _animator.SetBool("isRunning", true);
            _characterController.Move(Vector3.forward);
        }
        else if(Input.GetKeyUp(KeyCode.W))
        {
            _animator.SetBool("isRunning", false);
        }
    }
}
