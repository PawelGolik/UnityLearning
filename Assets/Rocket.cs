using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class Rocket : MonoBehaviour {

    [SerializeField]
    float RCSSpeed = 100f;
    [SerializeField]
    float mainThrust = 100f;
    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private GameObject Camera;
    private Vector3 startingPosition;
    private Quaternion startingRotation;
    // Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        Camera = GameObject.Find("Main Camera");
        audioSource = GetComponent<AudioSource>();
        startingPosition = transform.position;
        startingRotation = transform.rotation;
        audioSource.playOnAwake = false;
    }
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotation();
        HoldCameraOnRocket();

    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Noting happent");
                break;
            case "Fuel":
                print("Fuel");
                break;
            case "Finish":
                print("Finish");
                break;
            default:
                RestartLevel();
                break;
        }
    }
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
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
    }
    private void Rotation()
    {
        rigidBody.freezeRotation = true;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(-Vector3.forward * Time.deltaTime * RCSSpeed, Space.Self);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * RCSSpeed, Space.Self);
        }
        rigidBody.freezeRotation = false;
    }
    private void HoldCameraOnRocket()
    {
        Camera.transform.position = new Vector3(rigidBody.transform.position.x, rigidBody.transform.position.y + 5, rigidBody.transform.position.z  + 25);

    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}