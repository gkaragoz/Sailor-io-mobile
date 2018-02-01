using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RandomColor : MonoBehaviour {

    /*/* RED          #F34235
     * PINK         #E81D62
     * DARK GRAY    #51585D
     * PURPLE       #9B26AF
     * DEEP PURPLE  #6639B6
     * INDIGO       #3E50B4
     * BLUE         #2095F2
     * LIGHT BLUE   #02A8F3
     * CYAN         #00BBD3
     * TEAL         #009587
     * GREEN        #4BAE4F
     * LIGHT GREEN  #8AC249
     * LIME         #CCDB38
     * YELLOW       #FEEA3A
     * AMBER        #FEC006
     * ORANGE       #FE9700
     * DEEP ORANGE  #FE5621
     * BROWN        #785447
     * GREY         #9D9D9D
     * BLUE GREY    #5F7C8A
     * */

    public Color[] Colors;

    private Renderer _renderer;

    private void Start() {
        _renderer = GetComponent<Renderer>();

        if (_renderer == null)
            return;

        var tempMaterial = new Material(_renderer.sharedMaterial);
        tempMaterial.color = Colors[Random.Range(0, Colors.Length)];
        _renderer.sharedMaterial = tempMaterial;
    }

}
