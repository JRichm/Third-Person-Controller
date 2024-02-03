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
    private float timeBetweenShots;
    private Camera userCamera;

    private InteractScript interactScript;
    private AudioSource audioSource;
    [SerializeField] private GameObject shootFromPoint;

    private void Start() {

        isEquipped = false;

        currBullets = clipSize;
        maxAmmo = clipSize * 5;
        rsrvBullets = clipSize * 2;
        timeBetweenShots = 0.5f;

        interactScript = this.AddComponent<InteractScript>();
        audioSource = GetComponent<AudioSource>();
    }

    public void HandleShoot() {
        if (currBullets > 0) {
            Debug.Log("Shot bullet");
            audioSource.Play();
            ShowTracer();
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

    private void ShowTracer() {
        GameObject newTracer = new GameObject("String");
        TracerScript tracerScript = newTracer.AddComponent<TracerScript>();
        tracerScript.ShowTracer(shootFromPoint.transform, userCamera);
    }
}