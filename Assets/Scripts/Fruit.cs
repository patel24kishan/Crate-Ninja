using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject wholeFruit;
    public GameObject slicedFruit;

    private Rigidbody fruitRigidBody;
    private Collider fruitCollider;

    private GameManager gameManager;


    private void Awake()
    {
        fruitCollider= GetComponent<Collider>();
        fruitRigidBody= GetComponent<Rigidbody>();
        gameManager= FindObjectOfType<GameManager>();
    }
    private void Sliced(Vector3 direction, Vector3 position,float force)
    {
        wholeFruit.SetActive(false);
        slicedFruit.SetActive(true);

        gameManager.UpdateScore();

        float angle=Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;
        slicedFruit.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices=slicedFruit.GetComponentsInChildren<Rigidbody>(); 

        foreach(Rigidbody slice in slices){
            slice.velocity=fruitRigidBody.velocity;
            slice.AddForceAtPosition(direction*force,position,ForceMode.Impulse);
        }

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { 

            Blade blade= other.GetComponent<Blade>();
            Sliced(blade.direction, blade.transform.position, blade.slideForce);
        }
       
    }
}
