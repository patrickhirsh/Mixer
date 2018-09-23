using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkComponent : MonoBehaviour
{
    public static bool debugMode;   
    public static Dictionary<string, DrinkComponent> glassware;
    public static Dictionary<string, DrinkComponent> liquors;
    public static Dictionary<string, DrinkComponent> beers;
    public static Dictionary<string, DrinkComponent> bitters;
    public static Dictionary<string, DrinkComponent> nonAlcoholic;
    public static Dictionary<string, DrinkComponent> other;

    public string component;        // name of the drink component (typed nicely for front-end)
    public string category;         // the drinkComponent category this component belongs in
    public string keySequence;      // the keySequence required to properly add this ingredient to a drink


    // initialize static structures in DrinkComponent. Should be called once per level load
    public static void Initialize()
    {
        // DrinkComponent Categories
        glassware = new Dictionary<string, DrinkComponent>();
        liquors = new Dictionary<string, DrinkComponent>();
        beers = new Dictionary<string, DrinkComponent>();
        bitters = new Dictionary<string, DrinkComponent>();
        nonAlcoholic = new Dictionary<string, DrinkComponent>();
        other = new Dictionary<string, DrinkComponent>();


        #region            GLASSWARE

        // Pint Glass
        DrinkComponent pintGlass = new DrinkComponent();
        pintGlass.assign("Pint Glass", "glassware", "a");
        glassware.Add(pintGlass.component, pintGlass);

        // Mixed Drink Glass
        DrinkComponent mixedDrinkGlass = new DrinkComponent();
        mixedDrinkGlass.assign("Mixed Drink Glass", "glassware", "a");
        glassware.Add(mixedDrinkGlass.component, mixedDrinkGlass);

        // Cocktail Glass
        DrinkComponent cocktailGlass = new DrinkComponent();
        cocktailGlass.assign("Cocktail Glass", "glassware", "a");
        glassware.Add(cocktailGlass.component, cocktailGlass);

        // Copper Cup
        DrinkComponent copperCup = new DrinkComponent();
        copperCup.assign("Copper Cup", "glassware", "a");
        glassware.Add(copperCup.component, copperCup);

        #endregion


        #region            LIQUORS

        // Vodka
        DrinkComponent vodka = new DrinkComponent();
        vodka.assign("Vodka", "liquors", "a");
        liquors.Add(vodka.component, vodka);

        // Gin
        DrinkComponent gin = new DrinkComponent();
        gin.assign("Gin", "liquors", "a");
        liquors.Add(gin.component, gin);

        // Rum
        DrinkComponent rum = new DrinkComponent();
        rum.assign("Rum", "liquors", "a");
        liquors.Add(rum.component, rum);

        // Whiskey
        DrinkComponent whiskey = new DrinkComponent();
        whiskey.assign("Whiskey", "liquors", "a");
        liquors.Add(whiskey.component, whiskey);

        // Tequila
        DrinkComponent tequila = new DrinkComponent();
        tequila.assign("Tequila", "liquors", "a");
        liquors.Add(tequila.component, tequila);

        // Absinthe
        DrinkComponent absinthe = new DrinkComponent();
        absinthe.assign("Absinthe", "liquors", "a");
        liquors.Add(absinthe.component, absinthe);
        #endregion


        #region            BEERS

        // Lager
        DrinkComponent lager = new DrinkComponent();
        lager.assign("Lager", "beers", "a");
        beers.Add(lager.component, lager);

        // Ale
        DrinkComponent ale = new DrinkComponent();
        ale.assign("Ale", "beers", "a");
        beers.Add(ale.component, ale);
        #endregion


        #region           BITTERS

        // Angostura Bitters
        DrinkComponent angosturaBitters = new DrinkComponent();
        angosturaBitters.assign("Angostura Bitters", "bittersAndSyrups", "a");
        bitters.Add(angosturaBitters.component, angosturaBitters);

        // Peychaud's Bitters
        DrinkComponent peychaudsBitters = new DrinkComponent();
        peychaudsBitters.assign("Peychaud's Bitters", "bittersAndSyrups", "a");
        bitters.Add(peychaudsBitters.component, peychaudsBitters);

        // Campari
        DrinkComponent campari = new DrinkComponent();       
        campari.assign("Campari", "bittersAndSyrups", "a");
        bitters.Add(campari.component, campari);     
        
        // Sweet Vermouth
        DrinkComponent sweetVermouth = new DrinkComponent();
        sweetVermouth.assign("Sweet Vermouth", "bittersAndSyrups", "a");
        bitters.Add(sweetVermouth.component, sweetVermouth);

        // Dry Vermouth
        DrinkComponent dryVermouth = new DrinkComponent();       
        dryVermouth.assign("Dry Vermouth", "bittersAndSyrups", "a");
        bitters.Add(dryVermouth.component, dryVermouth);
        #endregion


        #region        NON-ALCOHOLIC

        // Tonic
        DrinkComponent tonic = new DrinkComponent();
        tonic.assign("Tonic", "nonAlcoholic", "a");
        nonAlcoholic.Add(tonic.component, tonic);

        // Coke
        DrinkComponent coke = new DrinkComponent();
        coke.assign("Coke", "nonAlcoholic", "a");
        nonAlcoholic.Add(coke.component, coke);

        // Ginger Beer
        DrinkComponent gingerBeer = new DrinkComponent();
        gingerBeer.assign("Ginger Beer", "nonAlcoholic", "a");
        nonAlcoholic.Add(gingerBeer.component, gingerBeer);

        // Lime Juice
        DrinkComponent limeJuice = new DrinkComponent();
        limeJuice.assign("Lime Juice", "nonAlcoholic", "a");
        nonAlcoholic.Add(limeJuice.component, limeJuice);

        // Lemon Juice
        DrinkComponent lemonJuice = new DrinkComponent();
        lemonJuice.assign("Lemon Juice", "nonAlcoholic", "a");
        nonAlcoholic.Add(lemonJuice.component, lemonJuice);

        // Margarita Mix
        DrinkComponent margaritaMix = new DrinkComponent();
        margaritaMix.assign("Margarita Mix", "nonAlcoholic", "a");
        nonAlcoholic.Add(margaritaMix.component, margaritaMix);

        // Simple Syrup
        DrinkComponent simpleSyrup = new DrinkComponent();
        simpleSyrup.assign("Simple Syrup", "nonAlcoholic", "a");
        nonAlcoholic.Add(simpleSyrup.component, simpleSyrup);
        #endregion


        #region           OTHER
        // Ice
        DrinkComponent ice = new DrinkComponent();
        ice.assign("Ice", "other", "a");
        other.Add(ice.component, ice);

        // Salt Rim
        DrinkComponent saltRim = new DrinkComponent();
        saltRim.assign("Salt Rim", "other", "a");
        other.Add(saltRim.component, saltRim);

        // Olive
        DrinkComponent olive = new DrinkComponent();
        olive.assign("Olive", "other", "a");
        other.Add(olive.component, olive);

        // Orange Garnish
        DrinkComponent orangeGarnish = new DrinkComponent();
        orangeGarnish.assign("Orange Garnish", "other", "a");
        other.Add(orangeGarnish.component, orangeGarnish);
        #endregion
    }


    public void assign(string component, string category, string keySequence)
    {
        this.component = component;
        this.category = category;
        this.keySequence = keySequence;
    }
}
