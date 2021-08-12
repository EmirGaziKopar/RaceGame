using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum Axel
{
    Front,Rear
}

[Serializable]
public struct Wheel //Wheel adýnda bir yapý tanýmladýk
{
    public GameObject model;
    public WheelCollider collider;
    public Axel axel;
}

public class CarController : MonoBehaviour
{
    [SerializeField] float MaximumAcceleration = 200f ;

    [SerializeField] float RotationSensitive;

    [SerializeField] float MaxAngleRotation;

    [SerializeField] private List<Wheel> wheels; //Bütün eklemiþ olduðumuz wheel yapýlarýný alabiliriz bu sayede //Wheel yapýsýndan wheels referans deðeri oluþturduk

    [SerializeField] float inputX, inputY;

    private void Update()
    {
        directionOfMove();
    }

    private void LateUpdate()  
    {
        move(); //daha sýk tazelendiði için buradan gelen hareketleri yani inputx ve inputy'yi kullanarak buradaki move fonksiyonunu þekillendireceðiz
    }

    void move()
    {
        foreach(var wheel in wheels) //wheels referans deðerinin içersindekileri tek tek wheel içersine çekerek iþlem yaptýk yani tüm tekerlere
        {
            wheel.collider.motorTorque = inputY * MaximumAcceleration * 500 * Time.deltaTime;
        }
    }

    void directionOfMove()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");


    } 

}



