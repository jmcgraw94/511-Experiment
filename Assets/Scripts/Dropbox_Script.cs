using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dropbox_Script : MonoBehaviour {

    public Shape_Script.ShapeType Type;

    void Start() {

    }

    void Update() {

    }

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Shape_Script>() != null) {
            if (other.GetComponent<Shape_Script>().Type == this.Type) {
                print("Correct!");
            }

            other.gameObject.AddComponent<ShrinkDestroy_Script>();
            ShapeSpawner_Script.CurInstance.ActiveShape = false;
            Simulation_Manager.CurInstance.TimeScores.Add(Simulation_Manager.CurInstance.currentTimer);
            Simulation_Manager.CurInstance.currentTimer = 0;
            //GameObject.Destroy(other.gameObject);
        }
    }
}
