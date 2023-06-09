using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UpgradeMenu.Upgrade;

public class NewWeaponUpgrade : Upgrade {
    [SerializeField] Weapon weapon;

    void Start() {
        picture = weapon.iconOfTheWeapon;
    }

    public override void UndoRarityValues() { }
    public override string GetDescription() { return description; }
    public override void SetRarity() { }
    public override type GetType() { return type.weapon; }
    public override Color GetColor() { return new Color32(44, 226, 231, 255); }

    public override void Use(PlayerController playerController, WeaponController weaponController) {
        weaponController.GetNewWeapon(weapon);
    }
}
