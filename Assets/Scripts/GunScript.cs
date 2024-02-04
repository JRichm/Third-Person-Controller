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
    private Camera playerCamera;

    private InteractScript interactScript;
    private AudioSource audioSource;
    [SerializeField] private GameObject shootFromPoint;
    [SerializeField] private Material tracerMaterial;

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

            RaycastHit lookatPoint;

            Vector3 bulletLandPoint;

            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out lookatPoint)) {
                bulletLandPoint = lookatPoint.point;
            } else {
                bulletLandPoint = playerCamera.transform.forward * 1000;
            }

            audioSource.Play();
            ShowTracer(bulletLandPoint);
            currBullets--;
        } else if (rsrvBullets < 0) {
            Debug.Log("No ammo");
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
        playerCamera = controller.userCamera;
    }

    private void ShowTracer(Vector3 bulletLandPoint) {
        GameObject newTracer = new GameObject("Tracer");
        TracerScript tracerScript = newTracer.AddComponent<TracerScript>();
        tracerScript.ShowTracer(shootFromPoint.transform.position, bulletLandPoint, tracerMaterial);
    }
}