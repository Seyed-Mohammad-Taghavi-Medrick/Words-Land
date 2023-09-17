using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvasPos;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetUpSetting()
    {
        transform.position = Vector3.Lerp(transform.position, mainCanvasPos.transform.position, Time.deltaTime * 75f);
    }
}