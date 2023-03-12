using UnityEngine;
public class AssaultEnemy : Enemy {
    public AssaultEnemy(GameObject prefab, Vector3 spawnPoint) : base(prefab, spawnPoint) {
        Instantiate(prefab, spawnPoint, Quaternion.identity);
    }
    void Awake() {
        value = 105;
        speed = 6.5f;
        attackSpeed = 5.5f;
        hp = 400;
        damage = 200;
        armor = 20;
        waitBetweenShots = true;
        burstBulletCount = 15;
        bulletSpread = 0.02f;
        attackRange = Random.Range(13f, 16.8f);
        courage = 0;
        base.Start();
    }
}