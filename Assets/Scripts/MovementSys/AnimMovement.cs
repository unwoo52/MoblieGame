using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimMovement : MonoBehaviour
{
    [SerializeField]
    protected Animator _animator;


    [SerializeField]
    protected float _roateSpeed = 10;

    protected Dictionary<string, float> _clipTimes = new Dictionary<string, float>();


    protected void Hit() { }

    protected void FootL() { }

    protected void FootR() { }

    protected void Attack()
    {
        _animator.SetTrigger("TriggerAttack");
    }

    public void Lookat(Vector3 toTarget)
    {
        Quaternion targetRotation = Quaternion.LookRotation(toTarget);
        transform.rotation =
            Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _roateSpeed);
    }

    public void Move(Vector3 toTarget)
    {
        Lookat(toTarget);
        _animator.SetBool("IsMove", true);
    }

    public void StopMove()
    {
        _animator.SetBool("IsMove", false);
    }

    public void Scream()
    {
        _animator.SetTrigger("TriggerScream");
    }

    public void WanderWalk()
    {
        _animator.SetTrigger("WanderWalk");
    }
}
