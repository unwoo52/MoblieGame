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
            Debug.LogError("Init() ����! ������Ʈ�� ã�� ���߽��ϴ�.");
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
            // 2���� 5�� ������ ������ �ð� ���ϱ�
            float waitTime = Random.Range(7f, 10.1f);

            // ������ �ð���ŭ ��ٸ���
            yield return new WaitForSeconds(waitTime);

            // ���� �ڵ� ����
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
