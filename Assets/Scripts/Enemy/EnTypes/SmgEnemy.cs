using UnityEngine;
public class SmgEnemy : Enemy
{
    public SmgEnemy(GameObject prefab, Vector3 spawnPoint) : base(prefab, spawnPoint) {
        Instantiate(prefab, spawnPoint, Quaternion.identity);
    }

    void Awake() {
        value = 105;
        speed = 12.5f;
        attackSpeed = 2.5f;
        hp = 250;
        damage = 150;
        armor = 20;
        waitBetweenShots = true;
        burstBulletCount = 3;
        bulletSpread = 0.1f;
        attackRange = Random.Range(8.5f, 12f);
        courage = Random.Range(30, 50);
        base.Start();
    }

    void Update() {
        base.Update();
    }
}