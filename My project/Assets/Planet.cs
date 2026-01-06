using UnityEngine;
using System.Collections.Generic;

public class Planet : MonoBehaviour
{
    public int mass;
    public Vector3Int initPosition;
    public Vector3Int initVelocity;

    static readonly float g = 6.674f;
    static List<PlanetProps> planets = new List<PlanetProps>();

    private PlanetProps planet;

    public class PlanetProps {
        int id;
        float mass;
        Vector3 position;
        Vector3 deltaPos;
        Vector3 velocity;
        Vector3 accel;

        public PlanetProps(float mass, Vector3 pos, Vector3 vel) {
            this.id = planets.Count;
            planets.Add(this);
            this.mass = mass;
            position = pos;
            velocity = vel;
        }

        void findForce(PlanetProps props)
        {
            //Other position - this position = vector pointing from this to other
            Vector3 tweener = new Vector3(props.position.x,props.position.y,props.position.z);
            tweener = tweener - position;

            float distance = tweener.magnitude;
            tweener.Normalize();

            float grav = (g * mass * props.mass) / (distance*distance);

            //Tweener vector now = vector representation of gravity 
            tweener *= grav;

            //Set acceleration
            this.accel = tweener / mass;

        }

        internal void updateValues(){
            velocity+=(accel*Time.deltaTime);
            position += (velocity * Time.deltaTime);
            
        }

        internal void TranslateGameObject(GameObject gameObject){
            Vector3 translation = new Vector3(velocity.x, velocity.y, velocity.z)*Time.deltaTime;
            gameObject.transform.Translate(translation);
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.transform.SetPositionAndRotation(initPosition, Quaternion.identity);
        planet = new PlanetProps(mass,initPosition,initVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        planet.updateValues();
        planet.TranslateGameObject(gameObject);
    }
}
