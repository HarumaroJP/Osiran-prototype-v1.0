using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityGUIManager : MonoBehaviour {

    [SerializeField] private Font misakiGothicFont;


    void Start() {
        Initialize();
    }


    private void Initialize() {
        misakiGothicFont.material.mainTexture.filterMode = FilterMode.Point;
    }

}
