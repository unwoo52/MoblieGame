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
        Idle, // 배회. 주변을 배회
        curiosity, // 호기심. 소음에 이끌려 좀비가 모일 때 발동
        Tracking,//추적. 적을 인지해서 추적하는 상황
        Moribund,//시체상태. 부활의 여지가 있는 상황
        Death//온전한 사망. 잠시 후 시체가 사라짐
    }

    private ZOMBIEBEHAVIOR _currentState = ZOMBIEBEHAVIOR.Start;

    private void Start()
    { 
        //ChangerState(ZOMBIEBEHAVIOR.Idle); 


        if (!TryGetComponent(out AnimMovement _animMovement))
        {
            Debug.LogError("애니메이터가 존재하지 않습니다.");
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
            // 2에서 5초 사이의 랜덤한 시간 구하기
            float waitTime = Random.Range(6f, 6.1f);

            // 랜덤한 시간만큼 기다리기
            yield return new WaitForSeconds(waitTime);

            // 다음 코드 실행
            //random dir Lookat
            _RandomDir.x = Random.Range(-100f, 100f);
            _RandomDir.z = Random.Range(-100f, 100f);

            Debug.DrawRay(transform.position, _RandomDir,Color.red, 2.0f);

            if (!TryGetComponent(out AnimMovement _animMovement))
            {
                Debug.LogError("애니메이터가 존재하지 않습니다.");
                this._animMovement = _animMovement;
            }
            _animMovement.Lookat(_RandomDir);
            //trigger
            //_animMovement.WanderWalk();

        }
    }
}
