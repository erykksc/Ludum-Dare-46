using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activatable : MonoBehaviour
{
    [SerializeField]
    private bool is_logic_enabled;
    public bool active;
    [SerializeField, Tooltip("Logic input to use if logic is enabled")]
    private logicGate logicGate_input;

    public void activate() {
        if (is_logic_enabled) {
            active = true;
        }
    }
    public void de_activate() {
        if (is_logic_enabled) {
            active = false;
        }
    }
    void Start() {
        if (is_logic_enabled) {
            active = false;
            logicGate_input.openFn = activate;
            logicGate_input.closeFn = de_activate;
        }
        else {
            active = true;
        }
    }
}
