using UnityEngine;
using TMPro;
using System.Collections;

public class Shooting : MonoBehaviour
{
    //bullet 
    public GameObject bulletRed;
    public GameObject bulletBlue;

    //bullet force
    public float shootForce, upwardForce;

    //Gun stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    int bulletsLeft, bulletsShot;

    //bools
    bool shootingRed, readyToShoot, reloading, shootingBlue;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;



    //bug fixing :D
    public bool allowInvoke = true;

    private void Awake()
    {
        //make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();


    }
    private void MyInput()
    {
        //Check if allowed to hold down button and take corresponding input
        if (allowButtonHold) shootingRed = Input.GetKey(KeyCode.Mouse0);
        else shootingRed = Input.GetKeyDown(KeyCode.Mouse0);

        if(allowButtonHold) shootingBlue = Input.GetKey(KeyCode.Mouse1);
        else shootingBlue= Input.GetKeyDown(KeyCode.Mouse1);

        //Reloading 
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        //Reload automatically when trying to shoot without ammo
        if (readyToShoot && (shootingRed || shootingBlue) && !reloading && bulletsLeft <= 0) Reload();

        //Shooting
        if (readyToShoot && (shootingRed || shootingBlue) && !reloading && bulletsLeft > 0)
        {
            //Set bullets shot to 0
            bulletsShot = 0;

            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        //Find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view
        RaycastHit hit;

        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); //Just a point far away from the player

        //Calculate direction from attackPoint to targetPoint
        Vector3 Direction = targetPoint - attackPoint.position;

        //Instantiate bullet/projectile
        GameObject currentBullet;
        if (shootingRed)
        {
            currentBullet = Instantiate(bulletRed, attackPoint.position, Quaternion.Euler(0f,0f,90f)); //store instantiated bullet in currentBullet
            StartCoroutine(Yeet(currentBullet));
        }
        else
        {
            currentBullet = Instantiate(bulletBlue, attackPoint.position, Quaternion.Euler(0f,0f,90f));
            StartCoroutine(Yeet(currentBullet));
        }
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = Direction.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(Direction.normalized * shootForce, ForceMode.Impulse);

        bulletsLeft--;
        bulletsShot++;

        //Invoke resetShot function (if not already invoked), with your timeBetweenShooting
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;

        }
    }
    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime); //Invoke ReloadFinished function with your reloadTime as delay
    }
    private void ReloadFinished()
    {
        //Fill magazine
        bulletsLeft = magazineSize;
        reloading = false;
    }

    IEnumerator Yeet(GameObject BULLET)    
    {
        yield return new WaitForSeconds(3);
        Destroy(BULLET);
    }


}