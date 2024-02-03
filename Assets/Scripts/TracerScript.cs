using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TracerScript : MonoBehaviour
{
    private LineRenderer tracerRenderer;

    private void Start() {
        tracerRenderer = this.AddComponent<LineRenderer>();
    }

    public void ShowTracer(Transform shootPoint, Camera playerCamera) {

        RaycastHit lookatPoint;




        // get point user is looking at
        
        // if point hits somethings
            // draw tracer from gun to hit point

        // else
            // draw tracer from gun to lookat point





        RaycastHit hit;

        if (Physics.Raycast(shootPoint.position, -shootPoint.right, out hit)) {
            Debug.DrawRay(shootPoint.position, -shootPoint.right * hit.distance, Color.green, 1000);
            Debug.Log("Did Hit");
        } else {
            Debug.DrawRay(shootPoint.position, -shootPoint.right * 1000, Color.red, 1000);
            Debug.Log("Did not Hit");
        }

        //tracerRenderer.positionCount = 2;
        //tracerRenderer.SetPosition(0, shootPoint.position);
        //tracerRenderer.SetPosition(1,)
    }
}
