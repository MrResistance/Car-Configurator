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
        carNames.Clear();
        foreach (Car car in cars)
        {
            carNames.Add(car.modelPrefab.name);
        }
        selectCarDropdown.ClearOptions();
        selectCarDropdown.AddOptions(carNames);
    }
    public void ChooseNewCar()
    {
        HideCarModel(carsListIndex);
        carsListIndex = selectCarDropdown.value;
        currentlySelectedCar = cars[carsListIndex];
        DisplayCarInfo(currentlySelectedCar);
        DisplayCarModel(carsListIndex);
    }
    public void NextCar(bool value)
    {
        ChangeCarModelColour("#FFFFFF");
        HideCarModel(carsListIndex);
        int nextIndex;
        if (value == true)
        {
            nextIndex = (carsListIndex + 1) % cars.Count;
        }
        else
        {
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
        priceText.text = "£" + car.basePrice.ToString("n0");
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
        float currentPrice = cars[carsListIndex].basePrice;
        if (customColor)
        {
            currentPrice *= 1.2f;
            priceText.text = "£" + currentPrice.ToString("n0");
        }
        else
        {
            priceText.text = "£" + currentPrice.ToString("n0");
        }
    }
}
