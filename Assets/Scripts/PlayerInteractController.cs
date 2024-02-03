using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractController : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] float interactRange = 1.5f;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray) {
                if (collider.TryGetComponent(out InteractScript interactScript)) {
                    interactScript.Interact(controller);
                }
            }
        }
    }

    //  private void OnDrawGizmos() {
    //  Gizmos.color = Color.yellow;
    //  Gizmos.DrawWireSphere(transform.position + (Vector3.up * 2), interactRange);
    //
    //  Debug.DrawRay(transform.position + (Vector3.up * 2), controller.cameraHolder.transform.forward * interactRange, Color.red);
    //}
}
