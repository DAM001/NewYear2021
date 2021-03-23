using UnityEngine;

public class CursorScript : MonoBehaviour
{
    private GameObject _MainCamera;
    private GameObject _Player;


    private void Start() //start function | set variables
    {
        _MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update() //set cursor position
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        if (_Player != null)
        {
            transform.position = MouseCursorPosition(); //pc mouse control

            transform.GetChild(0).transform.rotation = _MainCamera.transform.rotation;
        }
    }

    private Vector3 MouseCursorPosition() //put aim control (mouse)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100, ~30 << 31)) return hit.point;
        else return Vector3.zero;
    }

    public void CursorState(bool state) //cursor state (enable/disable)
    {
        Cursor.visible = state;
    }
}