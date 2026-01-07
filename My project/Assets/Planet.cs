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
        Debug.Log($"Planet{id} position: {position}");
    }

    public Planet(Props props, int id): this( props.mass, id, props.position, props.velocity) { }

    /*
     * Given a force vector, updates the acceleration, velocity, and position vectors accordingly
     */
    internal void UpdateValues(Vector3 forceVec) {
        accel = forceVec / mass;
        velocity += (accel * Time.deltaTime);
        position += (velocity * Time.deltaTime);
        Debug.Log($"Planet{id} update:\n " +
            $"force received - {forceVec} \nacceleration - {accel} \nvelocity - {velocity} \nposition - {position}");

    }

    public void TranslateGameObject(GameObject gameObject) {
        Vector3 translation = new Vector3(velocity.x, velocity.y, velocity.z) * Time.deltaTime;
        Debug.Log($"Planet{id} translation: {translation}");
        gameObject.transform.Translate(translation);
    }

}

