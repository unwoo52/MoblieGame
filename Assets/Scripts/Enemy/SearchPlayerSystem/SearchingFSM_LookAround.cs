using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayerData
{
    public GameObject Player;
    public float watchedGauge = 0f; //max까지 차면, Tracking 시작
    public TargetPlayerData(GameObject gameObject)
    {
        Player = gameObject;
    }
}

public class SearchingFSM_LookAround : MonoBehaviour
{
    private float watchedValue = 5.0f; //매 초 마다 가해지는 값

    private float watchedGagueMax = 100.0f;

    private float visionRange = 100.0f;

    [SerializeField] private LayerMask ObstacleMask;

    private List<TargetPlayerData> TargetPlayerList = new List<TargetPlayerData>();
    [SerializeField] private LayerMask TargetLayerMask;

    private AnimMovement animMovement;

    private WaitForSeconds WaitTimeIsLostTarget = new(3.0f);
    //get ray

    private void Start()
    {
        if (!TryGetComponent(out AnimMovement animMovement)) Debug.LogError("이 enemy오브젝트에 AnimMovement컴포넌트를 추가해야 합니다.");
    }

    public void GetRay(Ray ray, GameObject gameObject)
    {
        TargetPlayerData targetPlayerData = FindTargetPlayerData(gameObject);

        AddWeight(ray, targetPlayerData);

        if (targetPlayerData.watchedGauge >= watchedGagueMax)
            StartTracking(gameObject);
    }

    #region .
    private TargetPlayerData FindTargetPlayerData(GameObject gameObject)
    {
        foreach (TargetPlayerData targetPlayerData in TargetPlayerList)
        {
            if (targetPlayerData.Player == gameObject)
            {
                return targetPlayerData;
            }
        }

        return AddTargetPlayerList(gameObject);
    }
    private void AddWeight(Ray ray, TargetPlayerData targetPlayerData)
    {
        if (!IsHitObstacle(ray))
            targetPlayerData.watchedGauge += watchedValue;
    }

    private TargetPlayerData AddTargetPlayerList(GameObject gameobject)
    {
        TargetPlayerData temp = new(gameobject);
        TargetPlayerList.Add(temp);

        return temp;
    }

    private bool IsHitObstacle(Ray ray)
    {
        Debug.DrawRay(ray.origin, ray.direction * visionRange, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hit, visionRange, ObstacleMask))
        {
            return true;
        }

        return false;
    }

    private bool IsPlayerAlreadyInTargetPlayerList(GameObject gameObject, ref int listIndex)
    {
        listIndex = 0;
        foreach (TargetPlayerData targetPlayerData in TargetPlayerList)
        {
            if (targetPlayerData.Player == gameObject)
            {
                return true;
            }
            listIndex++;
        }
        return false;
    }

    #endregion

    public void LostInSight()
    {

    }

    private void StartTracking(GameObject gameObject)
    {

        animMovement.Lookat(gameObject.transform.position);
        animMovement.Scream();
        //lookat

        //scream

        //use nav

        //make del, and if destinated do del
    }

    private void LostTarget()
    {
        //zombie FSM changestate idle
    }
    /*
    IEnumerator ReturnIsTargetLost(GameObject gameobject)
    {
        while (true)
        {
            //gameOb - this pos
            Vector3 dir = gameobject.transform.position - this.transform.position;
            //계속 타겟과의 레이를 탐색
            Ray ray = new(transform.position, dir);

            if(Physics.Raycast(ray, out RaycastHit hit, zombieSight))

            //if 타겟이 다시 보였으면 lost가중치 초기화
            break;


            //if 시간이 모두 초과되었다면
            break;
        }
    }*/

    //Check
    private bool IsPlayerInsight(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, visionRange, TargetLayerMask)) { return true; }
        return false;
    }
}
