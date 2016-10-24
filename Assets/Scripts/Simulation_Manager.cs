using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Simulation_Manager : MonoBehaviour {

    public static Simulation_Manager CurInstance;
    public enum ColorType {
        Color,
        Monotone,
    }

    public GameObject Cube_Prefab, Cylinder_Prefab, Sphere_Prefab, Torus_Prefab;
    public ColorType Color_Type;
    public List<float> TimeScores = new List<float>();

    public float currentTimer = 0;

    void Start () {
        CurInstance = this;
	}
	
	void Update () {
        
	}

    void FixedUpdate() {
        currentTimer += Time.fixedDeltaTime;
    }

    void SetColor() {

    }
}
