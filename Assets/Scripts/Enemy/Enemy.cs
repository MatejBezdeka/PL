using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public abstract class Enemy : MonoBehaviour {
    public static List<Enemy> listOfEnemies = new List<Enemy>();
    /*
     * vypocitani armoru?
     */
    protected int enemyID;
    protected int hp;
    [SerializeField] public int value { get; protected set; }
    protected int damage;
    public float speed { get; protected set; }
    protected int armor;
    protected float attackSpeed;
    protected float currentCooldown;
    protected int burstBulletCount;
    protected bool waitBetweenShots;
    float bulletSpeed;
    public float attackRange { get; protected set; }
    protected float bulletSpread;
    public int courage { get; protected set; }
    NavMeshAgent navMeshAgent;
    Transform gunBarrel;
    public Transform arm;
    StateController state;
    [SerializeField] 
    protected GameObject enemyPrefab;
    [SerializeField] 
    protected GameObject bulletPrefab;
    //--------------
    //private Dictionary<int, Tuple<GameObject, GameObject>> a = new Dictionary<int, Tuple<GameObject, GameObject>>();
    //--------------
    protected GameObject player;
    // private List<Transform> spawnPoints = new List<Transform>();
    /*
     * vybuchující enemy
     * sniper s laserem (pamalu se otáčí k hráči) - Rýšža
     * hp základ 1000
     * dmg 
     */
    protected Enemy(GameObject prefab,Vector3 spawnPoint) {
        Instantiate(prefab, spawnPoint, Quaternion.identity);
    }
    protected virtual void Start() {
        bulletSpeed = 3;
        player = GameObject.FindWithTag("Player");
        arm = gameObject.transform.GetChild(0);
        try {
            gunBarrel = arm.GetChild(0).GetChild(2);
        }
        catch (Exception e) {
            // ignored
        }
        currentCooldown = attackSpeed;
        gameObject.tag = "Enemy";
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        navMeshAgent.stoppingDistance = attackRange + 0.5f;
        state = new ChaseState(this, navMeshAgent, new Animator(), player.transform);
    }

    protected virtual void Update() {
        if (currentCooldown > 0) {
            currentCooldown -= Time.deltaTime;
        }
        state = state.Process();
    }

    public virtual void Attack() {
        if (currentCooldown <= 0) {
            for (int i = 0; i < burstBulletCount; i++) {
                //prodleva mezi výstřelama
                if (waitBetweenShots) {
                    Invoke(nameof(ShootBullet), 0.1f * i); 
                }
                else {
                    ShootBullet();
                }
            }
            currentCooldown = attackSpeed;
        }
    }

    void ShootBullet() {
        Bullet.MakeBullet(bulletPrefab,damage/burstBulletCount, gunBarrel.transform, player.transform.position, false, null, bulletSpread, bulletSpeed, navMeshAgent.velocity);
    }
    public void Move() {
        navMeshAgent.SetDestination(player.transform.position);
    }

    public void MoveFromPlayer() {
        navMeshAgent.ResetPath();
        navMeshAgent.Move(DirectionOfPlayerToEnemy().normalized * (navMeshAgent.speed * Time.deltaTime));
    }

    public void GetHit(int damage, PlayerController shotBy) {
        damage = damage / (1 + (armor / 100));
        hp -= damage;
        //Debug.Log("dm " + damage);
        //Debug.Log("jsem zraněn" + hp);
        if (hp <= 0) {
            state = new DeadState();
            Die(shotBy);
        }
    }

    void Die(PlayerController shotBy) {
        shotBy.ChangeScore(value);
        GameManager.manager.EnemyDied(this);
        Destroy(gameObject);
    }

    public float DistaceBetweenEnemyAndPlayer() {
        return Vector3.Distance(gameObject.transform.position, player.transform.position);
    }

    public Vector3 DirectionOfEnemyToPlayer() {
        return player.transform.position - transform.position;
    }
    public Vector3 DirectionOfPlayerToEnemy() {
        return  transform.position - player.transform.position;
    }
}