using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Function : MonoBehaviour{

    private LineRenderer lineRenderer;
    public Slider slider;
    public TMP_Text kText;
    int numPoints = 1000;

    void Start(){
        lineRenderer = GetComponent<LineRenderer>();
	lineRenderer.positionCount = numPoints;
	DrawCurve();
    }

    public void DrawCurve(){
	float k = slider.value * 1000f;
	kText.text = "k = " + k.ToString("F0");
	Vector3[] positions = new Vector3[numPoints];
	int validPointIndex = 0;
	for (int i = 0; i < numPoints; i++){
	    float x_world = Mathf.Lerp(-6.464f, 7.2844f, (float)i / (numPoints - 1));
	    float x_graph = Mathf.Lerp(2, 14, (float)i / (numPoints - 1));
	    float y_graph = k / (x_graph * x_graph);
	    float y_world = 0.065709f * y_graph + 2.021f;
	    if (y_world <= 8.5919){
		positions[validPointIndex] = new Vector3(x_world, y_world, 9.899f);
		validPointIndex++;
	    }
	}	
	System.Array.Resize(ref positions, validPointIndex);
    	lineRenderer.positionCount = positions.Length;
    	lineRenderer.SetPositions(positions);
    }

}

