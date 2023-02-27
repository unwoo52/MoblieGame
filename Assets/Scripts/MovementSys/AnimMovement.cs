using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AnimMovement : MonoBehaviour
{
    private Coroutine lookCorutine = null;
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

    public void OnceLookat(Vector3 toTarget)
    {
        if (lookCorutine != null) StopCoroutine(lookCorutine);
        lookCorutine = StartCoroutine(LookatCorutine(toTarget));

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

    IEnumerator LookatCorutine(Vector3 Target)
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
            float time = Time.deltaTime * _roateSpeed / 15;

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
}
