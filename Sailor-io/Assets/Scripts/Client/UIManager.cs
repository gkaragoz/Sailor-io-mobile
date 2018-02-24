using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;

    private void Awake() {
		if (instance == null)
			instance = this;

		DontDestroyOnLoad(instance);
	}

	public void ChangeScene(string sceneName) {
		SceneManager.LoadScene(sceneName);
	}
}