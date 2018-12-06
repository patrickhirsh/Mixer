using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Drink serves both static and instance functionality.
/// See the doc comments under each region for more information on their purposes.
/// </summary>
public class Drink
{
    // STATIC
    public static bool debugMode;
    private static List<Drink> drinks0;         // easy drink recipes
    private static List<Drink> drinks1;         // moderate drink recipes
    private static List<Drink> drinks2;         // challenging drink recipes
    private static List<Drink> drinks3;         // difficult drink recipes

    // INSTANCE
    public string drinkName { get; private set; }                   // formatted nicely for front-end
    public int drinkTier { get; private set; }                      // this drink's difficulty level (0 - 3)
    public List<DrinkComponent> components { get; private set; }    // a list of all components required to make this drink


    #region STATIC
    /*  
     *  Static Drink is responsible for maintaining lists of all possible drink
     *  orders. Furthermore, static Drink provides a way to obtain a random drink
     *  from the desired difficulty tier
     */

    // initialize static structures in Drink. Should be called once per level load
    public static void Initialize()
    {
        drinks0 = new List<Drink>();
        drinks1 = new List<Drink>();
        drinks2 = new List<Drink>();
        drinks3 = new List<Drink>();

        #region Tier 0

        // Lager
        Drink lager = new Drink();
        List<DrinkComponent> lagerRecipe = new List<DrinkComponent>();
        lagerRecipe.Add(DrinkComponent.glassware["Pint Glass"]);
        lagerRecipe.Add(DrinkComponent.beers["Lager"]);
        lager.assign("Lager", 0, lagerRecipe);
        drinks0.Add(lager);

        // Ale
        Drink ale = new Drink();
        List<DrinkComponent> aleRecipe = new List<DrinkComponent>();
        aleRecipe.Add(DrinkComponent.glassware["Pint Glass"]);
        aleRecipe.Add(DrinkComponent.beers["Ale"]);
        ale.assign("Ale", 0, aleRecipe);
        drinks0.Add(ale);

        #endregion

        #region Tier 1

        // Manhattan
        Drink manhattan = new Drink();
        List<DrinkComponent> manhattanRecipe = new List<DrinkComponent>();
        manhattanRecipe.Add(DrinkComponent.glassware["Cocktail Glass"]);
        manhattanRecipe.Add(DrinkComponent.liquors["Whiskey"]);
        manhattanRecipe.Add(DrinkComponent.bitters["Sweet Vermouth"]);
        manhattanRecipe.Add(DrinkComponent.bitters["Angostura Bitters"]);
        manhattan.assign("Manhattan", 1, manhattanRecipe);
        drinks1.Add(manhattan);

        // Martini
        Drink martini = new Drink();
        List<DrinkComponent> martiniRecipe = new List<DrinkComponent>();
        martiniRecipe.Add(DrinkComponent.glassware["Cocktail Glass"]);
        martiniRecipe.Add(DrinkComponent.liquors["Vodka"]);
        martiniRecipe.Add(DrinkComponent.bitters["Dry Vermouth"]);
        martiniRecipe.Add(DrinkComponent.other["Olive"]);
        martini.assign("Martini", 1, martiniRecipe);
        drinks1.Add(martini);

        // Daiquiri
        Drink daiquiri = new Drink();
        List<DrinkComponent> daiquiriRecipe = new List<DrinkComponent>();
        daiquiriRecipe.Add(DrinkComponent.glassware["Cocktail Glass"]);
        daiquiriRecipe.Add(DrinkComponent.liquors["Rum"]);
        daiquiriRecipe.Add(DrinkComponent.nonAlcoholic["Simple Syrup"]);
        daiquiriRecipe.Add(DrinkComponent.nonAlcoholic["Lime Juice"]);
        daiquiri.assign("Daiquiri", 1, daiquiriRecipe);
        drinks1.Add(daiquiri);

        // Gimlet
        Drink gimlet = new Drink();
        List<DrinkComponent> gimletRecipe = new List<DrinkComponent>();
        gimletRecipe.Add(DrinkComponent.glassware["Cocktail Glass"]);
        gimletRecipe.Add(DrinkComponent.liquors["Vodka"]);
        gimletRecipe.Add(DrinkComponent.nonAlcoholic["Simple Syrup"]);
        gimletRecipe.Add(DrinkComponent.nonAlcoholic["Lime Juice"]);
        gimlet.assign("Gimlet", 1, gimletRecipe);
        drinks1.Add(gimlet);

        #endregion

        #region Tier 2

        // Rum And Coke
        Drink rumAndCola = new Drink();
        List<DrinkComponent> rumAndCokeRecipe = new List<DrinkComponent>();
        rumAndCokeRecipe.Add(DrinkComponent.glassware["Mixed Drink Glass"]);
        rumAndCokeRecipe.Add(DrinkComponent.other["Ice"]);
        rumAndCokeRecipe.Add(DrinkComponent.liquors["Rum"]);
        rumAndCokeRecipe.Add(DrinkComponent.nonAlcoholic["Cola"]);
        rumAndCola.assign("Rum And Cola", 2, rumAndCokeRecipe);
        drinks2.Add(rumAndCola);

        // Negroni
        Drink negroni = new Drink();
        List<DrinkComponent> negroniRecipe = new List<DrinkComponent>();
        negroniRecipe.Add(DrinkComponent.glassware["Mixed Drink Glass"]);
        negroniRecipe.Add(DrinkComponent.other["Ice"]);
        negroniRecipe.Add(DrinkComponent.liquors["Gin"]);
        negroniRecipe.Add(DrinkComponent.bitters["Campari"]);
        negroniRecipe.Add(DrinkComponent.bitters["Sweet Vermouth"]);
        negroni.assign("Negroni", 2, negroniRecipe);
        drinks2.Add(negroni);

        // Gin And Tonic
        Drink ginAndTonic = new Drink();
        List<DrinkComponent> ginAndTonicRecipe = new List<DrinkComponent>();
        ginAndTonicRecipe.Add(DrinkComponent.glassware["Mixed Drink Glass"]);
        ginAndTonicRecipe.Add(DrinkComponent.other["Ice"]);
        ginAndTonicRecipe.Add(DrinkComponent.liquors["Gin"]);
        ginAndTonicRecipe.Add(DrinkComponent.nonAlcoholic["Tonic"]);
        ginAndTonicRecipe.Add(DrinkComponent.other["Orange Garnish"]);
        ginAndTonic.assign("Gin And Tonic", 2, ginAndTonicRecipe);
        drinks2.Add(ginAndTonic);

        #endregion

        #region Tier 3

        // Old Fashioned
        Drink oldFashioned = new Drink();
        List<DrinkComponent> oldFashionedRecipe = new List<DrinkComponent>();
        oldFashionedRecipe.Add(DrinkComponent.glassware["Mixed Drink Glass"]);
        oldFashionedRecipe.Add(DrinkComponent.other["Ice"]);
        oldFashionedRecipe.Add(DrinkComponent.liquors["Whiskey"]);
        oldFashionedRecipe.Add(DrinkComponent.bitters["Angostura Bitters"]);
        oldFashionedRecipe.Add(DrinkComponent.nonAlcoholic["Simple Syrup"]);
        oldFashionedRecipe.Add(DrinkComponent.other["Orange Garnish"]);
        oldFashioned.assign("Old Fashioned", 3, oldFashionedRecipe);
        drinks3.Add(oldFashioned);

        // Margarita
        Drink margarita = new Drink();
        List<DrinkComponent> margaritaRecipe = new List<DrinkComponent>();
        margaritaRecipe.Add(DrinkComponent.glassware["Mixed Drink Glass"]);
        margaritaRecipe.Add(DrinkComponent.other["Ice"]);
        margaritaRecipe.Add(DrinkComponent.liquors["Tequila"]);
        margaritaRecipe.Add(DrinkComponent.nonAlcoholic["Margarita Mix"]);
        margaritaRecipe.Add(DrinkComponent.nonAlcoholic["Lime Juice"]);
        margaritaRecipe.Add(DrinkComponent.other["Salt Rim"]);
        margarita.assign("Margarita", 3, margaritaRecipe);
        drinks3.Add(margarita);

        // Sazerac
        Drink sazerac = new Drink();
        List<DrinkComponent> sazeracRecipe = new List<DrinkComponent>();
        sazeracRecipe.Add(DrinkComponent.glassware["Mixed Drink Glass"]);
        sazeracRecipe.Add(DrinkComponent.liquors["Whiskey"]);
        sazeracRecipe.Add(DrinkComponent.liquors["Absinthe"]);
        sazeracRecipe.Add(DrinkComponent.nonAlcoholic["Simple Syrup"]);
        sazeracRecipe.Add(DrinkComponent.bitters["Peychaud's Bitters"]);
        sazerac.assign("Sazerac", 3, sazeracRecipe);
        drinks3.Add(sazerac);

        // Moscow Mule
        Drink moscowMule = new Drink();
        List<DrinkComponent> moscowMuleRecipe = new List<DrinkComponent>();
        moscowMuleRecipe.Add(DrinkComponent.glassware["Copper Cup"]);
        moscowMuleRecipe.Add(DrinkComponent.other["Ice"]);
        moscowMuleRecipe.Add(DrinkComponent.liquors["Vodka"]);
        moscowMuleRecipe.Add(DrinkComponent.nonAlcoholic["Ginger Beer"]);
        moscowMuleRecipe.Add(DrinkComponent.nonAlcoholic["Lime Juice"]);
        moscowMule.assign("Moscow Mule", 3, moscowMuleRecipe);
        drinks3.Add(moscowMule);

        #endregion
    }


