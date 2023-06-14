using UnityEngine;
public class PistolEnemy : Enemy {
    
    public PistolEnemy(GameObject prefab,Vector3 spawnPoint) : base(prefab, spawnPoint) {
        Instantiate(prefab, spawnPoint, Quaternion.identity);
    }
    void Awake() {
        value = 125;
        speed = 6.5f;
        attackSpeed = 2;
        hp = (int)(130 * GameManager.manager.enemyStrengthMultiplayer);
        damage = (int)(65 * GameManager.manager.enemyStrengthMultiplayer);
        armor = 20;
        burstBulletCount = 1;
        waitBetweenShots = false;
        bulletSpread = 0;
        attackRange = Random.Range(8.1f, 12f);
        courage = Random.Range(5, 20);
        base.Start();
    }
}