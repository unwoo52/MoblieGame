using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

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
        _path.ClearCorners();
        _agent.ResetPath();
        if(_agent.CalculatePath(dest, _path))
        {

            StartCoroutine(WaitPathComplete(dest));
        }
    }

    IEnumerator WaitPathComplete(Vector3 dest)
    {
        while(true){
            if (_path.status == NavMeshPathStatus.PathComplete)
            {
                if (coNavmove != null) StopCoroutine(coNavmove);
                coNavmove = StartCoroutine(NavMove(_path.corners));
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

        Vector3 destination = transform.position - pointlist[pointlist.Length - 1];
        float distance = destination.magnitude;

        currentCornerIndex = 1;
        _animmove.OnlyMove();
        while (true)
        {
            if (distance < arrivalRange)
            {
                currentCornerIndex++;
                if (currentCornerIndex == pointlist.Length)
                {
                    _animmove.OnStopMove();
                    yield break;
                }
                Debug.Log(currentCornerIndex);
            }
            destination = transform.position - pointlist[currentCornerIndex];
            distance = destination.magnitude;
            Debug.Log("Lookat" + pointlist[currentCornerIndex]);
            _animmove.Lookat(pointlist[currentCornerIndex]);

            yield return null;
        }
    }
}
