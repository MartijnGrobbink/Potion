using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : MonoBehaviour
{
    public float throwPower = 5;
    public float rotationSpeed = 10;

    public GameObject Potion;
    public Transform ThrowPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float HorizontalRotation= Input.GetAxis("Mouse X");
        float VerticalRotation = Input.GetAxis("Mouse Y");
        
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
        new Vector3(0, HorizontalRotation * rotationSpeed, VerticalRotation * rotationSpeed));

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject CreatedPotion = Instantiate(Potion, ThrowPoint.position, ThrowPoint.rotation);
            CreatedPotion.GetComponent<Rigidbody>().velocity = ThrowPoint.transform.up * throwPower;
        }
    }
}
