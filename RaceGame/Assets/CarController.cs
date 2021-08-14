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
    public Axel axel; //Enum
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
        turn();
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

    void turn()
    {
        foreach (var wheel in wheels) //wheels referans deðerinin içersindekileri tek tek wheel içersine çekerek iþlem yaptýk yani tüm tekerlere
        {
            if(wheel.axel == Axel.Front) //Bu kullanýma dikkat sadece ön tekerlere iþlem yapmak için enum kullandýk
            {
                var _steerAngle = inputX * RotationSensitive * MaxAngleRotation; //input x basýlma süresine baðlý kalarak 0.0 ve 1.0 arasýnda deðerler üretir dolayýsýyla burada d'ye her basldýðýnda max 45 olacak bir açý deðeri çýkacak elimizi çektiðimiz taktirde ise tekrar 0 deðeri alacaðýz.
                    
                wheel.collider.steerAngle = Mathf.Lerp(wheel.collider.steerAngle, _steerAngle, .1f); //Lerp komutu sayesinde Belirtilen noktalar arasýndaki geçiþin yumuþak olmasýný saðlýyor

            }
        }
    }

}



