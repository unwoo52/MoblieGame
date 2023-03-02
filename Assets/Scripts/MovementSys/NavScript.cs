using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class NavScript : MonoBehaviour
{
    [SerializeField] private LayerMask BuildingLayerMask;
    private Coroutine coNavMove;
    private AnimMovement _animMovement;
    private NavMeshPath _path;
    private void Start()
    {
        _path = new();
        if (!Initialize())
            Debug.LogError("ERROR! 컴포넌트를 찾지 못했습니다.");
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 10000, BuildingLayerMask))
            {
                Vector3 pos = hit.point;

                Debug.DrawRay(transform.position, pos - transform.position, Color.blue, 5f);
                Debug.DrawRay(pos + (Vector3.up*2), Vector3.up*5, Color.black, 5f);

                _path.ClearCorners();
                if (NavMesh.CalculatePath(transform.position, pos, 1 << NavMesh.GetAreaFromName("Walkable"), _path))
                {
                    if(coNavMove != null) StopNavMove();
                    coNavMove = StartCoroutine(NavMove(_path.corners));

                    testRay(_path.corners);

                }
            }
        }
    }

    IEnumerator NavMove(Vector3[] pointlist)
    {
        if(coNavMove != null) StopCoroutine(coNavMove);
        if(pointlist.Length <= 1) yield return null;
        int postVectorPoint = 1;


        while (postVectorPoint < pointlist.Length)
        {
            Vector3 direction = pointlist[postVectorPoint] - pointlist[postVectorPoint - 1];
            float distance = direction.magnitude;
            direction.Normalize();
            
            _animMovement.Lookat(pointlist[postVectorPoint]);
            //Debug.DrawRay(pointlist[postVectorPoint], Vector3.up, Color.yellow, 1.0f);
            _animMovement.OnlyMove();


            if (distance <= Mathf.Epsilon) 
            {
                postVectorPoint++;
            }

            yield return null;
        }
    }

    private void testRay(Vector3[] pointlist)
    {
        for(int i = 0; i < pointlist.Length -1; ++i)
        {
            Debug.DrawRay(pointlist[i], pointlist[i + 1], Color.red, 3f);
        }
    }

    private void StopNavMove()
    {
        StopCoroutine(coNavMove);
        _animMovement.StopMove();
    }

    private bool Initialize()
    {

        if (TryGetComponent(out AnimMovement animMovement)) _animMovement = animMovement; else return false;

        return true;
    }

}
