using UnityEngine;
using System.Collections;

public class ShockwaveWeapon : ChargeWeapon {

    public ParticleSystem smoke;

    public float maxChargeTime;
    private float chargeTime = 1.0f;

    public float cooldownTime;
    private float cdTimer;

    public float maxForce;

    public AudioSource chargeSound;
    public AudioSource boomSound;


    private bool isCharging = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            startCharge();
        }

        if(isCharging)
        {
            if(chargeTime < maxChargeTime)
            {
                chargeTime += Time.deltaTime;
            }
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            endCharge();
        }
        if(cdTimer <= cooldownTime)
        {
            cdTimer += Time.deltaTime;
        }
	
	}

    public override void startCharge()
    {
        if (cdTimer > cooldownTime)
        {
            isCharging = true;
            chargeSound.Play();
        }
    }

    public override void endCharge()
    {
        chargeSound.Stop();
        fire();
        chargeTime = 1;
        isCharging = false;
        boomSound.Play();
        smoke.Play();
        cdTimer = 0;
    }

    public override void fire()
    {
        if(chargeTime < float.Epsilon)
        {
            return;
        }
        Collider[] collided = Physics.OverlapSphere(transform.position, 5);
        foreach (Collider hit in collided)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if(rb != null && rb.transform != this.transform)
            {
                rb.AddExplosionForce(maxForce * (chargeTime/maxChargeTime), transform.position, 5);
            }
        }
    }
}
