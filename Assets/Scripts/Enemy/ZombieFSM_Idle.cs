using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFSM_Idle : MonoBehaviour
{
    private Vector3 _RandomDir = new();
    private Coroutine idleCorutine = null;
    [SerializeField] private ZombieFSM _zombieFSM;
    private AnimContoller _anim;
    private void Start()
    {
        if (!Initialized())
            Debug.LogError("Init() 실패! 컴포넌트를 찾지 못했습니다.");
    }
    public void EndStateBehavior()
    {
        StopCoroutine(idleCorutine);
        //OnStopLookat
        //StopCoroutine(_zombieFSM.AnimMove.LookCorutine);
    }

    public void StartStateBehavior()
    {
        idleCorutine = StartCoroutine(IdelRandomMove());
    }
    public void StateMachine()
    {
    }

    IEnumerator IdelRandomMove()
    {
        while (true)
        {
            // 2에서 5초 사이의 랜덤한 시간 구하기
            float waitTime = Random.Range(7f, 10.1f);

            // 랜덤한 시간만큼 기다리기
            yield return new WaitForSeconds(waitTime);

            // 다음 코드 실행
            //random dir Lookat
            _RandomDir.x = Random.Range(-100f, 100f);
            _RandomDir.z = Random.Range(-100f, 100f);


            _RandomDir.Normalize();

            _RandomDir.x *= 3;
            _RandomDir.z *= 3;


            Debug.Log(_RandomDir);

            _RandomDir.x += transform.position.x;
            _RandomDir.z += transform.position.z;


            Debug.DrawRay(_RandomDir, Vector3.up, Color.red, 4.0f);
            _anim.OnLookat(_RandomDir , 1);

            //
            //trigger
            _anim.OnWanderWalk();

        }
    }

    private bool Initialized()
    {
        if (TryGetComponent(out ZombieFSM zombieFSM)) _zombieFSM = zombieFSM; else return false;
        if (TryGetComponent(out AnimContoller animContoller)) _anim = animContoller; else return false;

        return true;
    }
}
