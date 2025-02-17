using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    public Keyboard keyboard;
    public int speed = 10;
    // Start is called before the first frame update
    void Start() {
        keyboard = Keyboard.current;
    }

    // Update is called once per frame
    void Update() {
        if (keyboard == null) {
            Debug.Log("Keyboard not found");
        } 

        if (keyboard.aKey.isPressed) {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        } else if (keyboard.dKey.isPressed) {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        } 
        if (keyboard.wKey.isPressed) {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
    }
}
