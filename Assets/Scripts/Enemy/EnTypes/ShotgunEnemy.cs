using UnityEngine;
public class ShotgunEnemy : Enemy
{
    public ShotgunEnemy(GameObject prefab, Vector3 spawnPoint) : base(prefab, spawnPoint) {
        Instantiate(prefab, spawnPoint, Quaternion.identity);
    }

    void Awake() {
        value = 135;
        speed = 10.5f;
        attackSpeed = 4;
        hp = 600;
        damage = 300;
        armor = 100;
        burstBulletCount = 7;
        waitBetweenShots = false;
        bulletSpread = 0.5f;
        attackRange = Random.Range(6, 8.5f);
        courage = Random.Range(40, 70);
        base.Start();
    }

    void Update() {
        base.Update();
    }
}