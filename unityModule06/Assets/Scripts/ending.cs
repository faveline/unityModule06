using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ending : MonoBehaviour
{
	public void endGame() {
		SceneManager.LoadScene(0);
	}
}
