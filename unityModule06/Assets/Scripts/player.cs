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
	private bool		keyBool;
	private GameObject	key;
	private int			nbrKey;

    void Start()
    {
		nbrKey = 0;
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
			anim.SetInteger("move", 1);
		} else if (Input.GetKey(KeyCode.S)) {
			anim.SetInteger("move", -1);
		} else {
			anim.SetInteger("move", 0);
		}
		// if (GameManager.Instance.getPov() == 2) {
		// 	if (Input.mousePosition.x < Screen.width * 0.35) {
		// 		transform.rotation = Quaternion.Euler(transform.eulerAngles - rota / 2 * Time.deltaTime);
		// 	}
		// 	if (Input.mousePosition.x > Screen.width * 0.65) {
		// 		transform.rotation = Quaternion.Euler(transform.eulerAngles + rota / 2 * Time.deltaTime);
		// 	}
		// }
		if (Input.GetKey(KeyCode.A)) {
			transform.rotation = Quaternion.Euler(transform.eulerAngles - rota * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.D)) {
			transform.rotation = Quaternion.Euler(transform.eulerAngles + rota * Time.deltaTime);	
		}
		if (Input.GetKeyDown(KeyCode.F)) {
			if (doorBool) {
				if (door.gameObject.tag != "lastDoor" || nbrKey == 3)
					door.GetComponent<Animator>().SetTrigger("openDoor");
			}
			if (keyBool) {
				nbrKey++;
				keyBool = false;	
				Destroy(key.gameObject);
				key = null;
			}
		}
    }

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == 8) {
			doorBool = true;
			door = other.transform.parent.gameObject;
		}
		if (other.gameObject.layer == 9) {
			keyBool = true;
			key = other.gameObject;
		}
		if (other.gameObject.layer == 10) {
			GameManager.Instance.gameOver(1);
		}
		if (other.gameObject.layer == 13) {
			GameManager.Instance.gameOver(0);
		}		
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.layer == 8) {
			doorBool = false;
			door = null;
		}
		if (other.gameObject.layer == 9) {
			keyBool = false;
			key = null;
		}
	}
}
