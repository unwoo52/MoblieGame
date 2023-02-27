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
        Idle, // 배회. 주변을 배회
        curiosity, // 호기심. 소음에 이끌려 좀비가 모일 때 발동
        Attck, // 플레이어가 설치한 오브젝트를 발견하고 공격할 때
        Tracking,//추적. 적을 인지해서 추적하는 상황
        Moribund,//시체상태. 부활의 여지가 있는 상황
        Death//온전한 사망. 잠시 후 시체가 사라짐
    }

    private ZOMBIEBEHAVIOR _currentState = ZOMBIEBEHAVIOR.Start;

    private void Start()
    {
        if(!Init()) 
            Debug.LogError("Init() 실패! 컴포넌트를 찾지 못했습니다.");

        ChangerState(ZOMBIEBEHAVIOR.Idle); 

    }

    //inerface는 updo enddo startdo가 있음

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
