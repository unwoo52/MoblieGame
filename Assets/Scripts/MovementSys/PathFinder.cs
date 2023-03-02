using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using static UnityEditor.PlayerSettings;

public class PathFinder : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _agent;

    private AnimMovement _animmove;

    private Coroutine coNavmove;

    private NavMeshPath _path = null;


    private float arrivalRange = 0.1f;
    private int currentCornerIndex = 0;

    private void Awake()
    {

        if(TryGetComponent(out AnimMovement animmove)) _animmove = animmove;
        _path = new NavMeshPath();
    }


    public void SetDestination(Vector3 dest)
    {
        //==before code==//
        /*
        _path.ClearCorners();
        _agent.CalculatePath(dest, _path);
        foreach(Vector3 temp in _path.corners)
        {
            Debug.Log(temp);
        }*/
        //=======test===//
        
        _path.ClearCorners();
        _agent.ResetPath();
        if(_agent.CalculatePath(dest, _path))
        {

            StartCoroutine(WaitPathComplete(dest));
            /*
            if (coNavmove != null) StopCoroutine(coNavmove);
            coNavmove = StartCoroutine(NavMove(_path.corners));

            Debug.DrawRay(transform.position, dest - transform.position, Color.blue, 5f);
            Debug.DrawRay(dest + (Vector3.up * 2), Vector3.up * 5, Color.black, 5f);
            testRay(_path.corners);
            */
        }
    
        //=============
    }

    IEnumerator WaitPathComplete(Vector3 dest)
    {
        while(true){
            if (_path.status == NavMeshPathStatus.PathComplete)
            {
                if (coNavmove != null) StopCoroutine(coNavmove);
                coNavmove = StartCoroutine(NavMove(_path.corners));

                Debug.DrawRay(transform.position, dest - transform.position, Color.blue, 5f);
                Debug.DrawRay(dest + (Vector3.up), Vector3.up * 5, Color.black, 5f);
                testRay(_path.corners);
                foreach (Vector3 temp in _path.corners)
                {
                    Debug.Log(temp);
                }

                yield break;
            }
            yield return null;
        }
    }

    IEnumerator NavMove(Vector3[] pointlist)
    {
        if (_path.corners.Length <= 1)
        {
            if (coNavmove != null) StopCoroutine(NavMove(pointlist));
            yield break;
        }

        //distance 정의 목적지는, 다음 포인트와의 거리
        Vector3 destination = transform.position - pointlist[pointlist.Length - 1];
        float distance = destination.magnitude;

        currentCornerIndex = 1;
        _animmove.OnLookat(pointlist[currentCornerIndex], testfloat);
        _animmove.OnlyMove();
        while (distance >= arrivalRange)
        {
            destination = transform.position - pointlist[currentCornerIndex + 1];
            distance = destination.magnitude;
            
            //코너에 도착하면, 코너 저장되는 멤버필드 수정
            if(distance < arrivalRange)
            {
                //마지막이라면, 이동 코루틴 종료
                if (currentCornerIndex == pointlist.Length - 1)
                {
                    _animmove.OnStopLookat();
                    _animmove.OnStopMove();
                    yield break;
                }
                currentCornerIndex++;
                _animmove.OnLookat(pointlist[currentCornerIndex], testfloat);
                Debug.Log(currentCornerIndex);
            }
            yield return null;
        }
    }

    public float testfloat;


    /*TEST METHOD 나중에 지울 것*/
    /*
     * 
    private void Update()
    {
        BeforeUpdateMethod();

    }

    private void BeforeUpdateMethod()
    {
        if (_path.corners.Length == _cornerIdx)
        {
            Clear();
            if (onStop != null)
            {
                onStop();
            }
        }

        if (_path.status == NavMeshPathStatus.PathComplete)
        {
            //매번 업데이트 때 마다 실행되지만, 나중에 코너에 도달했을 때 만 실행하도록 개선 가능
            _currentDestination = _path.corners[_cornerIdx];

            if (HasArriveDest())
            {
                _cornerIdx++;
            }
            else
            {
                Move();
            }
        }

    }
    private bool HasArriveDest()
    {
        if ((_currentDestination - transform.position).magnitude < 0.5f)
        {
            return true;
        }
        return false;
    }

    private void Move()
    {
        if (onMove != null)
        {
            Vector3 toTarget = (_currentDestination - transform.position).normalized;
            onMove(toTarget);
        }
    }
    private void Clear()
    {
        _cornerIdx = 0;
        _path.ClearCorners();
    }
    */
    private void testRay(Vector3[] pointlist)
    {
        for (int i = 0; i < pointlist.Length - 1; ++i)
        {
            Debug.Log(pointlist[i] + " => " + pointlist[i + 1]);
            Debug.DrawRay(pointlist[i], pointlist[i + 1] - pointlist[i], Color.red, 3f);
        }
    }
}
