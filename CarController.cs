using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody RB;
    public GameObject CarBody;

    public Transform Wheel1;
    public Transform Wheel2;
    public Transform Wheel3;
    public Transform Wheel4;

    public Transform Right;
    public Transform Left;
    public Transform Straight;

    public TrailRenderer Trail1;
    public TrailRenderer Trail2;
    public TrailRenderer Trail3;
    public TrailRenderer Trail4;

    public AudioSource CarEngine;
    public AudioSource CarDrift;
    public bool DriftCheck;

    private void Start()
    {
        RB = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey("w"))
        {
            RB.velocity += transform.forward * 150 * Time.deltaTime;

            Wheel1.Rotate(0, 0, -500 * Time.deltaTime);
            Wheel2.Rotate(0, 0, -500 * Time.deltaTime);
            Wheel3.Rotate(0, 0, 500 * Time.deltaTime);
            Wheel4.Rotate(0, 0, 500 * Time.deltaTime);

            if(CarEngine.isPlaying == false)
            {
                CarEngine.Play();
            }
        }
        else
        {
            CarEngine.Stop();
        }

        if (Input.GetKey("s"))
        {
            RB.velocity -= transform.forward * 80 * Time.deltaTime;

            Wheel1.Rotate(0, 0, 500 * Time.deltaTime);
            Wheel2.Rotate(0, 0, 500 * Time.deltaTime);
            Wheel3.Rotate(0, 0, -500 * Time.deltaTime);
            Wheel4.Rotate(0, 0, -500 * Time.deltaTime);
        }

        if(Input.GetKey("a") && Input.GetKey("w"))
        {
            transform.Rotate(0, -30 * Time.deltaTime, 0);
            CarBody.transform.rotation = Quaternion.Lerp(CarBody.transform.rotation, Left.rotation, 4 * Time.deltaTime);
            RB.velocity += CarBody.transform.forward * 120 * Time.deltaTime;
            RB.velocity -= transform.forward * 110 * Time.deltaTime;
        }

        if(Input.GetKey("d") && Input.GetKey("w"))
        {
            transform.Rotate(0, 30 * Time.deltaTime, 0);
            CarBody.transform.rotation = Quaternion.Lerp(CarBody.transform.rotation, Right.rotation, 4 * Time.deltaTime);
            RB.velocity += CarBody.transform.forward * 120 * Time.deltaTime;
            RB.velocity -= transform.forward * 110 * Time.deltaTime;
        }

        if(!Input.GetKey("d") && !Input.GetKey("a"))
        {
            CarBody.transform.rotation = Quaternion.Lerp(CarBody.transform.rotation, Straight.rotation, 4 * Time.deltaTime);
        }

        if(CarBody.transform.localRotation.y * 100 > 15 || CarBody.transform.localRotation.y * 100 < -15)
        {
            Trail1.emitting = true;
            Trail2.emitting = true;
            Trail3.emitting = true;
            Trail4.emitting = true;

            if(DriftCheck == false)
            {
                DriftCheck = true;
                CarDrift.Play();
            }
        }
        else
        {
            DriftCheck = false;

            Trail1.emitting = false;
            Trail2.emitting = false;
            Trail3.emitting = false;
            Trail4.emitting = false;
        }
    }
}
