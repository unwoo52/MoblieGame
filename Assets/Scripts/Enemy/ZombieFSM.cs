using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFSM : MonoBehaviour
{
    private Vector3 _RandomDir = new();
    private Coroutine idleCorutine = null;
    private AnimMovement _animMovement;
    public enum ZOMBIEBEHAVIOR
    {
        Start,
        Idle, // ��ȸ. �ֺ��� ��ȸ
        curiosity, // ȣ���. ������ �̲��� ���� ���� �� �ߵ�
        Tracking,//����. ���� �����ؼ� �����ϴ� ��Ȳ
        Moribund,//��ü����. ��Ȱ�� ������ �ִ� ��Ȳ
        Death//������ ���. ��� �� ��ü�� �����
    }

    private ZOMBIEBEHAVIOR _currentState = ZOMBIEBEHAVIOR.Start;

    private void Start()
    { 
        //ChangerState(ZOMBIEBEHAVIOR.Idle); 


        if (!TryGetComponent(out AnimMovement _animMovement))
        {
            Debug.LogError("�ִϸ����Ͱ� �������� �ʽ��ϴ�.");
            this._animMovement = _animMovement;
        }
    }

    private void Update()
    {
        StateMachine();
    }

    public void StateMachine()
    {
        switch (_currentState)
        {
            case ZOMBIEBEHAVIOR.Start:
                break;
            case ZOMBIEBEHAVIOR.Idle:
                break;
            case ZOMBIEBEHAVIOR.curiosity:
                break;
            case ZOMBIEBEHAVIOR.Tracking:
                break;
            case ZOMBIEBEHAVIOR.Moribund:
                break;
            case ZOMBIEBEHAVIOR.Death:
                break;
        }
    }

    public void ChangerState(ZOMBIEBEHAVIOR newState)
    {
        EndStateBehavior();
        _currentState = newState;
        StartStateBehavior();
    }

    private void EndStateBehavior()
    {
        switch (_currentState)
        {
            case ZOMBIEBEHAVIOR.Start:
                break;
            case ZOMBIEBEHAVIOR.Idle:
                StopCoroutine(idleCorutine);
                break;
            case ZOMBIEBEHAVIOR.curiosity:
                break;
            case ZOMBIEBEHAVIOR.Tracking:
                break;
            case ZOMBIEBEHAVIOR.Moribund:
                break;
            case ZOMBIEBEHAVIOR.Death:
                break;
        }
    }

    private void StartStateBehavior()
    {
        switch (_currentState)
        {
            case ZOMBIEBEHAVIOR.Start:
                break;
            case ZOMBIEBEHAVIOR.Idle:
                idleCorutine = StartCoroutine(IdelRandomMove());
                break;
            case ZOMBIEBEHAVIOR.curiosity:
                break;
            case ZOMBIEBEHAVIOR.Tracking:
                break;
            case ZOMBIEBEHAVIOR.Moribund:
                break;
            case ZOMBIEBEHAVIOR.Death:
                break;
        }
    }

    IEnumerator IdelRandomMove()
    {
        while (true)
        {
            // 2���� 5�� ������ ������ �ð� ���ϱ�
            float waitTime = Random.Range(6f, 6.1f);

            // ������ �ð���ŭ ��ٸ���
            yield return new WaitForSeconds(waitTime);

            // ���� �ڵ� ����
            //random dir Lookat
            _RandomDir.x = Random.Range(-100f, 100f);
            _RandomDir.z = Random.Range(-100f, 100f);

            Debug.DrawRay(transform.position, _RandomDir,Color.red, 2.0f);

            if (!TryGetComponent(out AnimMovement _animMovement))
            {
                Debug.LogError("�ִϸ����Ͱ� �������� �ʽ��ϴ�.");
                this._animMovement = _animMovement;
            }
            _animMovement.Lookat(_RandomDir);
            //trigger
            //_animMovement.WanderWalk();

        }
    }
}
