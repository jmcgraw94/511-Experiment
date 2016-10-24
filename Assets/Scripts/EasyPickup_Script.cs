using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EasyPickup_Script : MonoBehaviour {

    public static List<GameObject> HeldObjects = new List<GameObject>();
    public GameObject HeldObject;
    public GameObject SnapGrabOrientation;

    Vector3 DeltaPos, DeltaRot;

    Vector3 CurPos, OldPos;
    Vector3 CurRot, OldRot;
    Rigidbody ActiveBody;

    bool freezeProtect = false;


    Animator Anim;

    [HideInInspector]
    public int ID;


    void Start() {

        ID = (int)this.GetComponentInParent<Force_VR_Track_Script>().forcedID;

    }

    void Update() {
        //HandAnim.gripping = SteamVR_Controller.Input(ID).GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
    }

    void FixedUpdate() {
        CurPos = this.transform.position;
        CurRot = this.transform.rotation.eulerAngles;
        DeltaPos = CurPos - OldPos;
        DeltaRot = CurRot - OldRot;



        if (!SteamVR_Controller.Input(ID).GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger)) {
            freezeProtect = false;
        }

        if (HeldObject != null) {
            ActiveBody.useGravity = false;
            ActiveBody.velocity = Vector3.zero;
            ActiveBody.angularVelocity = Vector3.zero;

            //HeldObject.transform.position += DeltaPos;
            //if (!NodeTeleport_Script.moving) {
                if (!SteamVR_Controller.Input(ID).GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger)) {
                    Release();
                }
                if (SteamVR_Controller.Input(ID).GetPress(Valve.VR.EVRButtonId.k_EButton_Grip)) {
                    FreezeRelease();
                }
            //}

            OldPos = CurPos;
            OldRot = CurRot;
        }
    }

    public void Release() {


        ActiveBody.useGravity = true;
        ActiveBody.isKinematic = false;

        float throwSpeed = 60;// (Mathf.Pow(DeltaPos.magnitude * 4, 2));
        throwSpeed = Mathf.Clamp(throwSpeed, 0, 70);
        ActiveBody.velocity += (DeltaPos) * (throwSpeed);
        //ActiveBody.AddRelativeTorque(DeltaRot * 2);
        foreach (Collider c in HeldObject.GetComponentsInChildren<Collider>())
            if (!c.isTrigger)
                c.enabled = true;
        HeldObject.transform.SetParent(null, true);
        HeldObjects.Remove(HeldObject);

        HeldObject = null;

        SteamVR_Controller.Input(ID).TriggerHapticPulse(2999);

    }

    public void FreezeRelease() {

        ActiveBody.isKinematic = false;

        foreach (Collider c in HeldObject.GetComponentsInChildren<Collider>())
            if (!c.isTrigger)
                c.enabled = true;

        HeldObject.transform.SetParent(null, true);
        HeldObjects.Remove(HeldObject);

        HeldObject = null;
        freezeProtect = true;

        SteamVR_Controller.Input(ID).TriggerHapticPulse(2999);

    }

    void OnTriggerStay(Collider other) {

        if (HeldObject == null &&
            SteamVR_Controller.Input(ID).GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger) && !freezeProtect
            && !HeldObjects.Contains(other.gameObject)) {

            ActiveBody = other.GetComponentInParent<Rigidbody>();

            if (ActiveBody == null)
                return; //Can't pick up these types of objects

            if (ActiveBody == null)
                ActiveBody = other.GetComponentInParent<Rigidbody>();

            EasyPickup_Script ParentPickup = other.GetComponentInParent<EasyPickup_Script>();
            if (ParentPickup != null)
                return;
            //ParentPickup.Release();

            HeldObject = ActiveBody.gameObject;
            foreach (Collider c in HeldObject.GetComponentsInChildren<Collider>())
                if (!c.isTrigger)
                    c.enabled = false;


            ActiveBody.velocity = Vector3.zero;
            ActiveBody.angularVelocity = Vector3.zero;

            ActiveBody.transform.SetParent(this.transform, true);

            //if (HeldObject.GetComponent<Attributes_Script>() != null && HeldObject.GetComponent<Attributes_Script>().SnapGrab) {
            //    ActiveBody.transform.SetParent(SnapGrabOrientation.transform, true);
            //    ActiveBody.transform.localRotation = Quaternion.Euler(Vector3.zero);
            //    ActiveBody.transform.localPosition = Vector3.zero;
            //}

            HeldObjects.Add(HeldObject);
            SteamVR_Controller.Input(ID).TriggerHapticPulse(2999);
            //ActiveBody.isKinematic = true;
        }
    }


}
