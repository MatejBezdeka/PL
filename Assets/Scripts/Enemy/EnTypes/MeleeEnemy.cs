using UnityEngine;
public class MeleeEnemy : Enemy {
    public MeleeEnemy(GameObject prefab, Vector3 spawnPoint) : base(prefab, spawnPoint) {
        Instantiate(prefab, spawnPoint, Quaternion.identity);
    }
    void Awake() {
        value = 105;
        speed = 8.5f;
        attackSpeed = 3.8f;
        hp = 600;
        damage = 100;
        armor = 200;
        courage = 100;
        attackRange = 2.2f;
        base.Start();
    }
    public override void Attack() {
        if (currentCooldown <= 0) {
            player.GetComponent<PlayerController>().GetHit(damage);
            currentCooldown = attackSpeed;
        }
    }   
}