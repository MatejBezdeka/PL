using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UpgradeMenu.Upgrade;

public class NewWeaponUpgrade : Upgrade {
    [SerializeField] Weapon weapon;
    public override void UndoRarityValues() { }
    public override string GetDescription() { return description; }
    public override void SetRarity() { }
    public override type GetType() { return type.weapon; }
    public override Color GetColor() { return Color.green; }

    public override void Use(PlayerController playerController, WeaponController weaponController) {
        weaponController.GetNewWeapon(weapon);
    }
}
