using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkComponent : MonoBehaviour
{

    public static Dictionary<string, DrinkComponent> glassware;
    public static Dictionary<string, DrinkComponent> liquors;
    public static Dictionary<string, DrinkComponent> beers;
    public static Dictionary<string, DrinkComponent> bittersAndSyrups;
    public static Dictionary<string, DrinkComponent> otherMixers;

    public string component;        // name of the drink component (typed nicely for front-end)
    public string category;         // the drinkComponent category this component belongs in
    public string keySequence;      // the keySequence required to properly add this ingredient to a drink


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
        // initialize and populate glassware
        glassware = new Dictionary<string, DrinkComponent>();
        DrinkComponent pintGlass = new DrinkComponent();
        DrinkComponent mixedDrinkGlass = new DrinkComponent();
        DrinkComponent cocktailGlass = new DrinkComponent();
        DrinkComponent copperCup = new DrinkComponent();

        pintGlass.assign("Pint Glass", "glassware", "a");
        mixedDrinkGlass.assign("Mixed Drink Glass", "glassware", "a");
        cocktailGlass.assign("Cocktail Glass", "glassware", "a");
        copperCup.assign("Copper Cup", "glassware", "a");

        glassware.Add(pintGlass.component, pintGlass);
        glassware.Add(mixedDrinkGlass.component, mixedDrinkGlass);
        glassware.Add(cocktailGlass.component, cocktailGlass);
        glassware.Add(copperCup.component, copperCup);


        // initialize and populate liquors
        liquors = new Dictionary<string, DrinkComponent>();
        DrinkComponent vodka = new DrinkComponent();
        DrinkComponent gin = new DrinkComponent();
        DrinkComponent rum = new DrinkComponent();
        DrinkComponent whiskey = new DrinkComponent();
        DrinkComponent tequila = new DrinkComponent();
        DrinkComponent absinthe = new DrinkComponent();

        vodka.assign("Vodka", "liquors", "a");
        gin.assign("Gin", "liquors", "a");
        rum.assign("Rum", "liquors", "a");
        whiskey.assign("Whiskey", "liquors", "a");
        tequila.assign("Tequila", "liquors", "a");
        absinthe.assign("Absinthe", "liquors", "a");

        liquors.Add(vodka.component, vodka);
        liquors.Add(gin.component, gin);
        liquors.Add(rum.component, rum);
        liquors.Add(whiskey.component, whiskey);
        liquors.Add(tequila.component, tequila);
        liquors.Add(absinthe.component, absinthe);


        // initialize and populate beers
        beers = new Dictionary<string, DrinkComponent>();
        DrinkComponent lager = new DrinkComponent();
        DrinkComponent ale = new DrinkComponent();

        lager.assign("Lager", "beers", "a");
        ale.assign("Ale", "beers", "a");

        beers.Add(lager.component, lager);
        beers.Add(ale.component, ale);


        // initialize and populate bittersAndSyrups
        bittersAndSyrups = new Dictionary<string, DrinkComponent>();
        DrinkComponent angosturaBitters = new DrinkComponent();
        DrinkComponent peychaudsBitters = new DrinkComponent();
        DrinkComponent campari = new DrinkComponent();       
        DrinkComponent sweetVermouth = new DrinkComponent();
        DrinkComponent dryVermouth = new DrinkComponent();       
        DrinkComponent simpleSyrup = new DrinkComponent();

        angosturaBitters.assign("Angostura Bitters", "bittersAndSyrups", "a");
        peychaudsBitters.assign("Peychaud's Bitters", "bittersAndSyrups", "a");
        campari.assign("Campari", "bittersAndSyrups", "a");       
        sweetVermouth.assign("Sweet Vermouth", "bittersAndSyrups", "a");
        dryVermouth.assign("Dry Vermouth", "bittersAndSyrups", "a");       
        simpleSyrup.assign("Simple Syrup", "bittersAndSyrups", "a");

        bittersAndSyrups.Add(angosturaBitters.component, angosturaBitters);
        bittersAndSyrups.Add(peychaudsBitters.component, peychaudsBitters);
        bittersAndSyrups.Add(campari.component, campari);      
        bittersAndSyrups.Add(sweetVermouth.component, sweetVermouth);
        bittersAndSyrups.Add(dryVermouth.component, dryVermouth);       
        bittersAndSyrups.Add(simpleSyrup.component, simpleSyrup);


        // initialize and populate otherMixers
        otherMixers = new Dictionary<string, DrinkComponent>();
        DrinkComponent ice = new DrinkComponent();
        DrinkComponent saltRim = new DrinkComponent();
        DrinkComponent gingerBeer = new DrinkComponent();
        DrinkComponent limeJuice = new DrinkComponent();
        DrinkComponent lemonJuice = new DrinkComponent();
        DrinkComponent orangeJuice = new DrinkComponent();
        DrinkComponent margaritaMix = new DrinkComponent();
        DrinkComponent champagne = new DrinkComponent();
        DrinkComponent orangeGarnish = new DrinkComponent();
        DrinkComponent sugar = new DrinkComponent();
        DrinkComponent olive = new DrinkComponent();
        DrinkComponent soda = new DrinkComponent();

        ice.assign("Ice", "otherMixers", "a");
        saltRim.assign("Salt Rim", "otherMixers", "a");
        gingerBeer.assign("Ginger Beer", "otherMixers", "a");
        limeJuice.assign("Lime Juice", "otherMixers", "a");
        lemonJuice.assign("Lemon Juice", "otherMixers", "a");
        orangeJuice.assign("Orange Juice", "otherMixers", "a");
        margaritaMix.assign("Margarita Mix", "otherMixers", "a");
        champagne.assign("Champagne", "otherMixers", "a");
        orangeGarnish.assign("Orange Garnish", "otherMixers", "a");
        sugar.assign("Sugar", "otherMixers", "a");
        olive.assign("Olive", "otherMixers", "a");
        soda.assign("Soda", "otherMixers", "a");

        otherMixers.Add(ice.component, ice);
        otherMixers.Add(saltRim.component, saltRim);
        otherMixers.Add(gingerBeer.component, gingerBeer);
        otherMixers.Add(limeJuice.component, limeJuice);
        otherMixers.Add(lemonJuice.component, lemonJuice);
        otherMixers.Add(orangeJuice.component, orangeJuice);
        otherMixers.Add(margaritaMix.component, margaritaMix);
        otherMixers.Add(champagne.component, champagne);
        otherMixers.Add(orangeGarnish.component, orangeGarnish);
        otherMixers.Add(sugar.component, sugar);
        otherMixers.Add(olive.component, olive);
        otherMixers.Add(soda.component, soda);
    }


    public void assign(string component, string category, string keySequence)
    {
        this.component = component;
        this.category = category;
        this.keySequence = keySequence;
    }
}
