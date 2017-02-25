using UnityEngine;
using System.Collections;

public class Unit_Selection : MonoBehaviour {

    [SerializeField] private SteamVR_TrackedController c;

    private bool wasPressed = false;
    private LineRenderer lineRenderer;
    private RaycastHit hit;
    private RaycastHit hitTarget;

    private int lengthOfRender;
    private Vector3[] points; //= new Vector3[lengthOfRender];
    private Vector3 forward;

    public static Vector3 TargetVector;
    //public static bool readUpdate = false;

    // Use this for initialization
    void Start () {
        lengthOfRender = 2;
        points = new Vector3[lengthOfRender];
        forward = gameObject.transform.TransformDirection(Vector3.forward) * 10;
        c = GetComponent<SteamVR_TrackedController>();
        lineRenderer = GetComponent<LineRenderer>();
        points[0] = gameObject.transform.position;
        points[1] = forward;
    }
	
	// Update is called once per frame
	void Update () {
        SelectUnits();
        forward = gameObject.transform.TransformDirection(Vector3.forward) * 10;
        if (c.triggerPressed)
        {
            Debug.Log("Here4");

            if (!wasPressed)
            {
                Debug.Log("Here3");

                OnSelect();
            }
            wasPressed = true;
        }
        else
        {
            wasPressed = false;
        }
        //readUpdate = false;
        
    }

    private void SelectUnits()
    {
        points[0] = gameObject.transform.position;
        points[1] = forward;
        lineRenderer.SetPositions(points);
        //Check if the controller is pointing at a unit
        
    }


    private void OnSelect()
    {
        
        if (Physics.CapsuleCast(points[0], points[1], .05f, forward, out hit))
        {
            Debug.Log("Here1");

            //Check if trigger is pressed when pointing at unit
            if (c.triggerPressed /*&& c.controllerIndex == 2*/)
            {
                Debug.Log("Here2");

                //Select unit when trigger is pressed while pointing at it

                var su = hit.transform.gameObject.GetComponentInParent<SelectableUnit>();
                su.Selected = !su.Selected;

                Debug.Log(su.name); 
                //hit.transform.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(255, 0, 0)); //MOVED TO SELECTABLEUNIT CLASS
            }
        }
        //Debug.DrawRay(transform.position, forward, Color.green, 2.0f);
        //lineRenderer.SetPosition(0, forward);

    }

    private void SelectTarget()
    {
        if (Physics.Raycast(points[0], points[1], out hitTarget))
        {
            //Check if trigger is pressed when pointing at unit
            if (c.triggerPressed && c.controllerIndex == 1)
            {
                //readUpdate = true;
                TargetVector = hitTarget.point;
            }
        }
    }
}
