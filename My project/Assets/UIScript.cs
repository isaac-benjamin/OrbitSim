using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public class UIScript : MonoBehaviour
{
    public static UIScript instance;

    private UIDocument uiDoc;
    [SerializeField] private VisualTreeAsset windowTemplate;
    private VisualElement planetWindowContainer;
    private Planet x;

    private void Awake() {
        Singleton();
        uiDoc = gameObject.GetComponent<UIDocument>();
        planetWindowContainer = uiDoc.rootVisualElement.Q<VisualElement>("planetHouse");
    }

    public void MakeStatusWindow (Planet planet) {
        TemplateContainer window = windowTemplate.Instantiate();
        window.dataSource = planet;
        window.AddToClassList("statusWindowContainer");

        PropertyPath[] bindings = {new PropertyPath(nameof(planet.position)),new PropertyPath(nameof(planet.velocity)),
            new PropertyPath(nameof(planet.accel)), new PropertyPath(nameof(planet.force))  };
        String[] fieldNames = { "position", "velocity", "acceleration", "force" };

        for (int i = 0; i < bindings.Length; i++) {

            var vecField = window.Query(fieldNames[i]).Descendents<Vector3Field>().First();
            vecField.SetBinding("value", new DataBinding { dataSourcePath = bindings[i] });
        }

        planetWindowContainer.Add(window);

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
