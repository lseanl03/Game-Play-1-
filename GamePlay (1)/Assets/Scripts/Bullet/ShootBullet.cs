using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public Transform firePoints;
    public GameObject bulletPrefab;
    public float nextTimeShoot = 0f;
    public float timeBetweenShoot = 1.5f;
    public Animator animator;
    public EnergyBar energyBar;
    public Bullet bullet;
    public bool outOfEnergy;
    public float energyDecreaseSpeed = 1;
    public void Start()
    {
        this.animator= GetComponent<Animator>();
    }
    private void Update()
    {
        if(Time.time > nextTimeShoot)
        {
            if (Input.GetMouseButtonDown(1) && !outOfEnergy)
            {               
                Invoke("Shoot", 0.9f);
                this.nextTimeShoot = Time.time + this.timeBetweenShoot;
                this.animator.SetTrigger("Shoot");
                energyBar.slider.value-= energyDecreaseSpeed;
                if (energyBar.slider.value == 0)
                {
                    outOfEnergy = true;
                }
            }
            if (energyBar.slider.value > 0)
            {
                outOfEnergy= false;
            }
        }
    }
    void Shoot()
    {
        Instantiate(this.bulletPrefab, this.firePoints.position, this.firePoints.rotation);
    }
}
