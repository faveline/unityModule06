using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
	private static GameManager instance = null;
    public static GameManager Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

	private int			Pov;
	public GameObject	player;
	public GameObject	camera;
	public GameObject	cameraTPS;
	public GameObject	cameraFPS;
	public GameObject	ending;

	void Start() {
		Pov = 0;
	}

	void OnLevelWasLoaded() {
		Pov = 0;
		player = GameObject.FindGameObjectsWithTag("player")[0];
		camera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
		cameraTPS = GameObject.FindGameObjectsWithTag("cameratps")[0];
		cameraFPS = GameObject.FindGameObjectsWithTag("camerafps")[0];
		ending = GameObject.FindGameObjectsWithTag("ending")[0];
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.P)) {
			if (Pov == 0) {
				Pov = 1;
				cameraTPS.GetComponent<CinemachineVirtualCamera>().Priority = 0;
				cameraFPS.GetComponent<CinemachineVirtualCamera>().Priority = 10;
			} else {
				Pov = 0;
				player.transform.GetChild(0).gameObject.SetActive(true);
				cameraTPS.GetComponent<CinemachineVirtualCamera>().Priority = 10;
				cameraFPS.GetComponent<CinemachineVirtualCamera>().Priority = 0;
			}
		}
		if (Pov == 1 && camera.transform.position.y < 0.3) {
			Pov = 2;
			player.transform.GetChild(0).gameObject.SetActive(false);
		}
	}

	public int getPov() {
		return (Pov);
	}

	public void gameOver(int end) {
		Destroy(player.GetComponent<player>());
		if (end == 0) {
			ending.GetComponent<Animator>().SetTrigger("endGood");
			ending.transform.GetChild(1).gameObject.SetActive(true);
		} else if (end == 1) {
			ending.GetComponent<Animator>().SetTrigger("endGood");
			ending.transform.GetChild(0).gameObject.SetActive(true);
		}
	}
}
