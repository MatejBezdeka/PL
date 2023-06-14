using UnityEngine;

public class WeaponUpgrader : MonoBehaviour {
    public void AddDamage(int amount, Weapon weapon) {
        weapon.damage += amount;
    }

    public void AddMaxBullets(int amount, Weapon weapon) {
        weapon.magSize += amount;
        
    }

    public void ReloadFaster(float amount, Weapon weapon) {
        weapon.reloadCooldown -= amount;
    }

    public void AddBulletSpeed(float amount, Weapon weapon) {
        weapon.bulletSpeed += amount;
    }

    public void AddWeaponShootSpeed(float amount, Weapon weapon) {
        weapon.cooldown -= amount;
    }
}