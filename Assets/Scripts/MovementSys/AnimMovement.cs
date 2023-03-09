using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AnimMovement : MonoBehaviour
{
    private Coroutine _lookCorutine = null;
    public Coroutine LookCorutine { get { return _lookCorutine; } }
    [SerializeField]
    private Animator _animator;


    private void Start()
    {
        if (!Initialize())
            Debug.LogError("ERROR! ������Ʈ�� ã�� ���߽��ϴ�.");
    }

    protected void Hit() { }

    protected void FootL() { }

    protected void FootR() { }

    protected void Attack()
    {
        _animator.SetTrigger("TriggerAttack");
    }

    protected void OnceLookat(Vector3 toTarget, float lookspeed = 1)
    {
        if (_lookCorutine != null) StopCoroutine(_lookCorutine);
        _lookCorutine = StartCoroutine(LookatCorutine(toTarget, lookspeed));
    }

    protected void Lookat(Vector3 toTarget, float rotatespeed = 20f)
    {
        Quaternion targetRotation = Quaternion.LookRotation(toTarget - transform.position);
        transform.rotation =
            Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotatespeed);
    }

    protected void Move(Vector3 toTarget)
    {
        Lookat(toTarget);
        _animator.SetBool("IsMove", true);
    }

    protected void OnlyMove()
    {
        _animator.SetBool("IsMove", true);

    }

    protected void StopMove()
    {
        _animator.SetBool("IsMove", false);
    }

    protected void StopLookat()
    {
        if (_lookCorutine != null) StopCoroutine(_lookCorutine);
    }

    protected void Scream()
    {
        _animator.SetTrigger("TriggerScream");
    }

    protected void WanderWalk()
    {
        _animator.SetTrigger("WanderWalk");
    }

    protected void ExitWanderWalk()
    {
        _animator.SetTrigger("ExitWanderWalk");
    }



    IEnumerator LookatCorutine(Vector3 Target, float lookspeed)
    {
        // ���� ��ü�� ��ġ�� Ÿ�� ��ġ�� ������� ȸ�� ������ ����մϴ�.
        Vector3 dir = Target - transform.position;
        if (dir.magnitude <= Mathf.Epsilon) yield break;
        dir.Normalize();

        //d = ����
        float d = Vector3.Dot(dir, transform.forward);

        //r = radian���� ��ũcos���� ��ȯ�� ��
        float r = Mathf.Acos(d);
        float angle = r * Mathf.Rad2Deg;

        if (angle > Mathf.Epsilon)
        {
            float rotDir = Vector3.Dot(dir, transform.right) < 0.0f ? -1.0f : 1.0f;
            while (angle > Mathf.Epsilon)
            {
                float delta = lookspeed * Time.deltaTime;
                if (delta > angle)
                {
                    delta = angle;
                }
                angle -= delta;
                transform.Rotate(delta * rotDir * Vector3.up, Space.World);
                yield return null;
            }
        }
    }


    private bool Initialize()
    {
        if (TryGetComponent(out Animator animator)) _animator = animator; else return false;

        return true;
    }
}
