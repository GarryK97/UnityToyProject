using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBarControl : MonoBehaviour
{   
    Slider hpSlider;

    // Start is called before the first frame update
    void Start()
    {
        hpSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hpSlider.value < 1)
        {
            transform.Find("Fill Area").gameObject.SetActive(false);
        }
        else 
        {
            transform.Find("Fill Area").gameObject.SetActive(true);
        }
    }
}
