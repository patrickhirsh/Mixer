﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkComponent
{
    // STATIC
    public static bool debugMode;   
    public static Dictionary<string, DrinkComponent> glassware { get; private set; }
    public static Dictionary<string, DrinkComponent> beers { get; private set; }
    public static Dictionary<string, DrinkComponent> liquors { get; private set; }
    public static Dictionary<string, DrinkComponent> bitters { get; private set; }
    public static Dictionary<string, DrinkComponent> nonAlcoholic { get; private set; }
    public static Dictionary<string, DrinkComponent> other { get; private set; }

    // used in GraphicsManager to populate category label text fields.
    public static List<string> categoryLabels { get; private set; }

    // INSTANCE
    public string component { get; private set; }        // name of the drink component (typed nicely for front-end)
    public string category { get; private set; }         // the drinkComponent category this component belongs in
    public KeyCode key { get; private set; }             // the KeyCode associated with this component within it's category
    public string keyString { get; private set; }        // KeyCode (typed nicely for front-end)


    #region STATIC
    /*
     *  Static DrinkComponent maintains lists of all available components that
     *  can make up a drink, sorted by category. These lists, generated by
     *  Initialize(), are meant to be the set of valid components used to create
     *  drinks and therefore must be immutable outside this class. 
     *  
     *  Static DrinkComponent DOES provide a way to create a new DrinkComponent 
     *  outside of the aforementioned. This function allows the same container 
     *  format to be used as a way to store player-made drink components during 
     *  gameplay.
     */

    // initialize static structures in DrinkComponent. Should be called once per level load
    public static void Initialize()
    {
        // DrinkComponent Categories
        glassware = new Dictionary<string, DrinkComponent>();
        beers = new Dictionary<string, DrinkComponent>();
        liquors = new Dictionary<string, DrinkComponent>();
        bitters = new Dictionary<string, DrinkComponent>();
        nonAlcoholic = new Dictionary<string, DrinkComponent>();
        other = new Dictionary<string, DrinkComponent>();

        categoryLabels = new List<string>() { "Glassware", "Beers", "Liquors", "Bitters", "Non-Alcoholic", "Other"};


        #region            GLASSWARE

        // Pint Glass
        DrinkComponent pintGlass = new DrinkComponent();
        pintGlass.assign("Pint Glass", InputManager.category1, "glassware");
        glassware.Add(pintGlass.component, pintGlass);

        // Mixed Drink Glass
        DrinkComponent mixedDrinkGlass = new DrinkComponent();
        mixedDrinkGlass.assign("Mixed Drink Glass", InputManager.category2, "glassware");
        glassware.Add(mixedDrinkGlass.component, mixedDrinkGlass);

        // Cocktail Glass
        DrinkComponent cocktailGlass = new DrinkComponent();
        cocktailGlass.assign("Cocktail Glass", InputManager.category3, "glassware");
        glassware.Add(cocktailGlass.component, cocktailGlass);

        // Copper Cup
        DrinkComponent copperCup = new DrinkComponent();
        copperCup.assign("Copper Cup", InputManager.category4, "glassware");
        glassware.Add(copperCup.component, copperCup);

        #endregion


        #region            BEERS

        // Lager
        DrinkComponent lager = new DrinkComponent();
        lager.assign("Lager", InputManager.category1, "beers");
        beers.Add(lager.component, lager);

        // Ale
        DrinkComponent ale = new DrinkComponent();
        ale.assign("Ale", InputManager.category2, "beers");
        beers.Add(ale.component, ale);
        #endregion


        #region            LIQUORS

        // Vodka
        DrinkComponent vodka = new DrinkComponent();
        vodka.assign("Vodka", InputManager.category1, "liquors");
        liquors.Add(vodka.component, vodka);

        // Gin
        DrinkComponent gin = new DrinkComponent();
        gin.assign("Gin", InputManager.category2, "liquors");
        liquors.Add(gin.component, gin);

        // Rum
        DrinkComponent rum = new DrinkComponent();
        rum.assign("Rum", InputManager.category3, "liquors");
        liquors.Add(rum.component, rum);

        // Whiskey
        DrinkComponent whiskey = new DrinkComponent();
        whiskey.assign("Whiskey", InputManager.category4, "liquors");
        liquors.Add(whiskey.component, whiskey);

        // Tequila
        DrinkComponent tequila = new DrinkComponent();
        tequila.assign("Tequila", InputManager.category5, "liquors");
        liquors.Add(tequila.component, tequila);

        // Absinthe
        DrinkComponent absinthe = new DrinkComponent();
        absinthe.assign("Absinthe", InputManager.category6, "liquors");
        liquors.Add(absinthe.component, absinthe);
        #endregion


        #region           BITTERS

        // Angostura Bitters
        DrinkComponent angosturaBitters = new DrinkComponent();
        angosturaBitters.assign("Angostura Bitters", InputManager.category1, "bitters");
        bitters.Add(angosturaBitters.component, angosturaBitters);

        // Peychaud's Bitters
        DrinkComponent peychaudsBitters = new DrinkComponent();
        peychaudsBitters.assign("Peychaud's Bitters", InputManager.category2, "bitters");
        bitters.Add(peychaudsBitters.component, peychaudsBitters);

        // Campari
        DrinkComponent campari = new DrinkComponent();       
        campari.assign("Campari", InputManager.category3, "bitters");
        bitters.Add(campari.component, campari);     
        
        // Sweet Vermouth
        DrinkComponent sweetVermouth = new DrinkComponent();
        sweetVermouth.assign("Sweet Vermouth", InputManager.category4, "bitters");
        bitters.Add(sweetVermouth.component, sweetVermouth);

        // Dry Vermouth
        DrinkComponent dryVermouth = new DrinkComponent();       
        dryVermouth.assign("Dry Vermouth", InputManager.category5, "bitters");
        bitters.Add(dryVermouth.component, dryVermouth);
        #endregion


        #region        NON-ALCOHOLIC

        // Tonic
        DrinkComponent tonic = new DrinkComponent();
        tonic.assign("Tonic", InputManager.category1, "nonAlcoholic");
        nonAlcoholic.Add(tonic.component, tonic);

        // Coke
        DrinkComponent coke = new DrinkComponent();
        coke.assign("Coke", InputManager.category2, "nonAlcoholic");
        nonAlcoholic.Add(coke.component, coke);

        // Ginger Beer
        DrinkComponent gingerBeer = new DrinkComponent();
        gingerBeer.assign("Ginger Beer", InputManager.category3, "nonAlcoholic");
        nonAlcoholic.Add(gingerBeer.component, gingerBeer);

        // Lime Juice
        DrinkComponent limeJuice = new DrinkComponent();
        limeJuice.assign("Lime Juice", InputManager.category4, "nonAlcoholic");
        nonAlcoholic.Add(limeJuice.component, limeJuice);

        // Lemon Juice
        DrinkComponent lemonJuice = new DrinkComponent();
        lemonJuice.assign("Lemon Juice", InputManager.category5, "nonAlcoholic");
        nonAlcoholic.Add(lemonJuice.component, lemonJuice);

        // Margarita Mix
        DrinkComponent margaritaMix = new DrinkComponent();
        margaritaMix.assign("Margarita Mix", InputManager.category6, "nonAlcoholic");
        nonAlcoholic.Add(margaritaMix.component, margaritaMix);

        // Simple Syrup
        DrinkComponent simpleSyrup = new DrinkComponent();
        simpleSyrup.assign("Simple Syrup", InputManager.category7, "nonAlcoholic");
        nonAlcoholic.Add(simpleSyrup.component, simpleSyrup);
        #endregion


        #region           OTHER
        // Ice
        DrinkComponent ice = new DrinkComponent();
        ice.assign("Ice", InputManager.category1, "other");
        other.Add(ice.component, ice);

        // Salt Rim
        DrinkComponent saltRim = new DrinkComponent();
        saltRim.assign("Salt Rim", InputManager.category2, "other");
        other.Add(saltRim.component, saltRim);

        // Olive
        DrinkComponent olive = new DrinkComponent();
        olive.assign("Olive", InputManager.category3, "other");
        other.Add(olive.component, olive);

        // Orange Garnish
        DrinkComponent orangeGarnish = new DrinkComponent();
        orangeGarnish.assign("Orange Garnish", InputManager.category4, "other");
        other.Add(orangeGarnish.component, orangeGarnish);
        #endregion
    }


    // Allows the creation of DrinkComponents external from those created in Initialize()
    // this is useful when Order needs to build DrinkComponents that the player creates
    // I want all members of DrinkComponent to be Immutable from outside this class, and since
    // we can't use constructors when inheriting from MonoBehavior, this is the only way. Thanks Obama.
    public static DrinkComponent generateExternalDrinkComponent(KeyCode key, string category)
    {
        DrinkComponent component = new DrinkComponent();
        component.key = key;
        component.category = category;
        return component;
    }


    // when the InputManager alters the keybindings, this method needs to be called in order to
    // update all the formatted keystrings. Otherwise, keybinding hints (using keyString) won't be updated
    public static void updateKeyStrings()
    {
        foreach (KeyValuePair<string, DrinkComponent> component in glassware)
            component.Value.keyString = InputManager.getStringFromKeyCode(component.Value.key);
        foreach (KeyValuePair<string, DrinkComponent> component in beers)
            component.Value.keyString = InputManager.getStringFromKeyCode(component.Value.key);
        foreach (KeyValuePair<string, DrinkComponent> component in liquors)
            component.Value.keyString = InputManager.getStringFromKeyCode(component.Value.key);
        foreach (KeyValuePair<string, DrinkComponent> component in bitters)
            component.Value.keyString = InputManager.getStringFromKeyCode(component.Value.key);
        foreach (KeyValuePair<string, DrinkComponent> component in nonAlcoholic)
            component.Value.keyString = InputManager.getStringFromKeyCode(component.Value.key);
        foreach (KeyValuePair<string, DrinkComponent> component in other)
            component.Value.keyString = InputManager.getStringFromKeyCode(component.Value.key);
    }


    #endregion


    #region INSTANCE
    /*
     *  An instance of DrinkComponent represents a component used to construct a drink
     *  including a name, KeyCode, and category
     */

    // given a DrinkComponent, assign it the given parameters
    private void assign(string component, KeyCode key, string category)
    {
        this.component = component;
        this.category = category;
        this.key = key;
        this.keyString = InputManager.getStringFromKeyCode(key);
    }

    #endregion
}
