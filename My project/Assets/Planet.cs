using UnityEngine;

public class Planet {

    public readonly int id;

    private Rigidbody body;
    private ConstantForce force { get; set; }

    public Transform transform { get; private set; }
    public float mass { get; private set; }

    [System.Serializable]
    public struct Props {
        public float mass;
        public Rigidbody body;
        public ConstantForce force;
    }

    public Planet(int id, float mass, Rigidbody body, ConstantForce force){
        this.id = id;
        this.body=body;
        body.mass = mass;
        this.mass = mass;
        this.force = force;
        this.transform = body.transform;
    }

    public Planet(Props props, int id): this( id, props.mass, props.body, props.force) { }

    /*
     * Given a force vector, updates the acceleration, velocity, and position vectors accordingly
     */
    internal void UpdateValues(Vector3 forceVec) {
        force.force = forceVec;
    }


}

