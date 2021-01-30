using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreadMeter : MonoBehaviour {

    public float dreadGrowth;
    public Text display;

    private float dreadValue;

    // Start is called before the first frame update
    void Start() {
        float dread = 0f;
    }


    // Update is called once per frame
    void Update() {
        ModifyValue(Time.deltaTime * dreadGrowth);
        if (display) {
            display.text = Mathf.RoundToInt(dreadValue).ToString();
        }
    }


    public void ModifyValue(float mod) {
        dreadValue = Mathf.Clamp(dreadValue + mod, 0f, 100f);
    }
}
