using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CreateName : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        int index = name.IndexOf("-");
        string Name = name.Insert(index, "-0");
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).name = Name.Replace("-0", "-" + (i + 1).ToString("D2"));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
