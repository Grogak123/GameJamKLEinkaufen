using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatermelonTrigger : MonoBehaviour
{
    private bool triggered = false;

    public List<GameObject> rbObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(!triggered && other.CompareTag("Player"))
        {
            triggered = true;

            for (int i = 0; i < rbObjects.Count; i++)
            {
                Rigidbody rb = rbObjects[i].AddComponent<Rigidbody>();

                rb.AddForce(new Vector3(Random.Range(0, 3), .5f, 0), ForceMode.Impulse);
            }
            StartCoroutine(wait());
           
        }
    }


    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(5);
        Delete();

    }

    void Delete()
    {
        for (int i = 0; i < rbObjects.Count; i++)
        {
            Destroy(rbObjects[i]);
        }
    }
}
