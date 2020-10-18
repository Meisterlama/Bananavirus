using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;
using Slider = UnityEngine.UI.Slider;

internal struct TopPanelInfo
{
    public TextMeshProUGUI Money;
}

internal struct GenericPanelInfo
{
    public TextMeshProUGUI Name;
}

internal struct FactoryPanelInfo
{
    public TextMeshProUGUI Aspinium;
    public TextMeshProUGUI Dolinium;
    public TMP_Dropdown RecipeSelect;
    public TextMeshProUGUI SelectedRecipeText;
    public Slider SelectedRecipeSlider;
    public TMP_Dropdown CitySelect;
    public TextMeshProUGUI Total;
    public Selectable CommandButton;
    public Slider ProductionSlider;
}

internal struct SupplierPanelInfo
{
    public TextMeshProUGUI Aspinium;
    public Slider AspiniumSlider;
    public TextMeshProUGUI Dolinium;
    public Slider DoliniumSlider;
    public TMP_Dropdown FactorySelect;
    public TextMeshProUGUI Total;
    public Selectable CommandButton;
    public Slider ProductionSlider;
}

internal struct CityPanelInfo
{
    public TextMeshProUGUI HabitantInfo;
    public TextMeshProUGUI Aspidos;
    public TextMeshProUGUI Dolifront;
}

public class UIScript : MonoBehaviour
{
    // Top panel related variables
    private GameObject m_TopPanel;
    private TopPanelInfo m_TopPanelInfo;

    // Generic panel related variables
    private GameObject m_GenericPanel;
    private GenericPanelInfo m_GenericPanelInfo;

    // Factory related variables
    private Canvas m_FactoryPanel;
    private FactoryPanelInfo m_FactoryPanelInfo;
    private Factory m_CurrentFactory = null;

    // Supplier related variables
    private Canvas m_SupplierPanel;
    private SupplierPanelInfo m_SupplierPanelInfo;
    private Supplier m_CurrentSupplier = null;

    // City related variables
    private Canvas m_CityPanel;
    private CityPanelInfo m_CityPanelInfo;
    private City m_CurrentCity = null;

    private GameManager m_GameManager;

