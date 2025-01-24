using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon[] weapons; 
    private int currentWeaponIndex = 0;

    public GameObject grenadePrefab; 
    public Transform grenadeSpawnPoint;

    void Start()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == currentWeaponIndex);
        }
    }

    public void FireCurrentWeapon()
    {
        weapons[currentWeaponIndex].Shoot();
    }

    public void SwitchToWeapon1()
    {
        SwitchWeapon(0);
    }

    public void SwitchToWeapon2()
    {
        SwitchWeapon(1);
    }

    public void ThrowGrenade()
    {
        Instantiate(grenadePrefab, grenadeSpawnPoint.position, grenadeSpawnPoint.rotation);
    }

    private void SwitchWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length) return;

        weapons[currentWeaponIndex].gameObject.SetActive(false);

        currentWeaponIndex = index;
        weapons[currentWeaponIndex].gameObject.SetActive(true);

    }
}
