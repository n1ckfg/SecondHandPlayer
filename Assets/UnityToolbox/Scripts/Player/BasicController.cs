using UnityEngine;
using System.Collections;
//using UnityEngine.Networking;

//[RequireComponent(typeof(NetworkIdentity))]
//public class BasicController : NetworkBehaviour {
public class BasicController : MonoBehaviour
{

    private bool isMobile = false;

    private void Awake() {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
		isMobile = true;
#endif
    }

    private void Start() {
        Cursor.visible = showCursor;
        //if (!isLocalPlayer) return;
        if (useKeyboard) wasdStart();
        if (useMouse) mouseStart();
    }

    private void Update() {
        //if (!isLocalPlayer) return;
        if (!fixedZ) zPos = lastHitPos.z;

        if (useKeyboard) wasdUpdate();
        if (useMouse) mouseUpdate();
        if (useRaycaster) rayUpdate();
    }

    // ~ ~ ~ ~ ~ ~ ~ ~ 

    [Header("Keyboard")]
    public bool useKeyboard = true;
    public bool useYAxis = false;
    public string yAxisName = "Vertical2";
    public float walkSpeed = 10f;
    public float runSpeed = 100f;
    public float accel = 0.01f;
    public Transform homePoint;

    private float currentSpeed;
    private Vector3 p = Vector3.zero;
    private bool run = false;

    private void wasdStart() {
        currentSpeed = walkSpeed;
    }

    private void wasdUpdate() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            run = true;
        } else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            run = false;
        }

        if (run && currentSpeed < runSpeed) {
            currentSpeed += accel;
            if (currentSpeed > runSpeed) currentSpeed = runSpeed;
        } else if (!run && currentSpeed > walkSpeed) {
            currentSpeed -= accel;
            if (currentSpeed < walkSpeed) currentSpeed = walkSpeed;
        }

        p.x = Input.GetAxis("Horizontal") * Time.deltaTime * currentSpeed;
        if (useYAxis) {
            p.y = Input.GetAxis(yAxisName) * Time.deltaTime * currentSpeed;
        } else {
            p.y = 0f;
        }
        p.z = Input.GetAxis("Vertical") * Time.deltaTime * currentSpeed;

        transform.Translate(p.x, p.y, p.z);

        if (homePoint != null && Input.GetKeyDown(KeyCode.Home)) {
            transform.position = homePoint.position;
            transform.rotation = homePoint.rotation;
            transform.localScale = homePoint.localScale;
        }
    }

    // ~ ~ ~ ~ ~ ~ ~ ~ 

    public enum RotationAxes { MouseXAndY, MouseX, MouseY, NONE };
    [Header("Mouse")]
    public bool useMouse = true;
    public bool showCursor = false;
    public bool useButton = true;
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 2f;
    public float sensitivityY = 2f;

    [HideInInspector] public Vector3 mousePos = Vector3.zero;

    [HideInInspector] public bool mouseDown = false;
    [HideInInspector] public bool mousePressed = false;
    [HideInInspector] public bool mouseUp = false;

    [HideInInspector] public bool mouseRDown = false;
    [HideInInspector] public bool mouseRPressed = false;
    [HideInInspector] public bool mouseRUp = false;

    [HideInInspector] public bool mouseMDown = false;
    [HideInInspector] public bool mouseMPressed = false;
    [HideInInspector] public bool mouseMUp = false;

    [HideInInspector] public int touchCount = 0;

    private bool fixedZ = false;
    //private float minimumX = -360f;
    //private float maximumX = 360f;
    private float minimumY = -60f;
    private float maximumY = 60f;
    private float zPos = 1f;
    private float rotationY = 0f;

    private void mouseStart() {
        if (GetComponent<Rigidbody>()) GetComponent<Rigidbody>().freezeRotation = true;
    }

    private void mouseUpdate() {
        if (axes == RotationAxes.MouseXAndY) {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0f);
        } else if (axes == RotationAxes.MouseX) {
            transform.Rotate(0f, Input.GetAxis("Mouse X") * sensitivityX, 0f);
        } else if (axes == RotationAxes.MouseY) {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0f);
        }

        // ~ ~ ~

        mouseDown = false;
        mouseUp = false;
        mouseRDown = false;
        mouseRUp = false;
        mouseMDown = false;
        mouseMUp = false;

        if (useButton && GUIUtility.hotControl == 0) {
            if (Input.GetMouseButtonDown(0)) {
                mouseDown = true;
                mousePressed = true;
            } else if (Input.GetMouseButtonUp(0)) {
                mousePressed = false;
                mouseUp = true;
            }

            if (isMobile) {
                touchCount = Input.touchCount;

                if (touchCount == 2 && Input.GetTouch(1).phase == TouchPhase.Began) {
                    mouseRDown = true;
                    mouseRPressed = true;
                } else if (touchCount == 2 && Input.GetTouch(1).phase == TouchPhase.Ended) {
                    mouseRPressed = false;
                    mouseRUp = true;
                }

                if (touchCount >= 3 && Input.GetTouch(2).phase == TouchPhase.Began) {
                    mouseMDown = true;
                    mouseMPressed = true;
                } else if (touchCount >= 3 && Input.GetTouch(2).phase == TouchPhase.Ended) {
                    mouseMPressed = false;
                    mouseMUp = true;
                }
            } else {
                if (Input.GetMouseButtonDown(1)) {
                    mouseRDown = true;
                    mouseRPressed = true;
                } else if (Input.GetMouseButtonUp(1)) {
                    mouseRPressed = false;
                    mouseRUp = true;
                }

                if (Input.GetMouseButtonDown(2)) {
                    mouseMDown = true;
                    mouseMPressed = true;
                } else if (Input.GetMouseButtonUp(2)) {
                    mouseMPressed = false;
                    mouseMUp = true;
                }
            }

            if ((mousePressed || mouseRPressed || mouseMPressed)) {
                if (isMobile && touchCount > 1) {
                    Vector2 avgPos = Vector2.zero;

                    for (int i = 0; i < touchCount; i++) {
                        avgPos += Input.GetTouch(i).position;
                    }

                    avgPos /= (float)touchCount;

                    mousePos = Camera.main.ScreenToWorldPoint(new Vector3(avgPos.x, avgPos.y, zPos));
                } else {
                    mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zPos));
                }
            }
        }
    }

    // ~ ~ ~ ~ ~ ~ ~ ~ 

    [Header("Raycaster")]
    public bool useRaycaster = true;
    public bool followMouse = true;

    [HideInInspector] public bool isLooking = false;
    [HideInInspector] public string isLookingAt = "";
    [HideInInspector] public Collider isLookingCol;
    [HideInInspector] public Vector3 lastHitPos = Vector3.one;

    void rayUpdate() {
        RaycastHit hit;
        Ray ray;

        if (followMouse) {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        } else {
            ray = new Ray(transform.position, transform.forward);
        }

        if (Physics.Raycast(ray, out hit)) {
            isLooking = true;
            isLookingAt = hit.collider.name;
            isLookingCol = hit.collider;

            lastHitPos = hit.point;
        } else {
            isLooking = false;
            isLookingAt = "";
        }
    }

}