    private void Start()
    {
        m_GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_TopPanel = transform.Find("TopPanel").gameObject;
        m_TopPanelInfo.Money = m_TopPanel.transform.Find("Money").gameObject.GetComponent<TextMeshProUGUI>();

        m_GenericPanel = transform.Find("GenericPanel").gameObject;
        m_GenericPanelInfo.Name = m_GenericPanel.transform.Find("Name").GetComponent<TextMeshProUGUI>();

        m_FactoryPanel = transform.Find("FactoryPanel").GetComponent<Canvas>();
        m_FactoryPanelInfo.Aspinium = m_FactoryPanel.transform.Find("AspiniumPanel").Find("Aspinium")
            .GetComponent<TextMeshProUGUI>();
        m_FactoryPanelInfo.Dolinium = m_FactoryPanel.transform.Find("DoliniumPanel").Find("Dolinium")
            .GetComponent<TextMeshProUGUI>();
        m_FactoryPanelInfo.RecipeSelect = m_FactoryPanel.transform.Find("RecipeSelect").GetComponent<TMP_Dropdown>();
        m_FactoryPanelInfo.SelectedRecipeText = m_FactoryPanel.transform.Find("SelectedRecipePanel")
            .Find("SelectedRecipeText").GetComponent<TextMeshProUGUI>();
        m_FactoryPanelInfo.SelectedRecipeSlider = m_FactoryPanel.transform.Find("SelectedRecipePanel")
            .Find("SelectedRecipeSlider").GetComponent<Slider>();
        m_FactoryPanelInfo.CitySelect = m_FactoryPanel.transform.Find("CitySelect").GetComponent<TMP_Dropdown>();
        m_FactoryPanelInfo.Total = m_FactoryPanel.transform.Find("Total").GetComponent<TextMeshProUGUI>();
        m_FactoryPanelInfo.CommandButton = m_FactoryPanel.transform.Find("CommandButton").GetComponent<Selectable>();
        m_FactoryPanelInfo.ProductionSlider = m_FactoryPanel.transform.Find("ProductionSlider").GetComponent<Slider>();


        m_SupplierPanel = transform.Find("SupplierPanel").GetComponent<Canvas>();
        m_SupplierPanelInfo.Aspinium = m_SupplierPanel.transform.Find("AspiniumPanel").Find("AspiniumText")
            .GetComponent<TextMeshProUGUI>();
        m_SupplierPanelInfo.AspiniumSlider = m_SupplierPanel.transform.Find("AspiniumPanel").Find("AspiniumSlider")
            .GetComponent<Slider>();
        m_SupplierPanelInfo.Dolinium = m_SupplierPanel.transform.Find("DoliniumPanel").Find("DoliniumText")
            .GetComponent<TextMeshProUGUI>();
        m_SupplierPanelInfo.DoliniumSlider = m_SupplierPanel.transform.Find("DoliniumPanel").Find("DoliniumSlider")
            .GetComponent<Slider>();
        m_SupplierPanelInfo.Total = m_SupplierPanel.transform.Find("Total").GetComponent<TextMeshProUGUI>();
        m_SupplierPanelInfo.CommandButton = m_SupplierPanel.transform.Find("CommandButton").GetComponent<Selectable>();
        m_SupplierPanelInfo.ProductionSlider =
            m_SupplierPanel.transform.Find("ProductionSlider").GetComponent<Slider>();
        m_SupplierPanelInfo.FactorySelect =
            m_SupplierPanel.transform.Find("FactorySelect").GetComponent<TMP_Dropdown>();

        List<Factory> factories = m_GameManager.factories;
        List<string> factoriesName = new List<string>();
        foreach (var factory in factories)
        {
            factoriesName.Add(factory.name);
        }

        m_SupplierPanelInfo.FactorySelect.AddOptions(factoriesName);

        List<City> cities = m_GameManager.cities;
        List<string> citiesName = new List<string>();
        foreach (var city in cities)
        {
            citiesName.Add(city.name);
        }

        m_FactoryPanelInfo.CitySelect.AddOptions(citiesName);


        m_CityPanel = transform.Find("CityPanel").GetComponent<Canvas>();
        m_CityPanelInfo.HabitantInfo = m_CityPanel.transform.Find("HabitantPanel").Find("HabitantInfo")
            .GetComponent<TextMeshProUGUI>();
        m_CityPanelInfo.Aspidos =
            m_CityPanel.transform.Find("AspidosPanel").Find("Aspidos").GetComponent<TextMeshProUGUI>();
        m_CityPanelInfo.Dolifront = m_CityPanel.transform.Find("DolifrontPanel").Find("Dolifront")
            .GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        m_TopPanelInfo.Money.text = $"Current Banana Cash : {m_GameManager.currentMoney}";
        if (m_CurrentFactory)
        {
            UpdateFactoryPanel();
        }

        if (m_CurrentSupplier)
        {
            UpdateSupplierPanel();
        }

        if (m_CurrentCity)
        {
            UpdateCityPanel();
        }
    }

    public void ActivateFactoryPanel(Factory factory)
    {
        DeactivatePanels();
        m_GenericPanel.gameObject.SetActive(true);
        m_FactoryPanel.gameObject.SetActive(true);
        m_CurrentFactory = factory;
        UpdateFactoryPanel();
    }

    private void UpdateFactoryPanel()
    {
        if (m_CurrentFactory.CurrentCommand.isDone)
        {
            m_FactoryPanelInfo.CommandButton.transform.gameObject.SetActive(true);
            m_FactoryPanelInfo.ProductionSlider.transform.gameObject.SetActive(false);
        }
        else
        {
            m_FactoryPanelInfo.CommandButton.transform.gameObject.SetActive(false);
            m_FactoryPanelInfo.ProductionSlider.transform.gameObject.SetActive(true);
            m_FactoryPanelInfo.ProductionSlider.value = m_CurrentFactory.CurrentCommand.ElapsedTime /
                                                        m_CurrentFactory.CurrentCommand.TimeToProduce;
        }

        m_GenericPanelInfo.Name.text = m_CurrentFactory.name;
        m_FactoryPanelInfo.Aspinium.text = $"{m_CurrentFactory.FactoryInventory.Aspinium.type.ToString()}\n" +
                                           $"{m_CurrentFactory.FactoryInventory.Aspinium.quantity}";
        m_FactoryPanelInfo.Dolinium.text = $"{m_CurrentFactory.FactoryInventory.Dolinium.type.ToString()}\n" +
                                           $"{m_CurrentFactory.FactoryInventory.Dolinium.quantity}";

        GameRecipe currentRecipe = m_FactoryPanelInfo.RecipeSelect.value == 0
            ? m_CurrentFactory.aspidosRecipe
            : m_CurrentFactory.dolifrontRecipe;
        GameResource currentResource = m_FactoryPanelInfo.RecipeSelect.value == 0
            ? m_CurrentFactory.FactoryInventory.Aspidos
            : m_CurrentFactory.FactoryInventory.Dolifront;
        
        m_FactoryPanelInfo.SelectedRecipeSlider.maxValue = m_CurrentFactory.MaxProductionQuantity(currentRecipe);

        m_FactoryPanelInfo.SelectedRecipeText.text =
            $"Max : {m_CurrentFactory.MaxProductionQuantity(currentRecipe)}\n" +
            $"Price : {currentResource.price}$\n" +
            $"Time : {currentResource.timeToProduce}s";
        UpdateFactoryCommandPrice();
    }

