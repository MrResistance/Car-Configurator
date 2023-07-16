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
    private int carsListIndex;
    // Start is called before the first frame update
    void Start()
    {
        currentlySelectedCar = cars[0];
        carsListIndex = 0;
        Debug.Log("Car: " + currentlySelectedCar.modelPrefab.name + ", BHP: " + currentlySelectedCar.brakeHorsepower + ", 0-60mph in " + currentlySelectedCar.zeroToSixtyInSeconds + " seconds, Price: £" + currentlySelectedCar.basePrice);
        DisplayCarInfo(currentlySelectedCar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void chooseNewCar()
    //{
    //    Car newCar;

    //    return newCar;
    //}
    //public void nextCar()
    //{
    //  carsListIndex++;
    //  currentlySelectedCar = cars[carsListIndex];
    //  DisplayCarInfo(currentlySelectedCar);
    //}
    //public Car previousCar() 
    //{
    //  carsListIndex--;
    //  currentlySelectedCar = cars[carsListIndex];
    //  DisplayCarInfo(currentlySelectedCar);
    //}

    private void DisplayCarInfo(Car car)
    {
        brakeHorsepowerText.text = "Brake Horsepower: " + car.brakeHorsepower.ToString();
        zeroToSixtyInSecondsText.text = "0-60mph in " + car.zeroToSixtyInSeconds.ToString() + " seconds.";
        priceText.text = "£" + car.basePrice.ToString();
    }
}
