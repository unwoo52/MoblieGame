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
            Debug.LogError("ERROR! 컴포넌트를 찾지 못했습니다.");
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


    IEnumerator LookatCorutine(Vector3 Target, float lookspeed)
    {
        // 현재 객체와 타겟 사이의 거리를 계산합니다.
        float distance = Vector3.Distance(transform.position, Target);

        // 거리가 1 이하인 경우, 즉 거의 같은 위치에 있는 경우 즉시 함수를 종료합니다.
        if (distance <= 1f)
        {
            yield break;
        }

        // 현재 객체의 위치와 타겟 위치를 기반으로 회전 방향을 계산합니다.
        Vector3 direction = Target - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // 현재 객체의 회전과 타겟 회전 간의 각도 차이를 계산합니다.
        float angle = Quaternion.Angle(transform.rotation, targetRotation);

        // 회전을 완료할 때까지 Coroutine을 실행합니다.
        while (angle > 0.01f)
        {
            // 회전 시간을 계산합니다.
            float time = Time.deltaTime * lookspeed;

            // Slerp를 사용하여 현재 회전 방향에서 목표 회전 방향으로 부드럽게 회전합니다.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, time);

            // 회전 방향과 각도를 다시 계산합니다.
            direction = Target - transform.position;
            targetRotation = Quaternion.LookRotation(direction);
            angle = Quaternion.Angle(transform.rotation, targetRotation);

            // 거리를 다시 계산합니다.
            distance = Vector3.Distance(transform.position, Target);

            // 거리가 1 이하인 경우, 즉 거의 같은 위치에 있는 경우 즉시 함수를 종료합니다.
            if (distance <= 1f)
            {
                yield break;
            }

            // 다음 프레임까지 기다립니다.
            yield return null;
        }

        // 회전을 완료하면 Coroutine을 종료합니다.
        yield break;
    }


    private bool Initialize()
    {
        if (TryGetComponent(out Animator animator)) _animator = animator; else return false;

        return true;
    }
}
