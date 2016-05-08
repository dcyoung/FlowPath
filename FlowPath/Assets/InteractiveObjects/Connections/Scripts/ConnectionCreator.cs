using UnityEngine;
using System.Collections;

public class ConnectionCreator : MonoBehaviour {

    public Material lineMaterial;
    //private Vector3? _from;
    //private Vector3? _to;
    private GameObject _from;
    private GameObject _to;
    
    //Subscribe to Event: at its creation, subscribe to receive updates from the prospectiveconnection manager
    void OnEnable()
    {
        ProspectiveConnectionManager.OnConnectingStateChange += UpdateConnectionProgress;
    }
    //Unsubscribe from Event: when disabling this item, unsubscribe from the prospectiveconnection manager
    void OnDisable()
    {
        ProspectiveConnectionManager.OnConnectingStateChange -= UpdateConnectionProgress;
    }

    // Use this for initialization
    void Start()
    {
        _from = null;
        _to = null;
    }

/*
    // Update is called once per frame
    void Update2 () {
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
            //draw line
        }
    }
*/

    private void UpdateConnectionProgress(ProspectiveConnectionState newState)
    {
        switch (newState)
        {
            case ProspectiveConnectionState.Latent:
                if( _from != null){ RemovePortSelectionVisual( _from ); }
                if( _to   != null){ RemovePortSelectionVisual( _to );   }
                _from = null;
                _to = null;
                break;
            case ProspectiveConnectionState.Initiated:
                _from = ProspectiveConnectionManager.GetSpecifiedOutputPort();
                //draw the visual representation of the selected port
                ShowPortSelectionVisual(_from);
                break;
            case ProspectiveConnectionState.Completed:
                _from = ProspectiveConnectionManager.GetSpecifiedOutputPort();
                _to = ProspectiveConnectionManager.GetSpecifiedInputPort();
                //draw the visual representations of both selected ports
                ShowPortSelectionVisual(_from);
                ShowPortSelectionVisual(_to);
                break;
            default:
                break;
        }
    }

    //DRAW SOME VISUAL REPRESENTATION OF THE PORT
    private void ShowPortSelectionVisual(GameObject port)
    {
        ///FIXME:
    }

    //Remove any visual representation of the port
    private void RemovePortSelectionVisual(GameObject port)
    {
        ///FIXME:
    }

    //Old draw line method... will not work with VR, but good for debugging.
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
