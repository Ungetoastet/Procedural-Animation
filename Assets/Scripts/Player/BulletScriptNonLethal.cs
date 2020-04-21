using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScriptNonLethal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<MoodManager>() != null)
            collision.gameObject.GetComponent<MoodManager>().FaceUpdate(1);
    }
}