    public void UpdateFactoryCommandPrice()
    {
        GameRecipe currentRecipe = m_FactoryPanelInfo.RecipeSelect.value == 0
            ? m_CurrentFactory.aspidosRecipe
            : m_CurrentFactory.dolifrontRecipe;
        GameResource currentResource = m_FactoryPanelInfo.RecipeSelect.value == 0
            ? m_CurrentFactory.FactoryInventory.Aspidos
            : m_CurrentFactory.FactoryInventory.Dolifront;
        float recipeCount = m_FactoryPanelInfo.SelectedRecipeSlider.value;
        
        m_FactoryPanelInfo.Total.text =
            $"Total {currentResource.type.ToString()}: {recipeCount}\n" +
            $"Total Price : {(recipeCount * (currentResource.price)):F2}$\n" +
            $"Total Time : {(recipeCount * (currentResource.timeToProduce)):F2}s\n";
    }

    public void MakeFactoryCommand()
    {
        GameRecipe currentRecipe = m_FactoryPanelInfo.RecipeSelect.value == 0
            ? m_CurrentFactory.aspidosRecipe
            : m_CurrentFactory.dolifrontRecipe;
        GameResource currentResource = m_FactoryPanelInfo.RecipeSelect.value == 0
            ? m_CurrentFactory.FactoryInventory.Aspidos
            : m_CurrentFactory.FactoryInventory.Dolifront;
        int recipeCount = (int) m_FactoryPanelInfo.SelectedRecipeSlider.value;

        m_CurrentFactory.FactoryInventory.Aspinium.quantity -= (int)currentRecipe.aspiniumQuantity * recipeCount;
        m_CurrentFactory.FactoryInventory.Dolinium.quantity -= (int)currentRecipe.doliniumQuantity * recipeCount;
        
        List<City> cities = m_GameManager.cities;
        m_CurrentFactory.MakeCommand(cities[m_FactoryPanelInfo.CitySelect.value], currentResource.type, recipeCount);
        m_FactoryPanelInfo.SelectedRecipeSlider.value = 0;
    }

    public void ActivateSupplierPanel(Supplier supplier)
    {
        DeactivatePanels();
        m_GenericPanel.gameObject.SetActive(true);
        m_SupplierPanel.gameObject.SetActive(true);
        m_CurrentSupplier = supplier;
        m_SupplierPanelInfo.AspiniumSlider.value = 0;
        m_SupplierPanelInfo.DoliniumSlider.value = 0;
        UpdateSupplierPanel();
    }

    private void UpdateSupplierPanel()
    {
        if (m_CurrentSupplier.CurrentCommand.isDone)
        {
            m_SupplierPanelInfo.CommandButton.transform.gameObject.SetActive(true);
            m_SupplierPanelInfo.ProductionSlider.transform.gameObject.SetActive(false);
        }
        else
        {
            m_SupplierPanelInfo.CommandButton.transform.gameObject.SetActive(false);
            m_SupplierPanelInfo.ProductionSlider.transform.gameObject.SetActive(true);
            m_SupplierPanelInfo.ProductionSlider.value = m_CurrentSupplier.CurrentCommand.ElapsedTime /
                                                         m_CurrentSupplier.CurrentCommand.TimeToProduce;
        }

        m_GenericPanelInfo.Name.text = m_CurrentSupplier.name;
        m_SupplierPanelInfo.AspiniumSlider.maxValue = m_CurrentSupplier.aspinium.quantity;
        m_SupplierPanelInfo.Aspinium.text =
            $"{m_CurrentSupplier.aspinium.type.ToString()}\n" +
            $"{m_CurrentSupplier.aspinium.price:F2}$\n" +
            $"{m_CurrentSupplier.aspinium.timeToProduce:F2}sec";

        m_SupplierPanelInfo.DoliniumSlider.maxValue = m_CurrentSupplier.dolinium.quantity;
        m_SupplierPanelInfo.Dolinium.text =
            $"{m_CurrentSupplier.dolinium.type.ToString()}\n" +
            $"{m_CurrentSupplier.dolinium.price:F2}$\n" +
            $"{m_CurrentSupplier.dolinium.timeToProduce:F2}sec";

        UpdateSupplierCommandPrice();
    }

