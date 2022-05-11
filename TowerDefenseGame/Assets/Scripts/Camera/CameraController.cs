
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //private bool doMovement = true;
    
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;

    // Update is called once per frame
    void Update()
    {
     //when camera movement based off mouse input works add this peace of code
     //so that the camera does not go crazy

        /* if (Input.GetKeyDown(KeyCode.Escape))
            doMovement = !doMovement;
        if (!doMovement)
            return;
        */
       //************************************************************************************* 
        //moving camera 
        if (Input.GetKey("a")) //|| Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d")) //|| Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("w")) //|| Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s")) //|| Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        //moving with the camera doesnt work with the mouse input
        //if you go to the left it moves to the right and visa versa

        //*****************************************************************
        
        //scroll view
        float scroll = Input.GetAxis("Mouse ScrollWheel");
       // Debug.Log(scroll);

        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    
    } 
}








/*if (Input.GetKey("w") || Input.mousePosition.y >= Screen.width - panBorderThickness)
{
    transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
}
if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
{
    transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
}
if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
{
    transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
}
if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
{
    transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
}
// does everything in the complete opposite
*/

//original code ^