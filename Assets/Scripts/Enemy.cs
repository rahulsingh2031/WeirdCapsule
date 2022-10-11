using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : LivingEntity
{
    NavMeshAgent pathFinder;
    float attackDistanceThreshold;
    Transform target;
    float timeBetweenAttack = 1;
    float nextAttackTime;
    protected override void Start()
    {
        base.Start();
        pathFinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(UpdatePath());

    }

    void Update()
    {
        if (Time.time > nextAttackTime)
        {
            float sqrDistanceToTarget = (target.position - transform.position).sqrMagnitude;
            if (sqrDistanceToTarget < Mathf.Pow(attackDistanceThreshold, 2))
            {

            }
            nextAttackTime = Time.time + nextAttackTime;
        }

    }

    IEnumerator UpdatePath()

    {
        while (target != null)
        {

            float refreshRate = 0.3f;
            Vector3 newDestination = new Vector3(target.position.x, 0f, target.position.z);
            if (!isDead)
            {
                pathFinder.SetDestination(newDestination);
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
