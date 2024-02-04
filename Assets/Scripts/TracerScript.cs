using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TracerScript : MonoBehaviour
{
    private LineRenderer tracerRenderer;
    private float width;
    public bool useCurve = true;

    private void Awake() {
        tracerRenderer = this.AddComponent<LineRenderer>();
        width = 0.25f;
    }

    public void ShowTracer(Vector3 shootFromPoint, Vector3 bulletLandPoint, Material tracerMaterial) {
        Vector3[] points = new Vector3[] { shootFromPoint, bulletLandPoint };
        tracerRenderer.SetPositions(points);
        tracerRenderer.SetPosition(0, shootFromPoint);
        tracerRenderer.SetPosition(1, bulletLandPoint);
        tracerRenderer.material = tracerMaterial;
    }

    private void Update() {
        AnimationCurve curve = new AnimationCurve();
        if (useCurve) {
            curve.AddKey(0.0f, width);
            curve.AddKey(10, width);
        } else {
            curve.AddKey(0.0f, width);
            curve.AddKey(3.0f, width);
        }

        tracerRenderer.widthCurve = curve;
        tracerRenderer.widthMultiplier = width;

        width -= Time.deltaTime / 1.25f;
        if (width < 0) {
            Destroy(this.gameObject);
        }
    }
}
