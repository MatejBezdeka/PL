using UnityEngine;
public class PistolEnemy : Enemy {
    
    public PistolEnemy(GameObject prefab,Vector3 spawnPoint) : base(prefab, spawnPoint) {
        Instantiate(prefab, spawnPoint, Quaternion.identity);
    }
    void Awake() {
        value = 75;
        speed = 7.5f;
        attackSpeed = 2;
        hp = 500;
        damage = 166;
        armor = 50;
        burstBulletCount = 1;
        waitBetweenShots = false;
        bulletSpread = 0;
        attackRange = Random.Range(9.6f, 10.8f);
        courage = Random.Range(5, 20);
        base.Start();
    }
}