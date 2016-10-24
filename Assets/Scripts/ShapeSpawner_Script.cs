using UnityEngine;
using System.Collections;

public class ShapeSpawner_Script : MonoBehaviour {

    public static ShapeSpawner_Script CurInstance;

    public bool ActiveShape = false;

    [HideInInspector]
    public float SpawnDelay = .3f;
    public float DEF_SpawnDelay;

    void Start() {
        DEF_SpawnDelay = SpawnDelay;
        CurInstance = this;
    }

    void Update() {
        if (!ActiveShape) {
            SpawnDelay -= Time.deltaTime;
            if (SpawnDelay < 0) {
                SpawnDelay = DEF_SpawnDelay;
                SpawnRandomShape();
            }
        }
    }

    void SpawnRandomShape() {
        ActiveShape = true;
        Simulation_Manager.CurInstance.currentTimer = 0;
        switch ((int)(Random.value * 3)) {
            case 0:
                GameObject.Instantiate(Simulation_Manager.CurInstance.Cube_Prefab,
                    this.transform.position, Quaternion.identity);
                break;
            case 1:
                GameObject.Instantiate(Simulation_Manager.CurInstance.Cylinder_Prefab,
                    this.transform.position, Quaternion.identity);
                break;
            case 2:
                GameObject.Instantiate(Simulation_Manager.CurInstance.Torus_Prefab,
                    this.transform.position, Quaternion.identity);
                break;
            case 3:
                GameObject.Instantiate(Simulation_Manager.CurInstance.Sphere_Prefab,
                    this.transform.position, Quaternion.identity);
                break;
        }
    }

    void SpawnSpecific() {
        ActiveShape = true;
    }
}
