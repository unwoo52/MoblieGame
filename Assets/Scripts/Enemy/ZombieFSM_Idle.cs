using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFSM_Idle : MonoBehaviour
{
    private Vector3 _RandomDir = new();
    private Coroutine idleCorutine = null;
    private ZombieFSM _zombieFSM;
    private void Start()
    {
        if (!Initialized())
            Debug.LogError("Init() 실패! 컴포넌트를 찾지 못했습니다.");
    }
    public void EndStateBehavior()
    {
        StopCoroutine(idleCorutine);
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
            _zombieFSM.AnimMove.OnceLookat(_RandomDir);

            //
            //trigger
            _zombieFSM.AnimMove.WanderWalk();

        }
    }

    private bool Initialized()
    {
        _zombieFSM = GetComponent<ZombieFSM>();
        if (_zombieFSM == null) return false;

        return true;
    }
}
