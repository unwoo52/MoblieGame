using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFSM : MonoBehaviour
{
    [SerializeField] private ZombieFSM_Idle _zombieFSM_Idle;
    [SerializeField] private ZombieFSM_Tracking _zombieFSM_Tracking;
    [SerializeField] private AnimMovement _animMovement;
    public AnimMovement AnimMove { get => _animMovement; }
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
        if(!Initialize()) 
            Debug.LogError("Init() ����! ������Ʈ�� ã�� ���߽��ϴ�.");

        //ChangerState(ZOMBIEBEHAVIOR.Idle); 
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

    private bool Initialize()
    {
        if (TryGetComponent(out AnimMovement animMovement)) _animMovement = animMovement; else return false;
        if (TryGetComponent(out ZombieFSM_Idle zombieFSM_Idle)) _zombieFSM_Idle = zombieFSM_Idle; else return false;
        if (TryGetComponent(out ZombieFSM_Tracking zombieFSM_Tracking)) _zombieFSM_Tracking = zombieFSM_Tracking; else return false;

        return true;
    }
}
