using UnityEngine;
using TMPro;

public class FPS : MonoBehaviour{

    public TMP_Text info;
    float deltaTime = 0.0f;
    float updateInterval = 1.0f; 
    float accumulatedTime = 0.0f;
    int frames = 0;

    void Update(){
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        frames++;
        accumulatedTime += Time.deltaTime;
        if (accumulatedTime >= updateInterval){
            float fps = frames / accumulatedTime;
            info.text = $"\n  FPS: {Mathf.Ceil(fps)}";
            frames = 0;
            accumulatedTime = 0.0f;
        }
    }

}
