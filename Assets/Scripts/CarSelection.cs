using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarSelection : MonoBehaviour
{
    [SerializeField]
    private Car currentlySelectedCar = new();
    [SerializeField]
    private List<Car> cars;
    [SerializeField]
    private TextMeshProUGUI brakeHorsepowerText, zeroToSixtyInSecondsText, priceText;
    private float currentBrakeHorsepower, currentZeroToSixtyInSeconds, currentPrice;
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

    private void DisplayCarInfo(Car car)
    {
        brakeHorsepowerText.text = currentBrakeHorsepower.ToString();
        zeroToSixtyInSecondsText.text = currentZeroToSixtyInSeconds.ToString();
        priceText.text = currentPrice.ToString();
    }
}
