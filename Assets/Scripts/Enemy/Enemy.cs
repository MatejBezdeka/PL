using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public abstract class Enemy : MonoBehaviour {
    protected int hp;
    public int value;
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
    public NavMeshAgent navMeshAgent;
    [SerializeField] Transform gunBarrel;
    StateController state;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] Animator anim;
    [SerializeField] protected ParticleSystem blood;
    [SerializeField] protected ParticleSystem muzzleFlash;
    [SerializeField] protected AudioClip shootingSound;
    AudioSource audioSource;
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
        currentCooldown = attackSpeed;
        gameObject.tag = "Enemy";
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = shootingSound;
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        navMeshAgent.stoppingDistance = attackRange + 0.5f;
        muzzleFlash.transform.position = gunBarrel.position;
        state = new ChaseState(this, navMeshAgent, anim, player.transform, muzzleFlash);
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
                anim.Play("Shoot");
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
        //efekty
        audioSource.Play();
        muzzleFlash.Play(true);
        
        Bullet.MakeBullet(bulletPrefab,damage/burstBulletCount, gunBarrel.transform, player.transform.position, false, null, bulletSpread, bulletSpeed, navMeshAgent.velocity);
    }
    public void Move() {
        navMeshAgent.SetDestination(player.transform.position);
    }

    public void MoveFromPlayer() {
        navMeshAgent.ResetPath();
        navMeshAgent.Move(DirectionOfPlayerToEnemy().normalized * (navMeshAgent.speed * Time.deltaTime));
    }

    public void GetHit(int damage, PlayerController shotBy, Vector3 positon) {
        damage /= (1 + (armor / 100));
        hp -= damage;
        blood.transform.position = positon;
        blood.Play();
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
        Destroy(transform.parent.gameObject);
    }

    public float DistaceBetweenEnemyAndPlayer() {
        return Vector3.Distance(gameObject.transform.position, player.transform.position);
    }

    public Vector3 DirectionOfEnemyToPlayer() {
        return player.transform.position - transform.position;
    }
    Vector3 DirectionOfPlayerToEnemy() {
        return transform.position - player.transform.position;
    }
}