    /// <summary>
    /// uses the DifficultyManager to select a drink from a tier of the appropriate difficulty.
    /// </summary>
    public static Drink getRandomDrink()
    {
        return getRandomDrink(DifficultyManager.getDrinkTier());
    }

    /// <summary>
    /// given a difficulty (0-3), returns a random drink from that tier
    /// </summary>
    private static Drink getRandomDrink(int difficulty)
    {
        if (difficulty == 0)
        {
            int index = UnityEngine.Random.Range(0, drinks0.Count);
            return drinks0[index];
        }

        if (difficulty == 1)
        {
            int index = UnityEngine.Random.Range(0, drinks1.Count);
            return drinks1[index];
        }

        if (difficulty == 2)
        {
            int index = UnityEngine.Random.Range(0, drinks2.Count);
            return drinks2[index];
        }

        if (difficulty == 3)
        {
            int index = UnityEngine.Random.Range(0, drinks3.Count);
            return drinks3[index];
        }

        throw new Exception("ERROR: Invalid difficulty given to Drink.getRandomDrink()");
    }

    #endregion


    #region INSTANCE
    /*
     *  an instance of Drink is a container comprised of a drink name and all 
     *  DrinkComponents required to create it.
     */

    /// <summary>
    /// Used internally to assign all proper data to an instance of Drink
    /// </summary>
    private void assign(string drinkName, int drinkTier, List<DrinkComponent> components)
    {
        this.drinkName = drinkName;
        this.drinkTier = drinkTier;
        this.components = components;
    }

    #endregion
}
