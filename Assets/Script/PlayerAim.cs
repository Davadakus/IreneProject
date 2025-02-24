using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using static VConvert.Vector3Extension;

public class PlayerAim : MonoBehaviour
{
    private PlayerControls playerControls;
    private Vector3 mousePosition;
    // Start is called before the first frame update
    private void Awake() {
        // PlayerControls is the name of the set of control you set 
        playerControls = new PlayerControls();
        playerControls.Player.Enable();

    }

        void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position);

        
    //}

    private void FixedUpdate() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(playerControls.Player.Aim.ReadValue<Vector2>());
        //mousePosition.z = 0;
        Debug.Log("Mouse Vector: " + mousePosition);
        
        Vector3 rotation = mousePosition - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
    public Vector2 AsVector2(Vector3 _v) {
        return new Vector2(_v.x, _v.y);
    }


}
