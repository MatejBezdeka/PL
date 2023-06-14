using UnityEngine;
public class ShotgunEnemy : Enemy
{
    public ShotgunEnemy(GameObject prefab, Vector3 spawnPoint) : base(prefab, spawnPoint) {
        Instantiate(prefab, spawnPoint, Quaternion.identity);
    }

    void Awake() {
        value = 265;
        speed = 10.5f;
        attackSpeed = 4.8f;
        hp = (int)(170 * GameManager.manager.enemyStrengthMultiplayer);
        damage = (int)(40 * GameManager.manager.enemyStrengthMultiplayer);
        armor = 75;
        burstBulletCount = 7;
        waitBetweenShots = false;
        bulletSpread = 0.11f;
        attackRange = Random.Range(5, 8);
        courage = Random.Range(40, 70);
        base.Start();
    }

    void Update() {
        base.Update();
    }
}