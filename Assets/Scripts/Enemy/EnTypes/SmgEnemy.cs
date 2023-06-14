using UnityEngine;
public class SmgEnemy : Enemy
{
    public SmgEnemy(GameObject prefab, Vector3 spawnPoint) : base(prefab, spawnPoint) {
        Instantiate(prefab, spawnPoint, Quaternion.identity);
    }

    void Awake() {
        value = 205;
        speed = 12.5f;
        attackSpeed = 3.7f;
        hp = (int)(145 * GameManager.manager.enemyStrengthMultiplayer);;
        damage = (int)(75 * GameManager.manager.enemyStrengthMultiplayer);;
        armor = 20;
        waitBetweenShots = true;
        burstBulletCount = 3;
        bulletSpread = 0.07f;
        attackRange = Random.Range(5.8f, 12.3f);
        courage = Random.Range(30, 50);
        base.Start();
    }

    void Update() {
        base.Update();
    }
}