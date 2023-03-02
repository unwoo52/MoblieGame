using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testController : MonoBehaviour
{
    private Vector3 currVec = new();
    void Update()
    {
        Lookat(currVec);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 pos = hit.point;
                currVec= pos;
            }
        }
    }
    public void Lookat(Vector3 toTarget)
    {
        Debug.DrawRay(toTarget, Vector3.up, Color.yellow, 10f);
        Quaternion targetRotation = Quaternion.LookRotation(toTarget - transform.position);
        transform.rotation =
            Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 1);
    }
}
