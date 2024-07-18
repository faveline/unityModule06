using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorNavMesh : MonoBehaviour
{
	public void openNav() {
		gameObject.GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = false;
	}

	public void closeNav() {
		gameObject.GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = true;
	}
}
