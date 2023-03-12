using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

public abstract class Enemy : MonoBehaviour {
    public static List<Enemy> listOfEnemies = new List<Enemy>();
    /*
     * TODO speed back
     * TODO standing/firing range
     * hp
     * damage
     * spawmning
     * vypocitani armoru
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
    protected float bulletSpeed;
    public float attackRange { get; protected set; }
    public float bulletSpread { get; protected set; }
    public int courage { get; protected set; }
    protected NavMeshAgent navMeshAgent;
    protected Transform gunBarrel;
    public Transform arm;
    protected StateController state;
    [SerializeField] 
    protected GameObject enemyPrefab;
    [SerializeField] 
    protected GameObject bulletPrefab;
    //--------------
    //private Dictionary<int, Tuple<GameObject, GameObject>> a = new Dictionary<int, Tuple<GameObject, GameObject>>();
    //--------------
    protected bool friendly = false;
    protected GameObject player;
    // private List<Transform> spawnPoints = new List<Transform>();
    /*
     * vybuchující enemy
     * sniper s laserem (pamalu se otáčí k hráči) - rýšža
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

    public virtual void attack() {
        //můžu útočit?
        if (currentCooldown <= 0) {
            for (int i = 0; i < burstBulletCount; i++) {
                //prodleva mezi výstřelama
                if (waitBetweenShots) {
                    Invoke(nameof(shootBullet), 0.1f * i); 
                }
                else {
                    shootBullet();
                }
                //bulletCooldown = timeBetweenBullets;
            }
            currentCooldown = attackSpeed;
        }
    }

    void shootBullet() {
        Bullet.MakeBullet(bulletPrefab,damage/burstBulletCount, gunBarrel.transform, player.transform.position, friendly, null, bulletSpread, bulletSpeed, navMeshAgent.velocity);
    }
    public void move() {
        navMeshAgent.SetDestination(player.transform.position);
    }

    public void moveFromPlayer() {
        navMeshAgent.ResetPath();
        navMeshAgent.Move(getDirectionOfPlayerToEnemy().normalized * (navMeshAgent.speed * Time.deltaTime));
    }

    public void getHit(int damage, PlayerController shotBy) {
        damage = damage / (1 + (armor / 100));
        hp -= damage;
        Debug.Log("dm " + damage);
        Debug.Log("jsem zraněn" + hp);
        if (hp <= 0) {
            state = new DeadState();
            die(shotBy);
        }
    }

    void die(PlayerController shotBy) {
        shotBy.changeScore(value);
        Game.manager.EnemyDied(this);
        Destroy(gameObject);
    }

    public float distaceBetweenEnemyAndPlayer() {
        return Vector3.Distance(gameObject.transform.position, player.transform.position);
    }

    public Vector3 getDirectionOfEnemyToPlayer() {
        return player.transform.position - transform.position;
    }
    public Vector3 getDirectionOfPlayerToEnemy() {
        return  transform.position - player.transform.position;
    }
    /*void chaseState() {
        Debug.Log("chase");
        move(true);
    }
    
    void attackState() {
        Debug.Log("attack");
        gameObject.transform.LookAt(playerPos);
        attack();
    }

    void retreatState() {
        Debug.Log("retreat");
        if (Random.Range(0,100) > courage) {
            move(false);            
        }
        else {
            attack();
        }
    }
    
    void evade() {
        
    }
    */
    /*protected virtual void createEnemy(GameObject enemyPrefab, Vector3 spawnPoint, Enemy e) {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
        Enemy enemyComp = enemy.AddComponent<Enemy>();
        enemyComp.gunBarrel = GameObject.Find("GunBarrel");
        enemyComp.bulletPrefab = bulletPrefab;
        
        public virtual void attack( float timeBetweenBullets) {
        //float bulletCooldown = 0;
        if (currentCooldown <= 0) {
            for (int i = 0; i < burstBulletCount; i++) {
                if (waitBetweenShots) {
                    StartCoroutine(wait());
                }
                Bullet.makeBullet(bulletPrefab,damage/burstBulletCount, gunBarrel.transform, player.transform.position, friendly, gameObject, bulletSpread);
                //bulletCooldown = timeBetweenBullets;
            }
            currentCooldown = attackSpeed;
        }
    }
    }*/
    /*void Start()
    {
        // - After 0 seconds, prints "Starting 0.0"
        // - After 0 seconds, prints "Before WaitAndPrint Finishes 0.0"
        // - After 2 seconds, prints "WaitAndPrint 2.0"
        print("Starting " + Time.time);

        // Start function WaitAndPrint as a coroutine.

        coroutine = WaitAndPrint(2.0f);
        StartCoroutine(coroutine);

        print("Before WaitAndPrint Finishes " + Time.time);
    }

    // every 2 seconds perform the print()
    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            print("WaitAndPrint " + Time.time);
        }
    }*/
}
