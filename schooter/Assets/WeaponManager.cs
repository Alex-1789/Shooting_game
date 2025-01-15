using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon[] weapons; // Array of weapons
    private int currentWeaponIndex = 0;

    public GameObject grenadePrefab; // Grenade prefab
    public Transform grenadeSpawnPoint; // Spawn point for grenades

    void Start()
    {
        // Ensure only the current weapon is active
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == currentWeaponIndex);
        }
    }

    public void FireCurrentWeapon()
    {
        // Fire the currently selected weapon
        weapons[currentWeaponIndex].Shoot();
    }

    public void SwitchToWeapon1()
    {
        SwitchWeapon(0); // Switch to Weapon 1
    }

    public void SwitchToWeapon2()
    {
        SwitchWeapon(1); // Switch to Weapon 2
    }

    public void ThrowGrenade()
    {
        // Instantiate a grenade at the spawn point
        Instantiate(grenadePrefab, grenadeSpawnPoint.position, grenadeSpawnPoint.rotation);
    }

    private void SwitchWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length) return;

        // Deactivate the current weapon
        weapons[currentWeaponIndex].gameObject.SetActive(false);

        // Switch to the new weapon
        currentWeaponIndex = index;
        weapons[currentWeaponIndex].gameObject.SetActive(true);

    }
}
