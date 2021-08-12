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
    public Axel axel;
}

public class CarController : MonoBehaviour
{
    [SerializeField] float MaximumAcceleration = 200f ;

    [SerializeField] float RotationSensitive;

    [SerializeField] float MaxAngleRotation;

    [SerializeField] private List<Wheel> wheels; //B�t�n eklemi� oldu�umuz wheel yap�lar�n� alabiliriz bu sayede //Wheel yap�s�ndan wheels referans de�eri olu�turduk

    [SerializeField] float inputX, inputY;

    private void Update()
    {
        directionOfMove();
    }

    private void LateUpdate()  
    {
        move(); //daha s�k tazelendi�i i�in buradan gelen hareketleri yani inputx ve inputy'yi kullanarak buradaki move fonksiyonunu �ekillendirece�iz
    }

    void move()
    {
        foreach(var wheel in wheels) //wheels referans de�erinin i�ersindekileri tek tek wheel i�ersine �ekerek i�lem yapt�k yani t�m tekerlere
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



