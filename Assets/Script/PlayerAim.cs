using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using static VConvert.Vector3Extension;

public class PlayerAim : MonoBehaviour
{
    private PlayerControls playerControls;

    // Start is called before the first frame update
    private void Awake() {
        // PlayerControls is the name of the set of control you set 
        playerControls = new PlayerControls();
        playerControls.Player.Enable();
    }

    // When playing with rotation, SCALE OF PARENT MUST BE AT 1,1,1 https://discussions.unity.com/t/stretching-model-when-rotating/847320/2
    private void FixedUpdate() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(playerControls.Player.Aim.ReadValue<Vector2>());
        mousePosition.z = 0;
        Debug.Log("Mouse Vector: " + mousePosition);
        
        // normalized to keep it smooth
        Vector3 rotation = (mousePosition - transform.position).normalized;
        rotation.z = 0;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
}
