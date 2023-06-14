using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UpgradeMenu.Upgrade.Upgrades;
using Random = System.Random;
public class WeaponUpgrade : StatsUpgrade {
    Random rn = new Random();
    [SerializeField] weaponUpgrade upgrade;
    Weapon weapon;
    enum weaponUpgrade {
        weaponDamage, weaponFireRate, maxAmmo, bulletSpeed, reloadSpeed
    }

    void Start() {
        switch (upgrade) {
            case weaponUpgrade.bulletSpeed:
            case weaponUpgrade.reloadSpeed: 
            case weaponUpgrade.weaponDamage:
            case weaponUpgrade.weaponFireRate:
                percents = true;
                break;
        }
    }
    public string GetRandomWeaponInInventory(WeaponController weaponController) {
        while (true) {
            weapon = weaponController.listOfWeapons[rn.Next(0, weaponController.listOfWeapons.Count)];
            //Debug.Log(weapon.name + " try");
            if (weapon.available) {
                //Debug.Log(weapon.name);
                //picture = weapon.iconOfTheWeapon;
                return weapon.name;
            }
        }
    }
    
    public override void Use(PlayerController playerController, WeaponController weaponController) {
        if (percents) {
            value /= 100f;
        }
        switch (upgrade) {
            case weaponUpgrade.bulletSpeed:
                weapon.weaponUpgrader.AddBulletSpeed(value, weapon);
                break;
            case weaponUpgrade.maxAmmo:
                weapon.weaponUpgrader.AddMaxBullets((int)value, weapon);
                break;
            case weaponUpgrade.reloadSpeed:
                weapon.weaponUpgrader.ReloadFaster(value, weapon);
                break;
            case weaponUpgrade.weaponDamage:
                weapon.weaponUpgrader.AddDamage((int)value, weapon);
                break;
            case weaponUpgrade.weaponFireRate:
                weapon.weaponUpgrader.AddWeaponShootSpeed(value, weapon);
                break;
        }
        if (percents) {
            value *= 100;
        }
    }

    public override type GetType() {
        return type.weaponUp;
    }
}
