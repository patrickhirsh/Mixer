using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsManager : MonoBehaviour
{
    public static bool debugMode;
    private static float ORDER_SPRITE_SPACING = .5f;

    private static GameObject playerScore;
    private static GameObject pointsAwarded;
    private static GameObject currentDrinkPanel;
    private static GameObject currentDrinkLabel;
    private static GameObject categoryLabels;
    private static GameObject categoryIcons;
    private static GameObject categoryComponentList;
    private static GameObject currentDrinkProgress;


    public static void Initialize()
    {
        // obtain references to frequently updated GameObjects
        playerScore = GameObject.Find("score");
        pointsAwarded = GameObject.Find("points_awarded");
        currentDrinkPanel = GameObject.Find("current_drink");
        currentDrinkLabel = GameObject.Find("current_drink_label");
        currentDrinkProgress = GameObject.Find("current_drink_progress");
        categoryLabels = GameObject.Find("category_labels");
        categoryIcons = GameObject.Find("category_icons");
        categoryComponentList = GameObject.Find("category_component_list");

        // non-dynamic keybindings need to be manually populated on startup by calling this method
        // see the method's documentation for more info on "non-dynamic" keybinding hints
        updateNonDynamicKeybindingHints();
    }


    // Update is called once per frame
    void Update ()
    {
        updateBartender();
        updateOrders();
        updateCurrentDrinkPanel();
        updateCategories();
        updateComponents();
    }


    // updates the player's score. Should be called from GameManager any time the player's score changes
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
            Level.levels[GameManager.currentLevel].bartenderPositions[Bartender.position];
    }


    // updates the order sprites based on alley position and ORDER_SPRITE_SPACING
    public static void updateOrders()
    {
        foreach (Transform alley in OrderManager.orderAlleys.transform)
        {
            for (int i = 0; i < alley.childCount; i++)
            {
                alley.GetChild(i).position = new Vector2(
                    alley.position.x - ((i + 1) * ORDER_SPRITE_SPACING),
                    alley.position.y);
            }
        }
    }


    // updates the current drink panel's graphics (components, progress icons, etc.)
    public static void updateCurrentDrinkPanel()
    {
        // does there exist an order at the current bartending position?
        if (OrderManager.orderAlleys.transform.GetChild(Bartender.position).transform.childCount != 0)
        {
            // clear the current drink labels and icons
            for (int i = 0; i < currentDrinkPanel.transform.childCount; i++)
                currentDrinkPanel.transform.GetChild(i).GetComponent<Text>().text = "";
            for (int i = 0; i < currentDrinkProgress.transform.childCount; i++)
                currentDrinkProgress.transform.GetChild(i).gameObject.SetActive(false);

            // set the title at the top of the panel
            currentDrinkLabel.GetComponent<Text>().text =
                OrderManager.orderAlleys.transform.GetChild(Bartender.position).transform.GetChild(0).GetComponent<Order>().drink.drinkName;

            // render the drink components
            for (int i = 1; i <= OrderManager.orderAlleys.transform.GetChild(Bartender.position).GetChild(0).GetComponent<Order>().drink.components.Count; i++)
            {
                currentDrinkPanel.transform.GetChild(i).GetComponent<Text>().text =
                    OrderManager.orderAlleys.transform.GetChild(Bartender.position).GetChild(0).GetComponent<Order>().drink.components[i - 1].component;
            }

            // render the drink progress icons
            for (int i = 0; i < OrderManager.orderProgress[Bartender.position].Count; i++)
                currentDrinkProgress.transform.GetChild(i).gameObject.SetActive(true);
        }

        // if not, clear the panel
        else
        {
            // clear the icons and labels
            for (int i = 0; i < currentDrinkPanel.transform.childCount; i++)
                currentDrinkPanel.transform.GetChild(i).GetComponent<Text>().text = "";
            for (int i = 0; i < currentDrinkProgress.transform.childCount; i++)
                currentDrinkProgress.transform.GetChild(i).gameObject.SetActive(false);
        }
    }


    // updates the category icons and labels on the left menu block
    public static void updateCategories()
    {
        // bartender isn't in a menu, show menu labels
        if (Bartender.state == null)
        {
            for (int i = 0; i < categoryLabels.transform.childCount; i++)
                categoryLabels.transform.GetChild(i).gameObject.SetActive(true);
        }

        // Bartender is in a menu. Hide labels
        else
        {
            for (int i = 0; i < categoryLabels.transform.childCount; i++)
                categoryLabels.transform.GetChild(i).gameObject.SetActive(false);
        }
    }


    // updates the drink components section of the left menu block
    public static void updateComponents()
    {
        // bartender isn't in a menu. hide current category section
        if (Bartender.state == null)
        {
            for (int i = 0; i < categoryComponentList.transform.childCount; i++)
                categoryComponentList.transform.GetChild(i).GetComponent<Text>().text = "";
        }

        // bartender is in a menu. display the category and it's drink components
        else
        {
            // clear any existing labels
            for (int i = 0; i < categoryComponentList.transform.childCount; i++)
                categoryComponentList.transform.GetChild(i).GetComponent<Text>().text = "";

            // update the category label
            categoryComponentList.transform.GetChild(0).GetComponent<Text>().text = Bartender.state;

            // update the category component list 
            int index = 1;
            switch (Bartender.state)
            {
                case "Glassware":
                    // iterate through the components in this category and update the labels accordingly
                    foreach (KeyValuePair<string, DrinkComponent> component in DrinkComponent.glassware)
                    {
                        // perform cleanup on KeyCode.ToString() labels                          
                        categoryComponentList.transform.GetChild(index).GetComponent<Text>().text =
                            "(" + component.Value.keyString + ") " + component.Key;
                        index++;
                    }
                    // once we run out of components, use the index (keeping track of child position in categoryComponentList) to set the rest to ""
                    for (int i = index; i < categoryComponentList.transform.childCount; i++)
                        categoryComponentList.transform.GetChild(index).GetComponent<Text>().text = "";
                    break;

                case "Beers":
                    foreach (KeyValuePair<string, DrinkComponent> component in DrinkComponent.beers)
                    {
                        categoryComponentList.transform.GetChild(index).GetComponent<Text>().text =
                            "(" + component.Value.keyString + ") " + component.Key;
                        index++;
                    }
                    for (int i = index; i < categoryComponentList.transform.childCount; i++)
                        categoryComponentList.transform.GetChild(index).GetComponent<Text>().text = "";
                    break;

                case "Liquors":
                    foreach (KeyValuePair<string, DrinkComponent> component in DrinkComponent.liquors)
                    {
                        categoryComponentList.transform.GetChild(index).GetComponent<Text>().text =
                            "(" + component.Value.keyString + ") " + component.Key;
                        index++;
                    }
                    for (int i = index; i < categoryComponentList.transform.childCount; i++)
                        categoryComponentList.transform.GetChild(index).GetComponent<Text>().text = "";
                    break;

                case "Bitters":
                    foreach (KeyValuePair<string, DrinkComponent> component in DrinkComponent.bitters)
                    {
                        categoryComponentList.transform.GetChild(index).GetComponent<Text>().text =
                            "(" + component.Value.keyString + ") " + component.Key;
                        index++;
                    }
                    for (int i = index; i < categoryComponentList.transform.childCount; i++)
                        categoryComponentList.transform.GetChild(index).GetComponent<Text>().text = "";
                    break;

                case "NonAlcoholic":
                    foreach (KeyValuePair<string, DrinkComponent> component in DrinkComponent.nonAlcoholic)
                    {
                        categoryComponentList.transform.GetChild(index).GetComponent<Text>().text =
                            "(" + component.Value.keyString + ") " + component.Key;
                        index++;
                    }
                    for (int i = index; i < categoryComponentList.transform.childCount; i++)
                        categoryComponentList.transform.GetChild(index).GetComponent<Text>().text = "";
                    break;

                case "Other":
                    foreach (KeyValuePair<string, DrinkComponent> component in DrinkComponent.other)
                    {
                        categoryComponentList.transform.GetChild(index).GetComponent<Text>().text =
                            "(" + component.Value.keyString + ") " + component.Key;
                        index++;
                    }
                    for (int i = index; i < categoryComponentList.transform.childCount; i++)
                        categoryComponentList.transform.GetChild(index).GetComponent<Text>().text = "";
                    break;
            }            
        }
    }


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
}
