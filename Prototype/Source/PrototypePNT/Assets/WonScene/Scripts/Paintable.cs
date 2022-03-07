using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintable : MonoBehaviour
{
    public GameObject Brush;
    public float BrushSize = 0.1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        if (Input.GetMouseButton(0))
        {
            var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(Ray, out hit))
            {
                var go = Instantiate(Brush, hit.point+Vector3.up * 0.1f, Quaternion.Euler(270f,180f,180f));
                go.transform.localScale = Vector3.one * BrushSize;
            }
        }

    }
}
