using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemManager : MonoBehaviour
{

    public Text gemText;
    public int gemCount;
    public GameObject door;
    private bool doorDestroyed;

    void Start(){

    }

    void Update(){
        gemText.text = gemCount.ToString();


        if(gemCount == 2  && !doorDestroyed){
            doorDestroyed = true;
            Destroy(door);
        }
    }
}
