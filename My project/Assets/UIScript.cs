using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public class UIScript : MonoBehaviour
{

    private UIDocument uiDoc;
    [SerializeField] private VisualTreeAsset windowTemplate;
    private VisualElement planetWindowContainer;
    private Planet x; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiDoc = gameObject.GetComponent<UIDocument>();
        planetWindowContainer = uiDoc.rootVisualElement.Q<VisualElement>("planetHouse");

        x = new(20, 8, new(1, 44, 5), new(9, 0, 9));
        var myAsset = uiDoc.rootVisualElement.Q<Vector3Field>();
        myAsset.style.backgroundColor = new Color(100, 0, 0);
        myAsset.dataSource = x;
        //examplePlanet = PlanetManager.instance.planets[0];
        myAsset.SetBinding("value", new DataBinding { dataSourcePath = new PropertyPath(nameof(x.position)) });
    }

    public VisualTreeAsset MakeStatusWindow (ref Planet planet) {
        TemplateContainer window = windowTemplate.Instantiate();
        window.dataSource = planet;

        PropertyPath[] bindings = {new PropertyPath(nameof(planet.position)),new PropertyPath(nameof(planet.velocity)),
            new PropertyPath(nameof(planet.accel)), new PropertyPath(nameof(planet.force))  };
        String[] fieldNames = { "position", "velocity", "acceleration", "force" };

        for (int i = 0; i < bindings.Length; i++) {

            var vecField = window.Query(fieldNames[i]).Descendents<Vector3Field>().First();
            vecField.SetBinding("value", new DataBinding { dataSourcePath = bindings[i] });
        }

        uiDoc.rootVisualElement.Add(window);

        return null;
    }
    

}
