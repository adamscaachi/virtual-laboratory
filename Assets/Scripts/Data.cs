using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Data : MonoBehaviour{

    public GameObject dataPrefab;
    public TMP_InputField xInputField;
    public TMP_InputField yInputField;

    void AddDataToPlot(float x_graph, float y_graph){
	Vector2 worldPosition = GraphToWorldPosition(x_graph, y_graph);
        Instantiate(dataPrefab, new Vector3(worldPosition.x, worldPosition.y, 9.9f), Quaternion.identity);  
    }

    Vector2 GraphToWorldPosition(float x_graph, float y_graph){
	float x_world = 1.1457f * x_graph - 8.7554f;
	float y_world = 0.065709f * y_graph + 2.021f;
	return new Vector2(x_world, y_world);
    }

    public void OnButtonClick(){
        float x_graph, y_graph;
        if (float.TryParse(xInputField.text, out x_graph) && float.TryParse(yInputField.text, out y_graph)){
	    AddDataToPlot(x_graph, y_graph);
        }
    }


}
