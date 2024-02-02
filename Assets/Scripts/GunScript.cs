using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GunScript : MonoBehaviour {

    public bool isEquipped;
    private int clipSize = 30;
    private int currBullets;
    private int rsrvBullets;
    private int maxAmmo;


    private InteractScript interactScript;

    private void Start() {

        isEquipped = false;

        currBullets = clipSize;
        maxAmmo = clipSize * 5;
        rsrvBullets = clipSize * 2;

        interactScript = this.AddComponent<InteractScript>();
    }

    public void Shoot() {
        if (currBullets > 0) {
            Debug.Log("Shot bullet");
            currBullets--;
        } else if (rsrvBullets > 0) {
            Reload();
        }
    }

    private void Reload() {

        // check if player needs to reload
        if (currBullets < clipSize) {

            // check if player has any bullets in reserve
            if (rsrvBullets > 0) {

                // if players doesnt have enough reserve bullets to top off mag
                if (rsrvBullets < clipSize - currBullets) {
                    currBullets += rsrvBullets;
                    rsrvBullets = 0;

                // fill mag and subtract from reserve bullets
                } else {
                    currBullets = clipSize;
                    rsrvBullets -= clipSize - currBullets;
                }
            }
        }
    }

    public void PickUp(PlayerController controller) {
        controller.EquipWeapon(this.gameObject);
    }
}
