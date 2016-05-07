using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum InteractionMode
{
    SpectatorMode,
    MoveObjectsMode,
    ConnectionMode
}


public class ModeManager : MonoBehaviour {
    public Material lineMaterial;
    public GameObject text; // text object for panel
    private Text editText;
    static InteractionMode currentMode;
    //static bool connectionMode; // when true we are connecting components rather than building
    private Vector3? _from;
    private Vector3? _to;

    // Use this for initialization
    void Start () {
        currentMode = InteractionMode.MoveObjectsMode;
        _from = null;
        _to = null;
        editText = text.GetComponent<Text>();
        updateDisplayText();
    }

    void updateDisplayText()
    {
        switch (currentMode)
        {
            case InteractionMode.ConnectionMode:
                editText.text = "Mode:\n\nConnection";
                break;
            case InteractionMode.MoveObjectsMode:
                editText.text = "Mode:\n\nMove Objects";
                break;
            case InteractionMode.SpectatorMode:
                editText.text = "Mode:\n\nSpectate";
                break;
            default:
                editText.text = "Default";
                break;
        }
    }
    
    // Update is called once per frame
    void Update () {
        if (currentMode == InteractionMode.ConnectionMode) {
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
            cycleConnectionMode();
        }

        if (_from != null && _to != null) {
            drawLine((Vector3) _from, (Vector3) _to);
        }
    }

    public void cycleConnectionMode() {
        switch (currentMode)
        {
            case InteractionMode.ConnectionMode:
                currentMode = InteractionMode.SpectatorMode;
                break;
            case InteractionMode.MoveObjectsMode:
                currentMode = InteractionMode.ConnectionMode;
                break;
            case InteractionMode.SpectatorMode:
                currentMode = InteractionMode.MoveObjectsMode;
                break;
            default:
                currentMode = InteractionMode.SpectatorMode;
                break;
        }
        updateDisplayText();
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
