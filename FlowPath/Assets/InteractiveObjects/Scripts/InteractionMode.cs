using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractionMode : MonoBehaviour {
    public Material lineMaterial;
    public GameObject text; // text object for panel
    private Text editText;
    static bool connectionMode; // when true we are connecting components rather than building
    private Vector3? _from;
    private Vector3? _to;

    // Use this for initialization
    void Start () {
        connectionMode = false;
        _from = null;
        _to = null;
        editText = text.GetComponent<Text>();
        editText.text = connectionMode ? "Mode:\n\nWire" : "Mode:\n\nBuild";
    }
    
    // Update is called once per frame
    void Update () {
        if (connectionMode) {
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
            toggleConnectionMode();
        }

        if (_from != null && _to != null) {
            drawLine((Vector3) _from, (Vector3) _to);
        }
    }

    public void toggleConnectionMode() {
        connectionMode = !connectionMode;
        editText.text = connectionMode ? "Mode:\n\nWire" : "Mode:\n\nBuild";
    }

    private void drawLine(Vector3 start, Vector3 end) {
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
