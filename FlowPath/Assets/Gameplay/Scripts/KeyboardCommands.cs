using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KeyboardCommands : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("main");
        }
    }
}
