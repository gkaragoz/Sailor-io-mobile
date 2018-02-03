using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;

	#region LoginScreen

	public string GameServerUrl;
	private Button btnEnter;
	private InputField inputNickname;
	private InputField inputPassword;
	private string LogonUserEmail;
	private string LogonUserId;
	private string GameAccessToken;
	#endregion
	void Awake()
	{
		if (instance == null)
			instance = this;

		DontDestroyOnLoad(instance);
		GetUIReferences();
		AssignBtnDelegates();
	}

	private void AssignBtnDelegates()
	{
		btnEnter.onClick.AddListener(delegate
		{
			OnLogin();
		});
	}

	private void GetUIReferences()
	{
		btnEnter = GameObject.Find("btnEnter").GetComponent<Button>();
		inputNickname = GameObject.Find("inputNickname").GetComponent<InputField>();
		inputPassword = GameObject.Find("inputPassword").GetComponent<InputField>();
	}

	private void OnLogin()
	{
		string email = inputNickname.text;
		string password = inputPassword.text;

		if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
			return;

		#region LoginRequest
		var authResult = AuthManager.instance.doPostLogin(email, password);
		if (authResult != null)
		{
			var userField = authResult.GetField("user");
			var tokenField = authResult.GetField("token");

			LogonUserEmail = userField.GetField("email").ToString();
			LogonUserId = userField.GetField("id").ToString();
			GameAccessToken = tokenField.GetField("accessToken").ToString();

			Debug.Log("Successfully logged in ! >> " + LogonUserId);
			Debug.Log("Welcome ! >> " + LogonUserEmail);
			ChangeScene("Sandbox");
		}
		#endregion
	}

	public void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}