using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ColorUtility = UnityEngine.ColorUtility;

public class CarSelection : MonoBehaviour
{
    private Car currentlySelectedCar = new();
    [SerializeField]
    private List<Car> cars;
    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI modelName;
    [SerializeField]
    private TextMeshProUGUI brakeHorsepowerText;
    [SerializeField]
    private TextMeshProUGUI zeroToSixtyInSecondsText;
    [SerializeField]
    private TextMeshProUGUI priceText;
    [SerializeField]
    private TMP_Dropdown selectCarDropdown;
    [Header("Car Models")]
    [SerializeField]
    private Transform carModelsParent;
    private List<string> carNames = new();
    private int carsListIndex;
    private bool customColor = false;
    // Start is called before the first frame update
    void Start()
    {
        //Set the current car to the first one in the list
        currentlySelectedCar = cars[0];
        carsListIndex = 0;
        //Instantiate all the car models, but set them as inactive
        InitialiseCarModels();
        //Display the specifications in the UI of our currently selected car
        DisplayCarInfo(currentlySelectedCar);
        //Display the physical model of our currently selected car
        DisplayCarModel(carsListIndex);
        //Populate the car selection dropdown with the list of cars
        CreateOptionData();
    }
    private void CreateOptionData()
    {
        //Clear the string list first to ensure no duplication of data
        carNames.Clear();
        //We cannot directly add the car names to the dropdown so we add the list of cars to a list of strings first
        foreach (Car car in cars)
        {
            carNames.Add(car.modelPrefab.name);
        }
        //Clear the dropdown list to ensure no duplication of data
        selectCarDropdown.ClearOptions();
        //Add the string list we have created to the dropdown
        selectCarDropdown.AddOptions(carNames);
    }
    public void ChooseNewCar()
    {
        //Hide our current car
        HideCarModel(carsListIndex);
        //Set the index to the value that has been selected in the dropdown
        carsListIndex = selectCarDropdown.value;
        //Set our currently selected car to the index value we just received
        currentlySelectedCar = cars[carsListIndex];
        //Display the specifications and model of the car we selected
        DisplayCarInfo(currentlySelectedCar);
        DisplayCarModel(carsListIndex);
    }
    public void NextCar(bool value)
    {
        //Set the colour of the current car back to default before we change car
        ChangeCarModelColour("#FFFFFF");
        //Hide the current car before we change to a new one
        HideCarModel(carsListIndex);
        int nextIndex;
        //The bool parameter we receive for this function determines whether we should be going up or down the list, true for up & false for down
        if (value == true)
        {
            //If it's true, we set nextIndex to the modulo of the current index + 1 divded by the total count of cars
            //E.G. If our current index is 1 and we have 3 cars total, the function will look like this (carsListIndex(1) + 1) % 3 = 2 because 2 / 3 = 0 with a remainder of 2
            nextIndex = (carsListIndex + 1) % cars.Count;
        }
        else
        {
            //Same as above but in the other direction
            nextIndex = (carsListIndex - 1) % cars.Count;
        }
        // To ensure that the nextIndex field doesn't go outside the bounds of the array
        if (nextIndex < 0)
        {
            nextIndex = cars.Count - 1;
        }
        if (nextIndex > cars.Count)
        {
            nextIndex = 0;
        }
        carsListIndex = nextIndex;
        currentlySelectedCar = cars[carsListIndex];
        DisplayCarInfo(currentlySelectedCar);
        DisplayCarModel(carsListIndex);
        //To ensure the dropdown accurately reflects what car we are currently selected on
        selectCarDropdown.SetValueWithoutNotify(carsListIndex);
    }
    private void HideCarModel(int index)
    {
        carModelsParent.GetChild(index).gameObject.SetActive(false);
    }
    private void InitialiseCarModels()
    {
        foreach (Car car in cars)
        {
            Instantiate(car.modelPrefab, carModelsParent).SetActive(false);
        }
    }
    private void DisplayCarModel(int carIndex)
    {
        carModelsParent.GetChild(carIndex).gameObject.SetActive(true);
    }
    private void DisplayCarInfo(Car car)
    {
        modelName.text = "Model Name: " + car.modelPrefab.name.ToString();
        brakeHorsepowerText.text = "Brake Horsepower: " + car.brakeHorsepower.ToString();
        zeroToSixtyInSecondsText.text = "0-60mph in " + car.zeroToSixtyInSeconds.ToString() + " seconds.";
        priceText.text = "Price: £" + car.basePrice.ToString("n0");
    }
    public void ChangeCarModelColour(string hexColor)
    {
        if (ColorUtility.TryParseHtmlString(hexColor, out Color color))
        {
            carModelsParent.GetChild(carsListIndex).GetComponentInChildren<MeshRenderer>().material.color = color;
            if (hexColor != "#FFFFFF") customColor = true; else customColor = false;
            UpdatePrice();
        }
    }
    private void UpdatePrice()
    {
        //If the car has a custom colour, the price will be 1.2x the base price
        float currentPrice = cars[carsListIndex].basePrice;
        if (customColor)
        {
            currentPrice *= 1.2f;
            priceText.text = "Price: £" + currentPrice.ToString("n0");
        }
        else
        {
            priceText.text = "Price: £" + currentPrice.ToString("n0");
        }
    }
}
