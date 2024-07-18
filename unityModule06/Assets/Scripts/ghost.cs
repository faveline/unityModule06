using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ghost : MonoBehaviour
{
	public float			walkRadius;
	public GameObject		player;
	public float 			timerMax;
	private NavMeshAgent	agent;
	private Vector3			destination;
	private float			timer;
	private float			timeLast;	
	private bool			follow;
	private Vector3			cpyPos;
	

    void Start()
    {
		timeLast = 0f;
		destination = randomDestination();
		agent = gameObject.GetComponent<NavMeshAgent>();
		timer = Time.time + timerMax;
    }

    void Update()
    {
		if (Time.time < timeLast)
			return;
		timeLast = Time.time + Time.deltaTime;
		if (follow && Time.time <= timer) {
			destination = player.transform.position;
			agent.destination = destination;
		} else if (Time.time > timer || Vector3.Distance(transform.position, destination) < 0.1) {
			if (follow) {
				follow = false;
				destination = cpyPos;
			} else
				destination = randomDestination();
			timer = Time.time + timerMax;
		} else {
			agent.destination = destination;
		}
    }

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == 12) {
			destination = player.transform.position;
			agent.destination = destination;
			follow = true;
			timer = Time.time + timerMax;
			cpyPos = transform.position;
		}
	}

	private Vector3	randomDestination() {
		Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
		randomDirection += transform.position;
		NavMeshHit hit;
		NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
		return(hit.position);
	}
}
