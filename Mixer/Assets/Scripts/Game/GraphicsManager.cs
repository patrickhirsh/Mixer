using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsManager : MonoBehaviour
{
    public static bool debugMode;
    private static float ORDER_SPRITE_SPACING = .5f;

    private static GameObject playerScore;              // player's score
    private static GameObject pointsAwarded;            // object used to instantiated AnimatedText objects for points awarded
    private static GameObject currentDrinkText;         // [0] = current drink label. [1+] = current drink components
    private static GameObject currentDrinkProgress;     // the parent of all current drink progress sprites
    private static GameObject categoryLabels;           // the parent of all category labels


    public static void Initialize()
    {
        // obtain references to frequently updated GameObjects
        playerScore = GameObject.Find("score");
        pointsAwarded = GameObject.Find("points_awarded");
        currentDrinkText = GameObject.Find("current_drink_text");
        currentDrinkProgress = GameObject.Find("current_drink_progress");
        categoryLabels = GameObject.Find("category_labels");

        updateScore();
    }


    // Update is called once per frame
    void Update ()
    {
        updateBartender();
        updateCurrentDrinkProgress();
        updateCategories();
    }


    // updates the player's score score based on GameManager.playerScore
    public static void updateScore()
    {
        playerScore.GetComponent<Text>().text = GameManager.playerScore.ToString();
    }


    // spawns a +(points) string above the player score as visual feedback for the points they were awarded
    public static void spawnPointAward(int points)
    {
        // TODO: change the color based on the number of points awarded
        Color color = Color.yellow;
        color.a = .7f;
        GameObject pointsAwardedInstance = Instantiate(pointsAwarded, pointsAwarded.transform, true);
        pointsAwardedInstance.GetComponent<AnimatedText>().generateText("+" + points.ToString(), color);
    }


    // updates the bartender's position on-screen based on Bartender.position
    public static void updateBartender()
    {
        Bartender.bartender.transform.position =
            Bartender.bartenderPositions.transform.GetChild(Bartender.position).position;
    }


    // updates the current drink progress graphics (components, progress icons, etc.)
    public static void updateCurrentDrinkProgress()
    {
        // does there exist an order at the current bartending position?
        if (Bartender.bartenderPositions.transform.GetChild(Bartender.position).transform.childCount != 0)
        {
            // clear the current drink labels and icons
            for (int i = 0; i < currentDrinkText.transform.childCount; i++)
                currentDrinkText.transform.GetChild(i).GetComponent<Text>().text = "";
            for (int i = 0; i < currentDrinkProgress.transform.childCount; i++)
                currentDrinkProgress.transform.GetChild(i).gameObject.SetActive(false);

            // get a reference to the current drink
            Drink drink = Bartender.bartenderPositions.transform.GetChild(Bartender.position).GetChild(0).GetComponent<Order>().drink;

            // set the title at the top of the panel
            currentDrinkText.transform.GetChild(0).GetComponent<Text>().text = drink.drinkName;

            // render the drink components
            for (int i = 1; i <= drink.components.Count; i++)
                currentDrinkText.transform.GetChild(i).GetComponent<Text>().text = drink.components[i - 1].component;

            // render the drink progress icons
            for (int i = 0; i < OrderManager.orderProgress[Bartender.position].Count; i++)
                currentDrinkProgress.transform.GetChild(i).gameObject.SetActive(true);
        }

        // if not, clear the panel
        else
        {
            // clear the icons and labels
            for (int i = 0; i < currentDrinkText.transform.childCount; i++)
                currentDrinkText.transform.GetChild(i).GetComponent<Text>().text = "";
            for (int i = 0; i < currentDrinkProgress.transform.childCount; i++)
                currentDrinkProgress.transform.GetChild(i).gameObject.SetActive(false);
        }
    }


    // updates the category icons and labels on the left menu block
    public static void updateCategories()
    {
        // bartender isn't in a category, show category labels
        if (Bartender.menuState == bartenderMenuState.noCategorySelected)
        {
            // clear any existing labels
            for (int i = 0; i < categoryLabels.transform.childCount; i++)
                categoryLabels.transform.GetChild(i).GetComponent<Text>().text = "";

            // update categories header label
            categoryLabels.transform.GetChild(0).GetComponent<Text>().text = "CATEGORIES";

            // update labels for each category
            int index = 1;
            foreach (KeyValuePair<bartenderMenuState, string> label in DrinkComponent.categoryLabels)
            {
                categoryLabels.transform.GetChild(index).GetComponent<Text>().text = label.Value;
                index++;
            }
                
            // hide the remaining labels
            for (int i = index; i < categoryLabels.transform.childCount; i++)
                categoryLabels.transform.GetChild(index).GetComponent<Text>().text = "";
        }

        // Bartender is in a category. Display that category's contents
        else
        {
            // clear any existing labels
            for (int i = 0; i < categoryLabels.transform.childCount; i++)
                categoryLabels.transform.GetChild(i).GetComponent<Text>().text = "";

            // update the category header label
            categoryLabels.transform.GetChild(0).GetComponent<Text>().text = DrinkComponent.categoryLabels[Bartender.menuState];

            // update the category contents labels.           
            switch (Bartender.menuState)
            {
                case bartenderMenuState.glasswareCategory:
                    updateLabels(DrinkComponent.glassware);
                    break;

                case bartenderMenuState.beersCategory:
                    updateLabels(DrinkComponent.beers);
                    break;

                case bartenderMenuState.liquorsCategory:
                    updateLabels(DrinkComponent.liquors);
                    break;

                case bartenderMenuState.bittersCategory:
                    updateLabels(DrinkComponent.bitters);
                    break;

                case bartenderMenuState.nonAlcoholicCategory:
                    updateLabels(DrinkComponent.nonAlcoholic);
                    break;

                case bartenderMenuState.otherCategory:
                    updateLabels(DrinkComponent.other);
                    break;
            }
        }
    }


    /// <summary>
    /// given a source dictionary of labels for a category, populate the scene's menu with these labels
    /// </summary>
    private static void updateLabels(Dictionary<string, DrinkComponent> labelSource)
    {
        // Keep track of how many labels we populate. index = 1 because index 0 contains the category header label
        int index = 1;  

        // update labels for however many components we have
        foreach (KeyValuePair<string, DrinkComponent> component in labelSource)                    
            { categoryLabels.transform.GetChild(index).GetComponent<Text>().text = component.Key; index++; }

        // fill the rest with ""
        for (int i = index; i < categoryLabels.transform.childCount; i++)                                    
            categoryLabels.transform.GetChild(index).GetComponent<Text>().text = "";
    }


    /*
    // most keybinding hints are updated dynamically in-script when a list is re-populated (ie. components in a category).
    // in this case, it happens literally any time a category is changed, so we're always looking for updated keybindings.
    // other keybindings are static in that we don't overwrite their object's text field (and we can simply set them inactive)
    // so we call them "non-dynamic". These need to be updated any time the keybindings are changed using this method.
    public static void updateNonDynamicKeybindingHints()
    {
        // ensure category structures are consistent
        if (DrinkComponent.categoryLabels.Count != categoryLabels.transform.childCount)
            if (debugMode)
                Debug.Log("WARNING: Number of categories does not match the number of category labels");

        // update category labels to include new keybinding hints
        for (int i = 0; i < categoryLabels.transform.childCount; i++)
        {
            categoryLabels.transform.GetChild(i).GetComponent<Text>().text =
                DrinkComponent.categoryLabels[i] + " (" + InputManager.getStringFromKeyCode(InputManager.categoryKeys[i]) + ")";
        }
    }
    */
}
