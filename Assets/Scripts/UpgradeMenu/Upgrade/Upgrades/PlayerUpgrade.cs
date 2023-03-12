using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpgradeMenu.Upgrade.Upgrades;

public class PlayerUpgrade : StatsUpgrade {
    [SerializeField] playerUpgrade upgrade;
    void Start() {
        switch (upgrade) {
            case playerUpgrade.heal:
            case playerUpgrade.maxhealth:
            case playerUpgrade.scoreMultiplayer:
            case playerUpgrade.xpMultiplayer:
                percents = true;
                break;
        }
    }
    enum playerUpgrade {
        maxhealth, armor, speed, dashSpeed, jumpHeight, dashCount, scoreMultiplayer, xpMultiplayer, heal, healingPerSecond
    }
    public override void Use(PlayerController playerController, WeaponController weaponController) {
        if (percents) {
            value /= 100;
        }
        switch (upgrade) {
            case playerUpgrade.heal:
                playerController.playerUpgrader.Heal(value);
                break;
            case playerUpgrade.maxhealth:
                playerController.playerUpgrader.AddMaxHp(value);
                break;
            case playerUpgrade.scoreMultiplayer:
                playerController.playerUpgrader.AddScoreMultiplayer(value);
                break;
            case playerUpgrade.xpMultiplayer:
                playerController.playerUpgrader.AddXpMultiplayer(value);
                break;
        }
        if (percents) {
            value *= 100;
        }
    }
}