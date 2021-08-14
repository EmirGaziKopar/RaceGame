using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum Axel
{
    Front,Rear
}

[Serializable]
public struct Wheel //Wheel ad�nda bir yap� tan�mlad�k
{
    public GameObject model;
    public WheelCollider collider;
    public Axel axel; //Enum
}

public class CarController : MonoBehaviour
{
    [SerializeField] float MaximumAcceleration = 300f ;

    [SerializeField] float RotationSensitive;

    [SerializeField] float MaxAngleRotation;

    [SerializeField] private List<Wheel> wheels; //B�t�n eklemi� oldu�umuz wheel yap�lar�n� alabiliriz bu sayede //Wheel yap�s�ndan wheels referans de�eri olu�turduk

    [SerializeField] float inputX, inputY;

    [SerializeField] int brakePower;

    public Vector3 _centerOfMass;

    private void Awake()
    {
        GetComponent<Rigidbody>().centerOfMass = _centerOfMass;
    }
    private void Update()
    {
        brake();
        turnWheels();//tekerlerin d�nmesini sa�lar ama sa�a sola de�il kar��t�rma 
        directionOfMove();
        turn();
        
    }

    private void LateUpdate()  
    {
        move(); //daha s�k tazelendi�i i�in buradan gelen hareketleri yani inputx ve inputy'yi kullanarak buradaki move fonksiyonunu �ekillendirece�iz
    }

    void move()
    {
        
        foreach(var wheel in wheels) //wheels referans de�erinin i�ersindekileri tek tek wheel i�ersine �ekerek i�lem yapt�k yani t�m tekerlere
        {
            if(wheel.axel == Axel.Rear) //arkadan �eki�li
            {
                wheel.collider.motorTorque = inputY * MaximumAcceleration * 500 * Time.deltaTime;
            }
            
        }
    }

    void directionOfMove()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        
    } 

    void turn()
    {
        foreach (var wheel in wheels) //wheels referans de�erinin i�ersindekileri tek tek wheel i�ersine �ekerek i�lem yapt�k yani t�m tekerlere
        {
            if(wheel.axel == Axel.Front) //Bu kullan�ma dikkat sadece �n tekerlere i�lem yapmak i�in enum kulland�k
            {
                var _steerAngle = inputX * RotationSensitive * MaxAngleRotation; //input x bas�lma s�resine ba�l� kalarak 0.0 ve 1.0 aras�nda de�erler �retir dolay�s�yla burada d'ye her basld���nda max 45 olacak bir a�� de�eri ��kacak elimizi �ekti�imiz taktirde ise tekrar 0 de�eri alaca��z.
                    
                wheel.collider.steerAngle = Mathf.Lerp(wheel.collider.steerAngle, _steerAngle, .1f); //Lerp komutu sayesinde Belirtilen noktalar aras�ndaki ge�i�in yumu�ak olmas�n� sa�l�yor

            }
        }
    }


    void turnWheels()
    {
        foreach (var wheel in wheels) //wheels referans de�erinin i�ersindekileri tek tek wheel i�ersine �ekerek i�lem yapt�k yani t�m tekerlere
        {
            Quaternion _rot;
            Vector3 _pos;
            wheel.collider.GetWorldPose(out _pos, out _rot); //collider�n mevcut pozisyonunu ayarlaman�z� sa�layan bir kod        
            wheel.model.transform.position = _pos;
            wheel.model.transform.rotation = _rot;
        }
    }


    void brake()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var wheel in wheels)
            {
                wheel.collider.brakeTorque = brakePower; // brake power 
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            foreach (var wheel in wheels)
            {
                wheel.collider.brakeTorque = 0; // brake power 
            }
        }

    }
}



