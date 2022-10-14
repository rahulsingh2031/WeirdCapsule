using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : LivingEntity
{
    public enum State { Idle, Chasing, Attacking };

    public ParticleSystem deathEffect;
    State currentState;
    NavMeshAgent pathFinder;
    float attackDistanceThreshold = .5f;
    Transform target;
    LivingEntity targetEntity;


    Material skinMaterial;
    Color originalColor;


    float timeBetweenAttack = 1;
    float nextAttackTime;
    float damage = 1;


    float myCollisionRadius;
    float targetCollisionRadius;

    bool hasTarget;
    protected override void Start()
    {
        base.Start();
        pathFinder = GetComponent<NavMeshAgent>();
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            hasTarget = true;
            target = GameObject.FindGameObjectWithTag("Player").transform;
            targetEntity = target.GetComponent<LivingEntity>();
            targetEntity.OnDeath += OnTargetDeath;

            currentState = State.Chasing;

            skinMaterial = GetComponent<Renderer>().material;
            originalColor = skinMaterial.color;
            myCollisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;
            StartCoroutine(UpdatePath());
        }

    }

    public override void takeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (damage >= health)
        {
            Instantiate(deathEffect, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection));
        }
        base.takeHit(damage, hitPoint, hitDirection);
    }

    void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idle;
    }

    void Update()
    {
        if (hasTarget)
        {
            if (Time.time > nextAttackTime)
            {
                float sqrDistanceToTarget = (target.position - transform.position).sqrMagnitude;
                if (sqrDistanceToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2))
                {
                    nextAttackTime = Time.time + timeBetweenAttack;
                    StartCoroutine(Attack());
                }
            }
        }
    }

    IEnumerator Attack()
    {
        currentState = State.Attacking;
        pathFinder.enabled = false;

        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius);


        float attackSpeed = 3f;
        float percent = 0;
        skinMaterial.color = Color.red;
        bool hasAppliedDamage = false;

        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;

            if (percent >= 0.5f && !hasAppliedDamage)
            {
                targetEntity.takeDamage(damage);
                hasAppliedDamage = true;
            }
            //specific parabolic equation to interpolate betwen 0 to 1 and then 1 to 0 by using x vaiable(percent)
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
            // transform.position = Vector3.Lerp(originalPosition, attackPosition, Mathf.PingPong(Time.time * 2f, 1f));

            yield return null;
        }


        skinMaterial.color = originalColor;
        currentState = State.Chasing;
        pathFinder.enabled = true;
    }

    IEnumerator UpdatePath()

    {
        while (hasTarget)
        {
            float refreshRate = 0.3f;
            if (currentState == State.Chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 newDestination = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold / 2);
                if (!isDead)
                {
                    pathFinder.SetDestination(newDestination);
                }

            }
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
