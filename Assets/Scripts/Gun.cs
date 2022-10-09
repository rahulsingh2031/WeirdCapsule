using UnityEngine;

public class Gun : MonoBehaviour
{ //TODO: since GunController just call shoot method,so gun can either shoot projectie or raycast
    public Transform muzzle;
    public Projectile projectile;
    public float msBetweenPerShoot = 100;
    public float muzzleVelocity;//projectile velocity

    float nextShotTime;
    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + (msBetweenPerShoot / 1000);
            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation);
            newProjectile.SetSpeed(muzzleVelocity);
        }
    }
}
