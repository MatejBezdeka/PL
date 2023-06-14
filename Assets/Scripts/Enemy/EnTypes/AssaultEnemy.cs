using UnityEngine;
public class AssaultEnemy : Enemy {
    public AssaultEnemy(GameObject prefab, Vector3 spawnPoint) : base(prefab, spawnPoint) {
        Instantiate(prefab, spawnPoint, Quaternion.identity);
    }
    void Awake() {
        value = 245;
        speed = 6f;
        attackSpeed = 5.5f;
        hp = (int)(135 * GameManager.manager.enemyStrengthMultiplayer);;
        damage = (int)(35 * GameManager.manager.enemyStrengthMultiplayer);;
        armor = 35;
        waitBetweenShots = true;
        burstBulletCount = 9;
        bulletSpread = 0.02f;
        attackRange = Random.Range(11f, 18.8f);
        courage = 0;
        base.Start();
    }
}