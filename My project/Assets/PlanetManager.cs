using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class PlanetManager : MonoBehaviour {

    public static PlanetManager instance;

    static readonly float G = 6.674f;

    //This accepts the ID 
    public event Action OnForcesUpdated;

    public List<Planet> planets { get; private set; }
    // This field keeps track of all the forces acting on a current object BEFORE the planet.UpdateValues() is called
    // After planet.UpdateValues() is called, the index corresponding to the planets force vector should be cleared
    private List<Vector3> forces = new();

    /*
     * Given the planet props, will return a planet object
     * Assigns ID based on planets.count
     */
    public Planet AddPlanet(Planet.Props props) {
        Planet newest = new(props, planets.Count);
        planets.Add(newest);
        forces.Add(Vector3.zero);
        Debug.Log($"Added planet {newest.id}");
        return newest;
    }

    Vector3 FindForce(Planet p1, Planet p2) {
        //Other position - this position = vector pointing from p1 to p2
        Vector3 tweener = new Vector3(p2.position.x, p2.position.y, p2.position.z);
        tweener = tweener - p1.position;

        float distance = tweener.magnitude;
        //Debug.Log($"Distance: {distance}");
        tweener.Normalize();

        float grav = (G * p1.mass * p2.mass) / (distance * distance);
        //Debug.Log($"Gravity: {grav}");

        //Tweener vector now = vector representation of gravity 
        tweener *= grav;

        return tweener;
    }

    /*
     * For every pair of planets, the force between them is calculated, then added to their respective indices in planet list
     * At the end of this function every planet's index in forces should represent the force exerted on them that frame
     */
    void PopulateForces() {
        resetForces();
        for (int i = 0; i < planets.Count; i++) {
            for (int j = i+1; j < planets.Count; j++) {
                Vector3 iToJ = FindForce(planets[i], planets[j]);
                Vector3 jToI = iToJ * -1;
                forces[i] += iToJ;
                forces[j] += jToI;
            }
        }
    }

    void updateValues() {
        for (int i = 0; i < planets.Count; i++) {
            planets[i].UpdateValues(forces[i]);
        }
    }
   
    void resetForces() {
        for(int i=0; i<forces.Count; i++) {
            forces[i] = Vector3.zero;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Singleton();
        this.planets = new();
    }

    // Update is called once per frame
    void Update()
    {
        PopulateForces();
        updateValues();
        OnForcesUpdated.Invoke();
    }

    private void Singleton() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.Log("Two managers");
            Destroy(gameObject);
        }
    }

}
