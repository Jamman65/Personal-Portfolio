using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
    private HealthManager healthManager;
    public Texture hudHealthBar;
    public Texture hudHealthBarBackground;
    // Exercise 12.7
    private EnergyManager energyManager;
    private WeaponController weaponController;

    public Texture hudBackground;
    public Texture hudZoomBackground;
    public Texture hudRectile;
    public Texture hudRectileRed;

    public Texture hudAmmoBackground;
    public Texture hudAmmo;
    private int magazines;
    private int bulletsInMagazine;

    public Texture hudBloodSpat;




    void Start() {
        healthManager = GetComponent<HealthManager>();
        energyManager = GetComponent<EnergyManager>();
        // Exercise 2.6
        weaponController = GetComponent<WeaponController>();
    }

    void OnGUI() {
        GUI.DrawTexture(new Rect(0, Screen.height - 256, 256, 256), hudBackground, ScaleMode.ScaleToFit, true, 0.0f);
        int screenMiddleX = Screen.width / 2;
        int screenMiddleY = Screen.height / 2;
        if (weaponController.useScope) {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), hudZoomBackground, ScaleMode.StretchToFill, true, 0.0f);
        }
        else if (weaponController.animator.GetBool("Aim")) {
            GUI.DrawTexture(new Rect(screenMiddleX - 25, screenMiddleY - 25, 50, 50), hudRectile, ScaleMode.ScaleToFit, true, 0.0f);
        }


        int health = healthManager.Health;
        float healthRatio = health / (float)healthManager.MaxHealth;
        GUI.DrawTexture(new Rect(85, Screen.height - 60, 150, 15), hudHealthBarBackground, ScaleMode.StretchToFill, true, 0.0f);
        GUI.DrawTexture(new Rect(85, Screen.height - 60, Mathf.RoundToInt(healthRatio * 150), 15), hudHealthBar, ScaleMode.StretchToFill, true, 0.0f);
        GUI.Label(new Rect(85, Screen.height - 63, 100, 40), new GUIContent("Health : " + health.ToString()));


        int energy = energyManager.Energy;
        float energyRatio = energy / (float)energyManager.MaxEnergy;
        GUI.DrawTexture(new Rect(85, Screen.height - 100, 150, 15), hudHealthBarBackground, ScaleMode.StretchToFill, true, 0.0f);
        GUI.DrawTexture(new Rect(85, Screen.height - 100, Mathf.RoundToInt(energyRatio * 150), 15), hudHealthBar, ScaleMode.StretchToFill, true, 0.0f);
        GUI.Label(new Rect(85, Screen.height - 103, 100, 40), new GUIContent("Energy : " + energy.ToString()));

        if ((weaponController.MountedWeapon != null)) {
            GUI.DrawTexture(new Rect(55, Screen.height - 180, 220, 40), weaponController.MountedWeapon.hudWeaponImage, ScaleMode.ScaleToFit, true, 0.0f);
            GUI.DrawTexture(new Rect(55, Screen.height - 140, 220, 40), hudAmmoBackground, ScaleMode.ScaleToFit, true, 0.0f);
            bulletsInMagazine = weaponController.MountedWeapon.GetRoundCount();
            // Calculate the number of magazines - include the loaded magazine
            magazines = weaponController.MountedWeapon.GetMagazineCount();
            // The number of rounds in the loaded magazine
            int bulletsInLoadedMagazine = weaponController.MountedWeapon.loadedMagazineRoundCount;
            if ((bulletsInLoadedMagazine == 0) && (magazines == 1)) {
                bulletsInLoadedMagazine = bulletsInMagazine;
            }
            int x = 135;
            for (int n = 0; n < magazines; n++) {
                GUI.DrawTexture(new Rect(x, Screen.height - 135, 30, 30), hudAmmo, ScaleMode.ScaleToFit, true, 0.0f);
                x += 20;
            }
            GUI.Label(new Rect(85, Screen.height - 130, 30, 40), new GUIContent(bulletsInMagazine.ToString()));
        }
        // Only invoke if damaged
        if (health < 90f) {
            float alpha = 1 - (health / 100f);
            Color originalColor = GUI.color;
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), hudBloodSpat, ScaleMode.StretchToFill, true, 0.0f);
            GUI.color = originalColor;
        }

    }

}