    public void UpdateSupplierCommandPrice()
    {
        float aspiniumCount = m_SupplierPanelInfo.AspiniumSlider.value;
        float doliniumCount = m_SupplierPanelInfo.DoliniumSlider.value;
        float totalPrice = (aspiniumCount * (m_CurrentSupplier.aspinium.price) +
                            doliniumCount * (m_CurrentSupplier.dolinium.price));
        if (totalPrice < m_GameManager.currentMoney)
        {
            m_SupplierPanelInfo.Total.text =
                $"Total Aspinium: {aspiniumCount}\n" +
                $"Total Dolinium: {doliniumCount}\n" +
                $"Total Price : {totalPrice:F2}$\n" +
                $"Total Time : {Mathf.Max(aspiniumCount * (m_CurrentSupplier.aspinium.timeToProduce), doliniumCount * (m_CurrentSupplier.dolinium.timeToProduce)):F2}s\n";
        }
        else
        {
            m_SupplierPanelInfo.Total.text =
                $"Total Aspinium: {aspiniumCount}\n" +
                $"Total Dolinium: {doliniumCount}\n" +
                $"Total Price : <color=\"red\">{totalPrice:F2}$</color>\n" +
                $"Total Time : {Mathf.Max(aspiniumCount * (m_CurrentSupplier.aspinium.timeToProduce), doliniumCount * (m_CurrentSupplier.dolinium.timeToProduce)):F2}s\n";
        }
        
    }

    public void MakeSupplierCommand()
    {
        List<Factory> factories = m_GameManager.factories;
        int aspiniumCount = (int) m_SupplierPanelInfo.AspiniumSlider.value;
        int doliniumCount = (int) m_SupplierPanelInfo.DoliniumSlider.value;
        m_CurrentSupplier.MakeCommand(factories[m_SupplierPanelInfo.FactorySelect.value], aspiniumCount, doliniumCount);
        m_SupplierPanelInfo.AspiniumSlider.value = 0;
        m_SupplierPanelInfo.DoliniumSlider.value = 0;
    }

    public void ActivateCityPanel(City city)
    {
        DeactivatePanels();
        m_GenericPanel.gameObject.SetActive(true);
        m_CityPanel.gameObject.SetActive(true);
        m_CurrentCity = city;
        UpdateCityPanel();
    }

    private void UpdateCityPanel()
    {
        m_GenericPanelInfo.Name.text = m_CurrentCity.name;
        m_CityPanelInfo.HabitantInfo.text = $"Infected Habitants : {m_CurrentCity.shownInfectedHabitants}\n" +
                                            $"Total Habitants : {m_CurrentCity.totalHabitants}\n";

        m_CityPanelInfo.Aspidos.text = $"<align=\"center\"><i><b>Aspidos</b></i></align>\n" +
                                       $"<size=30>\n" +
                                       $"Needed:\n" +
                                       $"<align=\"right\">{m_CurrentCity.shownAspidosInfectedHabitants}</align>\n" +
                                       $"Availaible:\n" +
                                       $"<align=\"right\">{m_CurrentCity.aspidos.quantity}</align>";

        m_CityPanelInfo.Dolifront.text = $"<align=\"center\"><i><b>Dolifront</b></i></align>\n" +
                                         $"<size=30>\n" +
                                         $"Needed:\n" +
                                         $"<align=\"right\">{m_CurrentCity.shownDolifrontInfectedHabitants}</align>\n" +
                                         $"Availaible:\n" +
                                         $"<align=\"right\">{m_CurrentCity.dolifront.quantity}</align>";
    }

    public void QuitInfoPanel()
    {
        DeactivatePanels();
    }

    private void DeactivatePanels()
    {
        m_GenericPanel.gameObject.SetActive(false);
        m_FactoryPanel.gameObject.SetActive(false);
        m_CurrentFactory = null;
        m_SupplierPanel.gameObject.SetActive(false);
        m_CurrentSupplier = null;
        m_CityPanel.gameObject.SetActive(false);
        m_CurrentCity = null;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}