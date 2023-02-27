using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class PathFinder : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _agent;

    public Action<Vector3> onMove;
    public Action onStop;

    private NavMeshPath _path = null;

    private Vector3 _currentDestination = new Vector3();

    private int _cornerIdx = 0;

    public void SetDestination(Vector3 dest)
    {
        _agent.ResetPath();
        _agent.CalculatePath(dest, _path);
    }

    private void Awake()
    {
        _path = new NavMeshPath();
    }

    private void Update()
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
}
