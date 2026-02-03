using UnityEngine;
using System.Collections.Generic;
using Unity.Properties;

public class PlanetScript : MonoBehaviour
{
    [SerializeField]
    public Planet.Props props;

    [SerializeField]
    private bool takePosFromTransform;

    [SerializeField]
    UIScript uiscript;

    private Planet planet;

    //Subscribe to an event that when called will fire the MovePlanet function
    void MovePlanet() {
        planet.TranslateGameObject(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (takePosFromTransform) {
            props.position = gameObject.transform.position;
        }
        planet = PlanetManager.instance.AddPlanet( props );
        PlanetManager.instance.OnForcesUpdated += MovePlanet;
        gameObject.transform.SetPositionAndRotation(props.position, Quaternion.identity);
    }

    //// Update is called once per frame
    //void Update()
    //{
    //}
}
