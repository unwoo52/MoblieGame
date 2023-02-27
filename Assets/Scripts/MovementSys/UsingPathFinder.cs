using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class UsingPathFinder : MonoBehaviour
{

    [SerializeField]
    private NavMeshAgent _agent;


    [SerializeField]
    private AnimMovement _myAnimController;

    [SerializeField]
    private PathFinder _pathfinder;

    void Awake()
    {
        _pathfinder.onMove = _myAnimController.Move;
        _pathfinder.onStop = _myAnimController.StopMove;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 pos = hit.point;
                _pathfinder.SetDestination(pos);
            }
        }
    }
}
