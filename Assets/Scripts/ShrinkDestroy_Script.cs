using UnityEngine;
using System.Collections;

public class ShrinkDestroy_Script : MonoBehaviour {

    float startMag;

	void Start () {
        GameObject.Destroy(this.GetComponent<Collider>());
        GameObject.Destroy(this.GetComponent<Rigidbody>());
        startMag = this.transform.localScale.magnitude;
	}
	
	void FixedUpdate () {
        this.transform.localScale *= .85f;

        if (this.transform.localScale.magnitude < startMag / 100) {
            GameObject.Destroy(this.gameObject);
        }
	}
}
