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
    private PathFinder _pathfinder;

    public Action<Vector3> OnMove => Move;
    public Action OnOnlyMove => OnlyMove;
    public Action OnStopMove => StopMove;
    public Action<Vector3, float> OnLookat => OnceLookat;
    public Action OnStopLookat => StopLookat;

    private void Start()
    {
        if (!Initialize())
            Debug.LogError("ERROR! ������Ʈ�� ã�� ���߽��ϴ�.");
    }



    [SerializeField]
    protected float _roateSpeed = 10;

    protected void Hit() { }

    protected void FootL() { }

    protected void FootR() { }

    protected void Attack()
    {
        _animator.SetTrigger("TriggerAttack");
    }

    public void OnceLookat(Vector3 toTarget, float lookspeed = 1)
    {
        Debug.DrawRay(toTarget, Vector3.up, Color.yellow, 10f);
        if (_lookCorutine != null) StopCoroutine(_lookCorutine);
        _lookCorutine = StartCoroutine(LookatCorutine(toTarget, lookspeed));
    }

    public void Lookat(Vector3 toTarget, float rotatespeed = 20f)
    {
        //Debug.Log(toTarget);
        Debug.DrawRay(toTarget, Vector3.up, Color.yellow, 10f);
        Quaternion targetRotation = Quaternion.LookRotation(toTarget - transform.position);
        transform.rotation =
            Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotatespeed);
    }

    public void Move(Vector3 toTarget)
    {
        Lookat(toTarget);
        _animator.SetBool("IsMove", true);
    }

    public void OnlyMove()
    {
        _animator.SetBool("IsMove", true);

    }

    public void StopMove()
    {
        _animator.SetBool("IsMove", false);
    }

    public void StopLookat()
    {
        if (_lookCorutine != null) StopCoroutine(_lookCorutine);
    }

    public void Scream()
    {
        _animator.SetTrigger("TriggerScream");
    }

    public void WanderWalk()
    {
        _animator.SetTrigger("WanderWalk");
    }


    IEnumerator LookatCorutine(Vector3 Target, float lookspeed)
    {
        // ���� ��ü�� Ÿ�� ������ �Ÿ��� ����մϴ�.
        float distance = Vector3.Distance(transform.position, Target);

        // �Ÿ��� 1 ������ ���, �� ���� ���� ��ġ�� �ִ� ��� ��� �Լ��� �����մϴ�.
        if (distance <= 1f)
        {
            yield break;
        }

        // ���� ��ü�� ��ġ�� Ÿ�� ��ġ�� ������� ȸ�� ������ ����մϴ�.
        Vector3 direction = Target - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // ���� ��ü�� ȸ���� Ÿ�� ȸ�� ���� ���� ���̸� ����մϴ�.
        float angle = Quaternion.Angle(transform.rotation, targetRotation);

        // ȸ���� �Ϸ��� ������ Coroutine�� �����մϴ�.
        while (angle > 0.01f)
        {
            // ȸ�� �ð��� ����մϴ�.
            float time = Time.deltaTime * lookspeed;

            // Slerp�� ����Ͽ� ���� ȸ�� ���⿡�� ��ǥ ȸ�� �������� �ε巴�� ȸ���մϴ�.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, time);

            // ȸ�� ����� ������ �ٽ� ����մϴ�.
            direction = Target - transform.position;
            targetRotation = Quaternion.LookRotation(direction);
            angle = Quaternion.Angle(transform.rotation, targetRotation);

            // �Ÿ��� �ٽ� ����մϴ�.
            distance = Vector3.Distance(transform.position, Target);

            // �Ÿ��� 1 ������ ���, �� ���� ���� ��ġ�� �ִ� ��� ��� �Լ��� �����մϴ�.
            if (distance <= 1f)
            {
                yield break;
            }

            // ���� �����ӱ��� ��ٸ��ϴ�.
            yield return null;
        }

        // ȸ���� �Ϸ��ϸ� Coroutine�� �����մϴ�.
        yield break;
    }


    private bool Initialize()
    {
        if (TryGetComponent(out Animator animator)) _animator = animator; else return false;
        if (TryGetComponent(out PathFinder pathfinder)) _pathfinder = pathfinder; else return false;

        return true;
    }
}
