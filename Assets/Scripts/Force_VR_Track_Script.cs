using UnityEngine;
using System.Collections;

public class Force_VR_Track_Script : MonoBehaviour {
    public SteamVR_TrackedObject.EIndex forcedID;

    SteamVR_TrackedObject thisTrack;
    // Use this for initialization
    void Start() {
        thisTrack = this.GetComponent<SteamVR_TrackedObject>();

    }

    // Update is called once per frame
    void Update() {
        thisTrack.index = forcedID;
    }
}
