﻿// This code is based from the examples in three different projects that can be found here:
// https://unity3d.college/2017/07/03/using-vector3-reflect-to-cheat-ball-bouncing-physics-in-unity/
// https://github.com/FusedVR/Baseball
// https://devpost.com/software/splattervr-2a3y5z
// Many thanks to their guidance :)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceTowardPlayer_Improved : MonoBehaviour
{
	public AudioSource hit;
	public AudioSource splat;
	private float velocityMax = 200f;

    [SerializeField]
    [Range(0f, 1f)]
    [Tooltip("0 = regular bounce ignoring player | 1 = direct to the player")]
    private float bias = 0.5f;

    [SerializeField]
    [Tooltip("Just for debugging, adds some velocity during OnEnable")]
    private Vector3 initialVelocity;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private float bounceVelocity = 3.5f;

    private Vector3 lastFrameVelocity;
    private Rigidbody rb;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = initialVelocity;
    }

    private void Update()
    {
        lastFrameVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
		//ballColor = GetComponent<PainterScript>().splatColor();

		if (collision.gameObject.name == "Paddle") {
			//rb.velocity = Vector3.zero;
			hit.Play ();
			//Physics.IgnoreCollision (collision.gameObject.GetComponent<BoxCollider> (), gameObject.GetComponent<SphereCollider> ());

			float forceMultiplier = GetBatForce (collision.gameObject.GetComponent<Rigidbody> ());
			Vector3 direction = (transform.position - collision.contacts [0].point).normalized;
			rb.AddForce (direction * forceMultiplier, ForceMode.Impulse);


		} else {
			splat.Play ();
			Bounce (collision.contacts [0].normal);

			//this.gameObject.GetComponent<Renderer> ().material.color = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f));
			this.GetComponent<PainterScript>().splatColor = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f));
		}
    }

	private float GetBatForce(Rigidbody batRB) {
		return batRB.velocity.magnitude / velocityMax * 50f;
	}
    
    private void Bounce(Vector3 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var bounceDirection = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);
        var directionToPlayer = playerTransform.position - transform.position;

        var direction = Vector3.Lerp(bounceDirection, directionToPlayer, bias);

        Debug.Log("Out Direction: " + direction);
        rb.velocity = direction * bounceVelocity;
    }
}
