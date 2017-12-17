using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Called when the ball collides with objects. Should print decals and set new vector
    private void OnCollisionEnter(Collision collision)
    {
        // to print decals on contact

        if (collision.rigidbody) {
            collision.rigidbody.AddForce(Vector3.up * 15);
        }
    }

}
