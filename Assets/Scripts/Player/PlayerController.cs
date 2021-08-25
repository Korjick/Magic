using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamagable
{
    private Animator _animator;
    
    public int health;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        SwipeDetection.OnSwipeDirection += Attack;
    }

    private void OnDisable()
    {
        SwipeDetection.OnSwipeDirection -= Attack;
    }

    private void Attack(Vector2 dir)
    {
        if(dir == Vector2.up)
            _animator.SetTrigger("Up");
        if(dir == Vector2.down)
            _animator.SetTrigger("Down");
        if(dir == Vector2.left)
            _animator.SetTrigger("Left");
        if(dir == Vector2.right)
            _animator.SetTrigger("Right");
        if(dir == Vector2.one)
            _animator.SetTrigger("UR");
        if(dir == Vector2.one * -1)
            _animator.SetTrigger("UL");
        if(dir == new Vector2(1, -1))
            _animator.SetTrigger("DR");
        if(dir == new Vector2(-1, 1))
            _animator.SetTrigger("DL");
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
