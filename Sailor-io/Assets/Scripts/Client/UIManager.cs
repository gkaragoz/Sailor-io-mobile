using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;

    #region HUD
    public Text txtHealth_HUD;
    public Text txtSupply_HUD;
    public Text txtSailor_HUD;
    public Text txtGold_HUD;
    
    //Join a crew
    //Leaderboard
    #endregion

    private void Awake() {
		if (instance == null)
			instance = this;

		DontDestroyOnLoad(instance);
	}

	public void ChangeScene(string sceneName) {
		SceneManager.LoadScene(sceneName);
	}

    #region HUD Set Methods
    public void SetHealthHUD(float currentHealth, float maxHealth) {
        txtHealth_HUD.text = currentHealth + " / " + maxHealth;
    }

    public void SetSupplyHUD(int currentAmount, int maxCapacity) {
        txtSupply_HUD.text = currentAmount + " / " + maxCapacity;
    }

    public void SetSailorHUD(int currentCrewCount, int maxCrewCount) {
        txtSailor_HUD.text = currentCrewCount + " / " + maxCrewCount;
    }

    public void SetGoldHUD(int amount) {
        txtGold_HUD.text = amount.ToString();
    }
    #endregion

}