using UnityEngine;

public class Planet {

    public readonly int id;
    public float mass { get; private set; }
    public Vector3 position { get; private set; }
    public Vector3 velocity { get; private set; }
    public Vector3 accel { get; private set; }

    [System.Serializable]
    public struct Props {
        public float mass;
        public Vector3 position;
        public Vector3 velocity;
    }

    public Planet(float mass, int id, Vector3 pos, Vector3 vel) {
        this.id = id;
        this.mass = mass;
        position = pos;
        velocity = vel;
    }

    public Planet(Props props, int id): this( props.mass, id, props.position, props.velocity) { }

    /*
     * Given a force vector, updates the acceleration, velocity, and position vectors accordingly
     */
    internal void UpdateValues(Vector3 forceVec) {
        float t = Time.deltaTime;
        Vector3 initVelocity = new(velocity.x,velocity.y,velocity.z);

        accel = forceVec / mass; 
        velocity += (accel * t); //velocity final = velocity initial + acceleration x time
        position += ((initVelocity + velocity)/2) * t;  //change in x = average velocity x time
        Debug.Log($"Planet{id} update:\n " +
            $"force received: {forceVec}, delta time: {t} \nacceleration: {accel} \nvelocity: {velocity} \nposition: {position}");

    }

    public void TranslateGameObject(GameObject gameObject) {
        gameObject.transform.position = position;
    }

}

