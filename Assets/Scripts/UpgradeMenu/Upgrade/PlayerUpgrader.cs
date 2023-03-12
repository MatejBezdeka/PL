using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrader : MonoBehaviour {
    PlayerController player;
    void Start() {
        player = gameObject.GetComponent<PlayerController>();
    }
    public void Heal(float amount) {
        player.hp += (int) (player.hp * amount);
        Debug.Log("heal" + amount);
        if (player.hp > player.maxHp) {
            player.hp = player.maxHp;
        }
        player.UpdateHpUI();
    }
    public void AddMaxHp(float amount) {
        player.maxHp += (int) (player.maxHp * amount);
        player.UpdateHpUI();
    }

    public void AddXpMultiplayer(float amount) {
        player.xpMultiplayer += player.xpMultiplayer * amount;
    }

    public void AddScoreMultiplayer(float amount) {
        player.scoreMultiplayer += player.scoreMultiplayer * amount;
    }
}