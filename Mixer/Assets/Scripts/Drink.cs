using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink
{
    // STATIC
    public static bool debugMode;
    public static List<Drink> drinks0 { get; private set; }         // easy drink recipes
    public static List<Drink> drinks1 { get; private set; }         // moderate drink recipes
    public static List<Drink> drinks2 { get; private set; }         // challenging drink recipes
    public static List<Drink> drinks3 { get; private set; }         // difficult drink recipes

    // INSTANCE
    public string drinkName { get; private set; }                   // formatted nicely for front-end
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

        // Lager
        Drink lager = new Drink();
        List<DrinkComponent> lagerRecipe = new List<DrinkComponent>();
        lagerRecipe.Add(DrinkComponent.glassware["Pint Glass"]);
        lagerRecipe.Add(DrinkComponent.beers["Lager"]);
        lager.assign("Lager", lagerRecipe);
        drinks0.Add(lager);

        // Ale
        Drink ale = new Drink();
        List<DrinkComponent> aleRecipe = new List<DrinkComponent>();
        aleRecipe.Add(DrinkComponent.glassware["Pint Glass"]);
        aleRecipe.Add(DrinkComponent.beers["Ale"]);
        ale.assign("Ale", aleRecipe);
        drinks0.Add(ale);

        // Rum And Coke
        Drink rumAndCoke = new Drink();
        List<DrinkComponent> rumAndCokeRecipe = new List<DrinkComponent>();
        rumAndCokeRecipe.Add(DrinkComponent.glassware["Mixed Drink Glass"]);
        rumAndCokeRecipe.Add(DrinkComponent.other["Ice"]);
        rumAndCokeRecipe.Add(DrinkComponent.liquors["Rum"]);
        rumAndCokeRecipe.Add(DrinkComponent.nonAlcoholic["Coke"]);
        rumAndCoke.assign("Rum And Coke", rumAndCokeRecipe);
        drinks0.Add(rumAndCoke);

        // Gin And Tonic
        Drink ginAndTonic = new Drink();
        List<DrinkComponent> ginAndTonicRecipe = new List<DrinkComponent>();
        ginAndTonicRecipe.Add(DrinkComponent.glassware["Mixed Drink Glass"]);
        ginAndTonicRecipe.Add(DrinkComponent.other["Ice"]);
        ginAndTonicRecipe.Add(DrinkComponent.liquors["Gin"]);
        ginAndTonicRecipe.Add(DrinkComponent.nonAlcoholic["Tonic"]);
        ginAndTonicRecipe.Add(DrinkComponent.other["Orange Garnish"]);
        ginAndTonic.assign("Gin And Tonic", ginAndTonicRecipe);
        drinks0.Add(ginAndTonic);

        // Old Fashioned
        Drink oldFashioned = new Drink();
        List<DrinkComponent> oldFashionedRecipe = new List<DrinkComponent>();
        oldFashionedRecipe.Add(DrinkComponent.glassware["Mixed Drink Glass"]);
        oldFashionedRecipe.Add(DrinkComponent.other["Ice"]);
        oldFashionedRecipe.Add(DrinkComponent.liquors["Whiskey"]);
        oldFashionedRecipe.Add(DrinkComponent.bitters["Angostura Bitters"]);
        oldFashionedRecipe.Add(DrinkComponent.nonAlcoholic["Simple Syrup"]);
        oldFashionedRecipe.Add(DrinkComponent.other["Orange Garnish"]);
        oldFashioned.assign("Old Fashioned", oldFashionedRecipe);
        drinks0.Add(oldFashioned);

        // Margarita
        Drink margarita = new Drink();
        List<DrinkComponent> margaritaRecipe = new List<DrinkComponent>();
        margaritaRecipe.Add(DrinkComponent.glassware["Mixed Drink Glass"]);
        margaritaRecipe.Add(DrinkComponent.other["Ice"]);
        margaritaRecipe.Add(DrinkComponent.liquors["Tequila"]);
        margaritaRecipe.Add(DrinkComponent.nonAlcoholic["Margarita Mix"]);
        margaritaRecipe.Add(DrinkComponent.nonAlcoholic["Lime Juice"]);
        margaritaRecipe.Add(DrinkComponent.other["Salt Rim"]);
        margarita.assign("Margarita", margaritaRecipe);
        drinks0.Add(margarita);

        // Negroni
        Drink negroni = new Drink();
        List<DrinkComponent> negroniRecipe = new List<DrinkComponent>();
        negroniRecipe.Add(DrinkComponent.glassware["Mixed Drink Glass"]);
        negroniRecipe.Add(DrinkComponent.other["Ice"]);
        negroniRecipe.Add(DrinkComponent.liquors["Gin"]);
        negroniRecipe.Add(DrinkComponent.bitters["Campari"]);
        negroniRecipe.Add(DrinkComponent.bitters["Sweet Vermouth"]);
        negroni.assign("Negroni", negroniRecipe);
        drinks0.Add(negroni);

        // Moscow Mule
        Drink moscowMule = new Drink();
        List<DrinkComponent> moscowMuleRecipe = new List<DrinkComponent>();
        moscowMuleRecipe.Add(DrinkComponent.glassware["Copper Cup"]);
        moscowMuleRecipe.Add(DrinkComponent.other["Ice"]);
        moscowMuleRecipe.Add(DrinkComponent.liquors["Vodka"]);
        moscowMuleRecipe.Add(DrinkComponent.nonAlcoholic["Ginger Beer"]);
        moscowMuleRecipe.Add(DrinkComponent.nonAlcoholic["Lime Juice"]);
        moscowMule.assign("Moscow Mule", moscowMuleRecipe);
        drinks0.Add(moscowMule);

        // Whiskey Sour
        Drink whiskeySour = new Drink();
        List<DrinkComponent> whiskeySourRecipe = new List<DrinkComponent>();
        whiskeySourRecipe.Add(DrinkComponent.glassware["Mixed Drink Glass"]);
        whiskeySourRecipe.Add(DrinkComponent.other["Ice"]);
        whiskeySourRecipe.Add(DrinkComponent.liquors["Whiskey"]);
        whiskeySourRecipe.Add(DrinkComponent.nonAlcoholic["Lemon Juice"]);
        whiskeySourRecipe.Add(DrinkComponent.nonAlcoholic["Simple Syrup"]);
        whiskeySour.assign("Whiskey Sour", whiskeySourRecipe);
        drinks0.Add(whiskeySour);

        // Manhattan
        Drink manhattan = new Drink();
        List<DrinkComponent> manhattanRecipe = new List<DrinkComponent>();
        manhattanRecipe.Add(DrinkComponent.glassware["Cocktail Glass"]);
        manhattanRecipe.Add(DrinkComponent.liquors["Whiskey"]);
        manhattanRecipe.Add(DrinkComponent.bitters["Sweet Vermouth"]);
        manhattanRecipe.Add(DrinkComponent.bitters["Angostura Bitters"]);
        manhattan.assign("Manhattan", manhattanRecipe);
        drinks0.Add(manhattan);

        // Gimlet
        Drink gimlet = new Drink();
        List<DrinkComponent> gimletRecipe = new List<DrinkComponent>();
        gimletRecipe.Add(DrinkComponent.glassware["Cocktail Glass"]);
        gimletRecipe.Add(DrinkComponent.liquors["Vodka"]);
        gimletRecipe.Add(DrinkComponent.nonAlcoholic["Simple Syrup"]);
        gimletRecipe.Add(DrinkComponent.nonAlcoholic["Lime Juice"]);
        gimlet.assign("Gimlet", gimletRecipe);
        drinks0.Add(gimlet);

        // Sazerac
        Drink sazerac = new Drink();     
        List<DrinkComponent> sazeracRecipe = new List<DrinkComponent>();
        sazeracRecipe.Add(DrinkComponent.glassware["Mixed Drink Glass"]);
        sazeracRecipe.Add(DrinkComponent.liquors["Whiskey"]);
        sazeracRecipe.Add(DrinkComponent.liquors["Absinthe"]);
        sazeracRecipe.Add(DrinkComponent.nonAlcoholic["Simple Syrup"]);
        sazeracRecipe.Add(DrinkComponent.bitters["Peychaud's Bitters"]);
        sazerac.assign("Sazerac", sazeracRecipe);
        drinks0.Add(sazerac);

        // Martini
        Drink martini = new Drink();
        List<DrinkComponent> martiniRecipe = new List<DrinkComponent>();
        martiniRecipe.Add(DrinkComponent.glassware["Cocktail Glass"]);
        martiniRecipe.Add(DrinkComponent.liquors["Vodka"]);
        martiniRecipe.Add(DrinkComponent.bitters["Dry Vermouth"]);
        martiniRecipe.Add(DrinkComponent.other["Olive"]);
        martini.assign("Martini", martiniRecipe);
        drinks0.Add(martini);

        // Daiquiri
        Drink daiquiri = new Drink();
        List<DrinkComponent> daiquiriRecipe = new List<DrinkComponent>();
        daiquiriRecipe.Add(DrinkComponent.glassware["Cocktail Glass"]);
        daiquiriRecipe.Add(DrinkComponent.liquors["Rum"]);
        daiquiriRecipe.Add(DrinkComponent.nonAlcoholic["Simple Syrup"]);
        daiquiriRecipe.Add(DrinkComponent.nonAlcoholic["Lime Juice"]);
        daiquiri.assign("Daiquiri", daiquiriRecipe);
        drinks0.Add(daiquiri);
    }


    // given a difficulty (0-3), returns a random drink from that tier
    public static Drink getRandomDrink(int difficulty)
    {
        if (difficulty == 0)
        {
            int index = UnityEngine.Random.Range(0, drinks0.Count - 1);
            return drinks0[index];
        }

        if (difficulty == 1)
        {
            int index = UnityEngine.Random.Range(0, drinks1.Count - 1);
            return drinks1[index];
        }

        if (difficulty == 2)
        {
            int index = UnityEngine.Random.Range(0, drinks2.Count - 1);
            return drinks2[index];
        }

        if (difficulty == 3)
        {
            int index = UnityEngine.Random.Range(0, drinks3.Count - 1);
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

    private void assign(string drinkName, List<DrinkComponent> components)
    {
        this.drinkName = drinkName;
        this.components = components;
    }

    #endregion
}
