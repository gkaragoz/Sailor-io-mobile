using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Network.ApiModels;
using UnityEngine;

public class AuthManager : MonoBehaviour
{

	public static AuthManager instance;

	private string gameServerLoginURI = "/auth/login";
	private string gameServerRegisterURI = "/auth/register";
	private int wwwTimeout = 30;

	void Awake()
	{

		if (instance == null)
			instance = this;

		DontDestroyOnLoad(instance);
		
	}


	public JSONObject doPostLogin(string username, string password)
	{
		JSONObject json = new JSONObject();
		json.AddField("email", username);
		json.AddField("password", password);
	

		Dictionary<string, string> headers = new Dictionary<string, string>();
		headers.Add("Content-Type", "application/json");

		// convert json string to byte
		var formData = System.Text.Encoding.UTF8.GetBytes(json.ToString());

		WWW www = new WWW(UIManager.instance.GameServerUrl + gameServerLoginURI, formData, headers);
		StartCoroutine(CheckTimeout(www));
		
		string jsonData = "";
		
		if (string.IsNullOrEmpty(www.error))
		{
			jsonData = System.Text.Encoding.UTF8.GetString(www.bytes, 0,
				www.bytes.Length);
			return new JSONObject(jsonData);

		}
		else
		{
			return null;
		}
	}
	public void doPostRegister()
	{

	}
	private IEnumerator CheckTimeout(WWW www)
	{
		float startTime = Time.time;

		while (!www.isDone)
		{
			if (wwwTimeout > 0 && (Time.time - startTime) >= wwwTimeout)
			{
				//TODO: Show error to front
				yield break;
			}

		}
		yield return www;
	}
	
}
