using UnityEngine;
using System.Collections;

public class ConnectionCreator : MonoBehaviour {

    public Material lineMaterial;
    private Vector3? _from;
    private Vector3? _to;

    // Use this for initialization

    void Start()
    {
        _from = null;
        _to = null;
    }


    // Update is called once per frame
    void Update () {
        if (ModeManager.currentMode == InteractionMode.ConnectionMode) {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000.0f)) {
                    _from = hit.point; // where the hit actually happens
                }
                else _from = null;
            } else if (Input.GetKeyUp(KeyCode.Mouse0)) {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 1000.0f)) {
                        _to = hit.point;
                    }
                    else _to = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.M)) {
            ModeManager.cycleMode();
        }

        if (_from != null && _to != null) {
            drawLine((Vector3) _from, (Vector3) _to);
        }
    }
    


    private void drawLine(Vector3 start, Vector3 end)
    {
        GameObject newChild = new GameObject();
        newChild.transform.parent = gameObject.transform;
        LineRenderer line = newChild.AddComponent<LineRenderer>();
        Color startColor = Color.green;
        Color endColor = Color.red;
        line.SetColors(startColor, endColor);
        line.material = lineMaterial;
        line.SetWidth(0.01f, 0.01f);
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        _from = null;
        _to = null;
    }
}
