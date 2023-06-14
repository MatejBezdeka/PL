using UnityEngine;
public class MeleeEnemy : Enemy {
    public MeleeEnemy(GameObject prefab, Vector3 spawnPoint) : base(prefab, spawnPoint) {
        Instantiate(prefab, spawnPoint, Quaternion.identity);
    }
    void Awake() {
        value = 310;
        speed = 8.3f;
        attackSpeed = 3.8f;
        hp = (int)(195 * GameManager.manager.enemyStrengthMultiplayer);;
        damage = (int)(66 * GameManager.manager.enemyStrengthMultiplayer);;
        armor = 70;
        courage = 100;
        attackRange = 2.3f;
        base.Start();
    }
    public override void Attack() {
        if (currentCooldown <= 0) {
            player.GetComponent<PlayerController>().GetHit(damage);
            currentCooldown = attackSpeed;
        }
    }   
}