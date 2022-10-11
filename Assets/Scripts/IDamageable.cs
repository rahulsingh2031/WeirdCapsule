using UnityEngine;


public interface IDamageable
{
    void takeHit(float damage, RaycastHit hit);
}