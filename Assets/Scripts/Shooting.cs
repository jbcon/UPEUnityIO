using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

    
    public float explodeForce = 20f;
    public float explodeRadius = 10f;
    public float maxDist = 100f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown("Fire1"))
        {
            GameObject lineOfFire = new GameObject();
            Destroy(lineOfFire, 0.5f);
            LineRenderer lofLine = lineOfFire.AddComponent<LineRenderer>();
            lofLine.SetColors(Color.blue, Color.red);
            lofLine.SetPosition(0, Camera.main.transform.position);
            lofLine.SetWidth(0.1f, 0.1f);
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * maxDist);
            // perform raycast-based shot
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDist, LayerMask.NameToLayer("Default")))
            {
                Vector3 hitPoint = hit.point;
                lofLine.SetPosition(1, hitPoint);
                Debug.Log(hitPoint);
                Rigidbody[] rbs = FindObjectsOfType<Rigidbody>();
                foreach(Rigidbody rb in rbs)
                {
                    rb.AddExplosionForce(explodeForce, hitPoint, explodeRadius);
                }
            }
            else
            {
                lofLine.SetPosition(1, Camera.main.transform.position + Camera.main.transform.forward * maxDist);
            }
        }
	}
}
