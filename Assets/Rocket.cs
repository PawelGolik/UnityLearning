using UnityEngine;
using System.Collections;
using System;

public class Rocket : MonoBehaviour {

    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private GameObject Camera;
    // Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        Camera = GameObject.Find("Main Camera");
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
        
	}
    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(-Vector3.forward * Time.deltaTime * 100, Space.Self);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * 100, Space.Self);
        }
        Camera.transform.position = new Vector3(rigidBody.transform.position.x, rigidBody.transform.position.y , rigidBody.transform.position.z -9);
    }

}
