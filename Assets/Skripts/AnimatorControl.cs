using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class AnimatorControl : MonoBehaviour
{
    private Animator _animator;
    private int _isMoving = Animator.StringToHash("IsMoving");

    public bool IsMoving { private get;  set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _animator.SetBool(_isMoving, IsMoving);
    }
}
