using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour{

    public enum GameState{
        Default,
	Transition,
        ViewingGraph
    }

    public GameState CurrentState { get; private set; } = GameState.Default;
    private Camera mainCamera;
    public float maxDistance = 10f;
    public TMP_Text promptText;
    public GameObject xInputField;
    public GameObject yInputField;
    public GameObject plotButton;
    public GameObject exitButton;
    public GameObject kSlider;
    public GameObject kText;

    private Vector3 iPos;
    private Quaternion iRot;
    private Vector3 fPos;
    private Quaternion fRot;
    private float cameraSpeed = 5f;


    void Start(){
	ToggleGraphUI(false);
        mainCamera = Camera.main;
    }

    void Update(){
        switch (CurrentState){
            case GameState.Default:
                HandleDefaultState();
                break;
	    case GameState.Transition:
		HandleTransitionState();
		break;
            case GameState.ViewingGraph:
                HandleViewingGraphState();
                break;
            default:
                break;
        }
    }

    void HandleDefaultState(){
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        string text = " ";
        if (Physics.Raycast(ray, out hit, maxDistance)){
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.name == "Graph"){
                text = "Click to view graph \n ";
                if (Input.GetMouseButtonDown(0)){
	            iPos = mainCamera.transform.position;
                    iRot = mainCamera.transform.rotation;
                    fPos = new Vector3(0f, 5f, 2f);
                    fRot = Quaternion.Euler(0f, 0f, 0f);
		    StartCoroutine(Transition(iPos, iRot, fPos, fRot, GameState.ViewingGraph));
                }
            }
        }
        promptText.text = text;
    }

    void HandleTransitionState(){
	if (plotButton.activeSelf) ToggleGraphUI(false);
	promptText.text = " ";
    }

    void HandleViewingGraphState(){
	if (!plotButton.activeSelf) ToggleGraphUI(true);
    }

    IEnumerator Transition(Vector3 iPos, Quaternion iRot, Vector3 fPos, Quaternion fRot, GameState fState){
	CurrentState = GameState.Transition;
        float distance = Vector3.Distance(iPos, fPos);
        float duration = distance / cameraSpeed;
        float elapsedTime = 0f;
	while (elapsedTime < duration){
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            mainCamera.transform.position = Vector3.Lerp(iPos, fPos, t);
            mainCamera.transform.rotation = Quaternion.Slerp(iRot, fRot, t);
	    yield return null;
	}
	CurrentState = fState;
    }

    void ToggleGraphUI(bool active){
	xInputField.SetActive(active);
	yInputField.SetActive(active);
        plotButton.SetActive(active);
	exitButton.SetActive(active);
	kSlider.SetActive(active);
	kText.SetActive(active);
	Cursor.visible = active;
    }

    public void OnExitButtonClick(){
	StartCoroutine(Transition(fPos, fRot, iPos, iRot, GameState.Default));
    }

}

