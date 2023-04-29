using System;
using UnityEngine;
public class Weapon : MonoBehaviour {
    /*
    * todo:
    * zaměřování:pls
     */
    public string name { get; protected set; }
    public WeaponUpgrader weaponUpgrader;
    internal int damage;
    internal float cooldown;
    internal float currentCooldown;
    internal float bulletSpeed;
    public bool available;
    //public float switchCooldwon { get; protected set; }
    
    public float reloadCooldown { get; internal set; }
    protected float weaponSpread;
    public int magSize { get; internal set; }
    public int bulletsInMag { get; protected set; }
    /*protected enum mode {
        single, burst, auto, shotgun
    }*/
    public bool reloading { get; private set; }
    [SerializeField] protected Transform laserPos2;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform gunBarrel;
    [SerializeField] protected PlayerController playerController;
    [SerializeField] protected ParticleSystem muzzleFlash;
    [SerializeField] protected AudioClip shootSound;
    MeshRenderer meshRenderer;
    protected virtual void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        bulletsInMag = magSize;
        bulletSpeed = 15;
        reloading = false;
    }

    void Start() {
        weaponUpgrader = gameObject.AddComponent<WeaponUpgrader>();
    }

    public virtual void Attack() {
        if (currentCooldown > 0) return;
        shootBullet();
        bulletsInMag--;
        currentCooldown = cooldown;
    }
    protected void shootBullet() {
        GameManager.manager.PlayAudioCLip(shootSound);
        muzzleFlash.Play(true);
        Bullet.MakeBullet(bulletPrefab, damage, gunBarrel, laserPos2.position, true, playerController, weaponSpread, bulletSpeed, playerController.move);
    }
    public void Reload() {
        bulletsInMag = magSize;
    }

    public void update() {
        if (currentCooldown > 0) {
            currentCooldown -= Time.deltaTime;
        }
    }

    public void disableAndAbleMesh(Weapon nextWeapon) {
        meshRenderer.enabled = false;
        nextWeapon.meshRenderer.enabled = true;
        muzzleFlash.transform.position = nextWeapon.gunBarrel.position;

    }
}