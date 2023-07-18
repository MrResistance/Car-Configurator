using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using ColorUtility = UnityEngine.ColorUtility;

public class CarSelection : MonoBehaviour
{
    [SerializeField]
    private Car currentlySelectedCar = new();
    [SerializeField]
    private List<Car> cars;
    [SerializeField]
    private TextMeshProUGUI brakeHorsepowerText, zeroToSixtyInSecondsText, priceText;
    [SerializeField]
    private Transform carModelsParent;
    [SerializeField]
    private TMP_Dropdown selectCar;
    private List<string> carNames = new();
    private int carsListIndex;
    private bool customColor = false;
    // Start is called before the first frame update
    void Start()
    {
        currentlySelectedCar = cars[0];
        carsListIndex = 0;
        InitialiseCarModels();
        DisplayCarInfo(currentlySelectedCar);
        DisplayCarModel(carsListIndex);
        CreateOptionData();
    }
    private void CreateOptionData()
    {
        carNames.Clear();
        foreach (Car car in cars)
        {
            carNames.Add(car.modelPrefab.name);
        }
        selectCar.ClearOptions();
        selectCar.AddOptions(carNames);
    }
    public void ChooseNewCar()
    {
        HideCarModel(carsListIndex);
        carsListIndex = selectCar.value;
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
        selectCar.SetValueWithoutNotify(carsListIndex);
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
