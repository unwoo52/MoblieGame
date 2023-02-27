using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFSM : MonoBehaviour
{
    private ZombieFSM_Idle _zombieFSM_Idle;


    private AnimMovement _animMovement;
    public AnimMovement AnimMove { get => _animMovement; }
    [SerializeField] private ZombieFSM_Idle _zombieFSM;
    public enum ZOMBIEBEHAVIOR
    {
        Start,
        Idle, // ��ȸ. �ֺ��� ��ȸ
        curiosity, // ȣ���. ������ �̲��� ���� ���� �� �ߵ�
        Attck, // �÷��̾ ��ġ�� ������Ʈ�� �߰��ϰ� ������ ��
        Tracking,//����. ���� �����ؼ� �����ϴ� ��Ȳ
        Moribund,//��ü����. ��Ȱ�� ������ �ִ� ��Ȳ
        Death//������ ���. ��� �� ��ü�� �����
    }

    private ZOMBIEBEHAVIOR _currentState = ZOMBIEBEHAVIOR.Start;

    private void Start()
    {
        if(!Init()) 
            Debug.LogError("Init() ����! ������Ʈ�� ã�� ���߽��ϴ�.");

        ChangerState(ZOMBIEBEHAVIOR.Idle); 

    }

    //inerface�� updo enddo startdo�� ����

    private void Update()
    {
        //FSM(interface)
        StateMachine();
    }

    public void StateMachine() //FSM
    {

        //interface.updo();
        switch (_currentState)
        {
            case ZOMBIEBEHAVIOR.Start:
                break;
            case ZOMBIEBEHAVIOR.Idle:
                _zombieFSM_Idle.StateMachine();
                break;
            case ZOMBIEBEHAVIOR.curiosity:
                break;
            case ZOMBIEBEHAVIOR.Attck:
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
        //FSM(interface)
        //_currentState = newState;
        //FSM(interface)
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
                _zombieFSM_Idle.EndStateBehavior();
                break;
            case ZOMBIEBEHAVIOR.curiosity:
                break;
            case ZOMBIEBEHAVIOR.Attck:
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
                _zombieFSM_Idle.StartStateBehavior();
                break;
            case ZOMBIEBEHAVIOR.curiosity:
                break;
            case ZOMBIEBEHAVIOR.Attck:
                break;
            case ZOMBIEBEHAVIOR.Tracking:
                break;
            case ZOMBIEBEHAVIOR.Moribund:
                break;
            case ZOMBIEBEHAVIOR.Death:
                break;
        }
    }

    /* codes */

    private bool Init()
    {
        _zombieFSM_Idle = GetComponent<ZombieFSM_Idle>();
        if (_zombieFSM_Idle == null) return false;


        _animMovement = GetComponent<AnimMovement>();
        if (_animMovement == null) return false;


        return true;
    }

}
