using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
	public float		speedRota;
	private float		timeLast;
	private Vector3		rota;
	private Animator	anim;
	private bool		doorBool;
	private GameObject	door;
	private bool		doorToOpen;
	private GameObject	cpy;

    void Start()
    {
		cpy = null;
		doorToOpen = true;
		timeLast = 0f;
		rota = new Vector3(0, speedRota, 0);
		anim = transform.GetComponent<Animator>();
    }

    void Update()
    {
		if (Time.time < timeLast)
			return;
		timeLast = Time.time + Time.deltaTime;
		if (Input.GetKey(KeyCode.W)) {
			anim.SetBool("move", true);
		} else {
			anim.SetBool("move", false);
		}
		if (Input.GetKey(KeyCode.A)) {
			transform.rotation = Quaternion.Euler(transform.eulerAngles - rota * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.D)) {
			transform.rotation = Quaternion.Euler(transform.eulerAngles + rota * Time.deltaTime);	
		}
		if (Input.GetKeyDown(KeyCode.F) && doorBool) {
			door.GetComponent<Animator>().SetTrigger("openDoor");
		}
    }

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == 8) {
			doorBool = true;
			door = other.transform.parent.gameObject;
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.layer == 8) {
			doorBool = false;
			door = null;
		}
	}
}
