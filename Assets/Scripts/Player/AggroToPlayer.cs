using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroToPlayer : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayerMask;
    List<GameObject> EnemyList= new List<GameObject>();
    WaitForSeconds waitTime = new WaitForSeconds(1);

    private void Start()
    {
        StartCoroutine(AggroEnemy());
    }

    IEnumerator AggroEnemy()
    {
        if (EnemyList.Count <= 0) yield return waitTime;

        while (true)
        {
            foreach (GameObject enemy in EnemyList)
            {
                TryGetComponent(out SearchingFSM_LookAround script);
                Ray ray = new(transform.position, enemy.transform.position - transform.position);

                script.GetRay(ray, this.gameObject);
            }
            yield return waitTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == enemyLayerMask)
        {
            EnemyList.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //범위 밖으로 나간 적에게 em.Reset실행
        if (collision.gameObject.layer == enemyLayerMask)
        {
            EnemyList.Remove(collision.gameObject);
            TryGetComponent(out SearchingFSM_LookAround script);
            script.LostInSight();
        }
    }

    /* codes */
}
