using UnityEngine;
using Random = UnityEngine.Random;
public class Bullet : MonoBehaviour {
    const int BulletTimeMax = 5;
    float bulletTimeAlive = 0;
    int damage;
    bool friendly;
    PlayerController shotBy;

    public static void MakeBullet(GameObject bulletPrefab,int damage, Transform gunBarrel, Vector3 destination, bool friendly, PlayerController shotBy, float spread, float bulletSpeed, Vector3 directionOfEntity) {
        GameObject bullet = Instantiate(bulletPrefab,/*předpovídá pozici zbraně ->*/ gunBarrel.position + (directionOfEntity.normalized/15), gunBarrel.rotation);
        //GameObject bullet = Instantiate(bulletPrefab,/*předpovídá pozici zbraně ->*/ new Vector3(gunBarrel.position.x +(directionOfEntity.normalized.x/100),gunBarrel.position.y, gunBarrel.position.z + directionOfEntity.z), gunBarrel.rotation);
        //vlastnosti
        Bullet bulletComp =  bullet.AddComponent<Bullet>();
        bulletComp.damage = damage;
        bulletComp.friendly = friendly;
        bulletComp.shotBy = shotBy;
        bullet.transform.LookAt(destination);
        //rozptyl
        var rigid = bullet.GetComponent<Rigidbody>();
        Vector3 direction = bullet.transform.forward;
        direction.x += Random.Range(-spread, spread);
        direction.y += Random.Range(-spread, spread);
        rigid.AddForce(direction * bulletSpeed/4, ForceMode.Impulse);
    }
    void Update() {
        bulletTimeAlive += Time.deltaTime;
        if (bulletTimeAlive >= BulletTimeMax) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collisioned) {
        switch (collisioned.gameObject.tag) {
            case "Bullet":
                return;
            case "Player":
                if (!friendly)
                    collisioned.gameObject.GetComponent<PlayerController>().GetHit(damage);
                break;
            case "Enemy":
                if (friendly)
                    collisioned.gameObject.GetComponent<Enemy>().GetHit(damage, shotBy, transform.position);
                //else if(dif == hard)
                //return
                break; 
            
        } 
        Destroy(gameObject);
    }
}