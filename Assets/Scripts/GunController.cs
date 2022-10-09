using UnityEngine;

public class GunController : MonoBehaviour
{
    // just a position where player will hold the weapon from
    public Transform weaponHold;
    public Gun StartingGun;
    Gun equippedGun;

    private void Start()
    {
        if (StartingGun != null)
        {
            equipGun(StartingGun);
        }
    }
    public void equipGun(Gun gunToEquip)
    {
        if (equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
        }

        equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation, weaponHold.transform);
    }

    public void Shoot()
    {
        if (equippedGun != null)
        {
            equippedGun.Shoot();
        }
    }
}
