using UnityEngine;
using System.Collections.Generic;
using Unity.Properties;

public class PlanetScript : MonoBehaviour
{
    [SerializeField]
    public Planet.Props props;

    private Planet planet;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        planet = PlanetManager.instance.AddPlanet( props );
    }

    //// Update is called once per frame
    //void Update()
    //{
    //}
}
