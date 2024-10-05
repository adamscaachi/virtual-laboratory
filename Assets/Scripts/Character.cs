using UnityEngine;

public class Character : MonoBehaviour{

    private GameManager gameManager;
    public float moveSpeed = 5f; 
    public float sensitivity = 2f; 
    private Transform cameraTransform; 
    private Renderer characterRenderer;

    void Start(){
	gameManager = FindObjectOfType<GameManager>();
        cameraTransform = Camera.main.transform;
	characterRenderer = GetComponent<Renderer>();
	characterRenderer.enabled = false;
    }

    void Update(){
	switch(gameManager.CurrentState){
	    case GameManager.GameState.Default:
	        MoveCharacter();
	        RotateCamera();
		break;
	    default:
		break;
	}
    }

    void MoveCharacter(){
        float horizontalInput = Input.GetAxisRaw("Horizontal"); 
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    void RotateCamera(){
        float mouseX = Input.GetAxis("Mouse X") * sensitivity; 
        transform.Rotate(Vector3.up * mouseX);
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        cameraTransform.Rotate(-mouseY, 0, 0);
    }

}
