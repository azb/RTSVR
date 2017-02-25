using UnityEngine;
using System.Collections;

public class SelectableUnit : MonoBehaviour {

    [SerializeField] private bool selected;
    [SerializeField] Vector3 moveTo;
    public const float MovementThreshold = 20.0f;
    public bool Selected
    {
        get
        {
            return selected;
        }

        set
        {
            selected = value;
        }
    }

    // Use this for initialization
    void Start () {
        Selected = false;
    }
	void Update()
    {
        Renderer[] rends = gameObject.GetComponentsInChildren<Renderer>();
        if (selected)
        {
            foreach(Renderer r in rends)
            {
                r.material.SetColor("_EmissionColor", new Color(50, 50, 0));
            }
        }
        else
        {
            foreach (Renderer r in rends)
            {
                r.material.SetColor("_EmissionColor", new Color(0, 0, 0));
            }
        }
        if(selected)
        {
            moveTo = new Vector3(Unit_Selection.TargetVector.x, Unit_Selection.TargetVector.y, Unit_Selection.TargetVector.z);
        }
        if(moveTo != null)
        {
            MoveTo(moveTo);
        }
    }

    private void MoveTo(Vector3 target)
    {
        Transform[] transf = gameObject.GetComponentsInChildren<Transform>();

        foreach(Transform t in transf)
        {
            if (Vector3.Distance(t.position, target) >= MovementThreshold)
            {
                t.position = Vector3.Lerp(t.position, target, MovementThreshold / 2);
            }
        }
    }
}
