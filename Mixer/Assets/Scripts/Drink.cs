using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
    // lists of drink recipes. 0 = easiest. 3 = hardest
    public static List<Drink> drinks0;
    public static List<Drink> drinks1;
    public static List<Drink> drinks2;
    public static List<Drink> drinks3;

    public string drinkName;
    public List<DrinkComponent> components;


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    public static void Initialize()
    {
        drinks0 = new List<Drink>();
        drinks1 = new List<Drink>();
        drinks2 = new List<Drink>();
        drinks3 = new List<Drink>();

        Drink lager = new Drink();
        Drink ale = new Drink();
        Drink rumAndCoke = new Drink();
        Drink ginAndTonic = new Drink();
        Drink oldFashioned = new Drink();
        Drink margarita = new Drink();
        Drink negroni = new Drink();
        Drink moscowMule = new Drink();
        Drink whiskeySour = new Drink();
        Drink manhattan = new Drink();
        Drink mimosa = new Drink();
        Drink gimlet = new Drink();
        Drink sazerac = new Drink();
        Drink martini = new Drink();
        Drink daiquiri = new Drink();

        // Lager
        List<DrinkComponent> lagerRecipe = new List<DrinkComponent>();
        lagerRecipe.Add(DrinkComponent.glassware["Pint Glass"]);
        lagerRecipe.Add(DrinkComponent.beers["Lager"]);

        // Ale
        List<DrinkComponent> aleRecipe = new List<DrinkComponent>();
        aleRecipe.Add(DrinkComponent.glassware["Pint Glass"]);
        aleRecipe.Add(DrinkComponent.beers["Ale"]);

        // Rum And Coke
        List<DrinkComponent> rumAndCokeRecipe = new List<DrinkComponent>();
        rumAndCokeRecipe.Add(DrinkComponent.glassware["Mixed Drink Glass"]);
        rumAndCokeRecipe.Add(DrinkComponent.otherMixers["Ice"]);
        rumAndCokeRecipe.Add(DrinkComponent.liquors["Rum"]);
        rumAndCokeRecipe.Add(DrinkComponent.otherMixers["Soda"]);

        // Gin And Tonic
        List<DrinkComponent> ginAndTonicRecipe = new List<DrinkComponent>();
        ginAndTonicRecipe.Add(DrinkComponent.glassware["Mixed Drink Glass"]);
        ginAndTonicRecipe.Add(DrinkComponent.otherMixers["Ice"]);
        ginAndTonicRecipe.Add(DrinkComponent.liquors["Gin"]);
        ginAndTonicRecipe.Add(DrinkComponent.otherMixers["Soda"]);
        ginAndTonicRecipe.Add(DrinkComponent.otherMixers["Orange Garnish"]);

        // Old Fashioned
        List<DrinkComponent> oldFashionedRecipe = new List<DrinkComponent>();
        oldFashionedRecipe.Add(DrinkComponent.glassware["Mixed Drink Glass"]);
        oldFashionedRecipe.Add(DrinkComponent.otherMixers["Ice"]);
        oldFashionedRecipe.Add(DrinkComponent.liquors["Whiskey"]);
        oldFashionedRecipe.Add(DrinkComponent.bittersAndSyrups["Angostura Bitters"]);
        oldFashionedRecipe.Add(DrinkComponent.otherMixers["Sugar"]);
        oldFashionedRecipe.Add(DrinkComponent.otherMixers["Orange Garnish"]);

        // Margarita
        List<DrinkComponent> margaritaRecipe = new List<DrinkComponent>();
        margaritaRecipe.Add(DrinkComponent.glassware["Mixed Drink Glass"]);
        margaritaRecipe.Add(DrinkComponent.otherMixers["Ice"]);
        margaritaRecipe.Add(DrinkComponent.liquors["Tequila"]);
        margaritaRecipe.Add(DrinkComponent.otherMixers["Margarita Mix"]);
        margaritaRecipe.Add(DrinkComponent.otherMixers["Lime juice"]);
        margaritaRecipe.Add(DrinkComponent.otherMixers["Salt Rim"]);

        // Negroni
        List<DrinkComponent> negroniRecipe = new List<DrinkComponent>();
        negroniRecipe.Add(DrinkComponent.glassware["Mixed Drink Glass"]);
        negroniRecipe.Add(DrinkComponent.otherMixers["Ice"]);
        negroniRecipe.Add(DrinkComponent.liquors["Gin"]);
        negroniRecipe.Add(DrinkComponent.bittersAndSyrups["Campari"]);
        negroniRecipe.Add(DrinkComponent.bittersAndSyrups["Sweet Vermouth"]);

        // Moscow Mule
        List<DrinkComponent> moscowMuleRecipe = new List<DrinkComponent>();
        moscowMuleRecipe.Add(DrinkComponent.glassware["Copper Cup"]);
        moscowMuleRecipe.Add(DrinkComponent.otherMixers["Ice"]);
        moscowMuleRecipe.Add(DrinkComponent.liquors["Vodka"]);
        moscowMuleRecipe.Add(DrinkComponent.otherMixers["Ginger Beer"]);
        moscowMuleRecipe.Add(DrinkComponent.otherMixers["Lime Juice"]);

        // Whiskey Sour
        List<DrinkComponent> whiskeySourRecipe = new List<DrinkComponent>();
        whiskeySourRecipe.Add(DrinkComponent.glassware["Mixed Drink Glass"]);
        whiskeySourRecipe.Add(DrinkComponent.otherMixers["Ice"]);
        whiskeySourRecipe.Add(DrinkComponent.liquors["Whiskey"]);
        whiskeySourRecipe.Add(DrinkComponent.otherMixers["Lemon Juice"]);
        whiskeySourRecipe.Add(DrinkComponent.otherMixers["Sugar"]);

        // Manhattan
        List<DrinkComponent> manhattanRecipe = new List<DrinkComponent>();
        manhattanRecipe.Add(DrinkComponent.glassware["Cocktail Glass"]);
        manhattanRecipe.Add(DrinkComponent.liquors["Whiskey"]);
        manhattanRecipe.Add(DrinkComponent.bittersAndSyrups["Sweet Vermouth"]);
        manhattanRecipe.Add(DrinkComponent.bittersAndSyrups["Angostura Bitters"]);

        // Mimosa
        List<DrinkComponent> mimosaRecipe = new List<DrinkComponent>();
        mimosaRecipe.Add(DrinkComponent.glassware["Mixed Drink Glass"]);
        mimosaRecipe.Add(DrinkComponent.otherMixers["Champagne"]);
        mimosaRecipe.Add(DrinkComponent.otherMixers["Orange Juice"]);

        // Gimlet
        List<DrinkComponent> gimletRecipe = new List<DrinkComponent>();
        gimletRecipe.Add(DrinkComponent.glassware["Cocktail Glass"]);
        gimletRecipe.Add(DrinkComponent.liquors["Vodka"]);
        gimletRecipe.Add(DrinkComponent.bittersAndSyrups["Simple Syrup"]);
        gimletRecipe.Add(DrinkComponent.otherMixers["Lime Juice"]);

        // Sazerac
        List<DrinkComponent> sazeracRecipe = new List<DrinkComponent>();
        sazeracRecipe.Add(DrinkComponent.glassware["Mixed Drink Glass"]);
        sazeracRecipe.Add(DrinkComponent.liquors["Whiskey"]);
        sazeracRecipe.Add(DrinkComponent.liquors["Absinthe"]);
        sazeracRecipe.Add(DrinkComponent.bittersAndSyrups["Simple Syrup"]);
        sazeracRecipe.Add(DrinkComponent.bittersAndSyrups["Peychaud’s Bitters"]);

        // Martini
        List<DrinkComponent> martiniRecipe = new List<DrinkComponent>();
        martiniRecipe.Add(DrinkComponent.glassware["Cocktail Glass"]);
        martiniRecipe.Add(DrinkComponent.liquors["Vodka"]);
        martiniRecipe.Add(DrinkComponent.bittersAndSyrups["Dry Vermouth"]);
        martiniRecipe.Add(DrinkComponent.otherMixers["Olive"]);

        // Daiquiri
        List<DrinkComponent> daiquiriRecipe = new List<DrinkComponent>();
        daiquiriRecipe.Add(DrinkComponent.glassware["Cocktail Glass"]);
        daiquiriRecipe.Add(DrinkComponent.liquors["Rum"]);
        daiquiriRecipe.Add(DrinkComponent.bittersAndSyrups["Simple Syrup"]);
        daiquiriRecipe.Add(DrinkComponent.otherMixers["Lime Juice"]);

        lager.assign("Lager", lagerRecipe);
        ale.assign("Ale", aleRecipe);
        rumAndCoke.assign("Rum And Coke", rumAndCokeRecipe);
        ginAndTonic.assign("Gin And Tonic", ginAndTonicRecipe);
        oldFashioned.assign("Old Fashioned", oldFashionedRecipe);
        margarita.assign("Margarita", margaritaRecipe);
        negroni.assign("Negroni", negroniRecipe);
        moscowMule.assign("Moscow Mule", moscowMuleRecipe);
        whiskeySour.assign("Whiskey Sour", whiskeySourRecipe);
        manhattan.assign("Manhattan", manhattanRecipe);
        mimosa.assign("Mimosa", mimosaRecipe);
        gimlet.assign("Gimlet", gimletRecipe);
        sazerac.assign("Sazerac", sazeracRecipe);
        martini.assign("Martini", martiniRecipe);
        daiquiri.assign("Daiquiri", daiquiriRecipe);

        drinks0.Add(lager);
        drinks0.Add(ale);
        drinks0.Add(rumAndCoke);
        drinks0.Add(ginAndTonic);
        drinks0.Add(oldFashioned);
        drinks0.Add(margarita);
        drinks0.Add(negroni);
        drinks0.Add(moscowMule);
        drinks0.Add(whiskeySour);
        drinks0.Add(manhattan);
        drinks0.Add(mimosa);
        drinks0.Add(gimlet);
        drinks0.Add(sazerac);
        drinks0.Add(martini);
        drinks0.Add(daiquiri);
    }


    public void assign(string aDrinkName, List<DrinkComponent> aComponents)
    {
        this.drinkName = aDrinkName;
        this.components = aComponents;
    }
}
