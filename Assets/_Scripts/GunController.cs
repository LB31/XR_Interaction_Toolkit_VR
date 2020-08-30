using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float Speed = 40;
    public GameObject Bullet;
    public Transform Barrel;
    public AudioSource AudioSource;
    public AudioClip AudioClip;

    public void Fire()
    {
        GameObject spawnedBullet = Instantiate(Bullet, Barrel.position, Barrel.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = Speed * Barrel.forward;
        Destroy(spawnedBullet, 2);
    }
}
