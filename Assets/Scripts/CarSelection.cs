using System.Collections.Generic;
using UnityEngine;

public class CarSelection : MonoBehaviour
{
    [SerializeField]
    private Car currentlySelectedCar = new();
    [SerializeField]
    private List<Car> cars;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public Car chooseNewCar()
    //{
    //    Car newCar;

    //    return newCar;
    //}
    //public Car nextCar()
    //{
        
    //}
    //public Car previousCar() 
    //{
        
    //}
}
