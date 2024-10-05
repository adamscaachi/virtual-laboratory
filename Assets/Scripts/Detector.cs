using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class Detector : MonoBehaviour{

    private double k = 500;
    public GameObject character;
    public TMP_Text readout;

    void Start(){
    	StartCoroutine(PrintCounts());   
    }

    IEnumerator PrintCounts(){
	while (true){
	    float distance = GetDistance();
	    double mean = InverseSquare(distance);
	    double counts = GenerateCounts(mean);
	    readout.text = $"  Distance from source: {distance:F1} m\n  Counts: {counts} per second\n ";
	    yield return new WaitForSeconds(1f);
	}
    }

    float GetDistance(){
	Vector3 characterPosition = character.transform.position;
	Vector3 sourcePosition = new Vector3(0, 1, 0);
	return Vector3.Distance(characterPosition, sourcePosition);
    }

    double InverseSquare(float distance){
	return k / (distance * distance);
    }

    int GenerateCounts(double lambda){
	double L = Math.Exp(-lambda);
	double p = 1.0;
	int k = 0;
	while (p > L){
	    k++;
	    p *= UnityEngine.Random.value;
	}	
	return k - 1;
    }


}
