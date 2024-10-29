using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    [SerializeField] float size = 100.5f;
    [SerializeField] bool renderMesh = false;
    private Vector3 contact;
    // Start is called before the first frame update
    void Start()
    {
        setConditions();
        this.transform.position = GetComponentInParent<Transform>().position; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setConditions()
    {
        transform.localScale = new Vector3(size, size, size);
        GetComponent<Renderer>().enabled = renderMesh;
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Here");
        contact = collider.ClosestPoint(transform.position);
    }


    public Vector3 getCollision()
    {
        return contact;
    }
}
