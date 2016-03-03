using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;



public class kinkkuHefe : PhysicsGame
{
	List<PhysicsObject> ainekset = new List<PhysicsObject>();
	List<PhysicsObject> tuoteLista = new List<PhysicsObject> ();

	// TAUSTAKUVAT
	Image taustakuvaEkaSiirtyma = LoadImage("keittioAlku"); 	// Ladataan keittiö taustaksi ensimmäiseen siirtymään päävalikon jälkeen.
	Image keittiokohtaus = LoadImage("KEITTIO_V6.jpg"); 			// Ladataan todellisen toiminnan aikainen näkymä.

	int pisteenLasku = 0; // Pisteet kertyy tähän
	int tuotteidenMaara = 4;


	List<String> lisattyKinkkuunString = new List<String> (); //Lisätyt tuotteet
	// ALKUVALIKON KOHDAT LISTA
	//List<Label> valikonKohdat;

	// OBJEKTIT
	PhysicsObject kinkku;
	PhysicsObject elamansuola;
	PhysicsObject hksininen;
	PhysicsObject jackdaniels;
	PhysicsObject kebabkastike;
	PhysicsObject lanttu;
	PhysicsObject kossu;
	PhysicsObject mandariini;
	PhysicsObject marsipaani;
	PhysicsObject rakuuna;
	PhysicsObject msmjauhe;
	PhysicsObject mustaherukka;
	PhysicsObject mustakitaturska;
	PhysicsObject mustapippuri;
	PhysicsObject sukkahousut;
	PhysicsObject tilli;

	PhysicsObject logo;				// Logo fysiikkaolioksi




	public override void Begin ()
	{
		SetWindowSize (1920, 1080);								// Ikkunan koko sama kuin taustakuvien resoluutio
		IsFullScreen = true; 									// Peli asetetaan kokonäytölle.	
		Camera.ZoomToLevel (100);								// Koko tausta näkyvillä.
		Mouse.IsCursorVisible = true; 							// Hiiri näkyviin.
		SmoothTextures = false;									// Reunojen pehmennys pois käytöstä.
		Level.Background.Image = keittiokohtaus; 					// Ladataan keittiöstä kuva pelin taustaksi.
		Valikko ();												// Kutsutaan valikkoa heti alkuun, niin ei tarvitse pelaajan ESCiä painella.
	}


		void PeliKayntiin()
		{
		Remove (logo);	
		//ClearAll(); 											// Tyhjennetään kenttä kaikista objekteista
		pisteenLasku = 0;
		Level.Background.Image = keittiokohtaus; 					// Ladataan keittiöstä kuva pelin taustaksi
			Ainekset(ainekset);										// Lisätään ainekset kentälle, kun on valittu, että lähdetään paistamaan kinkkua.

			// HIIREN KÄYTTÖ OBJEKTIEN LIIKUTTELUUN & TUTKIMISEEN
			//Mouse.Listen (MouseButton.Left, ButtonState.Pressed, KuunteleLiiketta2, "Jos ei koordinaatio riitä ;D");
			Mouse.Listen (MouseButton.Left, ButtonState.Down, OnkoJoValmista, "Lisää aineksia kinkkuun mausteeksi.");
			Mouse.Listen (MouseButton.Left, ButtonState.Released, OnkoKinkunPaalla, null);

			// VALIKKOON MENEMINEN
			Keyboard.Listen (Key.Escape, ButtonState.Pressed, Valikko, "Avaa valikko");
		}

	void Valikko(){
		ClearAll(); 											// Tyhjennetään kenttä kaikista objekteista
		MultiSelectWindow valikko = new MultiSelectWindow("", "JOO JOO KINKKUU TULILLE", "HALL OF KINKKUHEFE", "SYÖN MIELUUMMIN ANANASPIZZAA...");
		Level.Background.Image = taustakuvaEkaSiirtyma; 				// Ladataan keittiöstä kuva valikon taustaksi
		Add(valikko);


		//HANDLERIT
		valikko.DefaultCancel = 3;								// Kolmas nappi poistuu pelistä, kuten ESC -> YES
		valikko.AddItemHandler(2, Exit);						// Poistu pelistä, kun nappia kaksi painetaan
		valikko.AddItemHandler(0, PeliKayntiin);				// Aloittaa pelin kun ylintä nappia painetaan
		valikko.AddItemHandler(1, HallOfKinkkuhefe);			// Näytetään top-score listaus 


		logo = PhysicsObject.CreateStaticObject(923, 538);		// Logo alkuvalikon taustalle
		logo.Image = LoadImage("hefeLogo");						// Logokuva
		logo.Position = new Vector (0, -50);
		Add (logo, 0);
	}

	void HallOfKinkkuhefe()
	{
		// Fetchaa suoraa koneen käyttäjän nimi joka on oletuksena topscore nicki
	}

	void KuunteleLiiketta2()
	{
		MessageDisplay.Add ("TARTU KUIN MIES!");
		MessageDisplay.MaxMessageCount = 0;
	}

	void Ainekset(List<PhysicsObject> ainekset){

		// MAUSTEET / AINEKSET OBJEKTEIKSI
		// THÖ KINKKU
		kinkku = PhysicsObject.CreateStaticObject (Level.Width * 0.23, Level.Height * 0.13);
		kinkku.Image = LoadImage ("kinkku");							// Lisätään kinkku
		kinkku.X = -230;
		kinkku.Y = -35;
		kinkku.Angle = Angle.FromDegrees (30);
		Add (kinkku);

		elamansuola = new PhysicsObject (Level.Width * 0.08, Level.Height * 0.1, Shape.Circle);
		elamansuola.Image = LoadImage("elamansuola"); 				// Lisätään suolapurkki
		elamansuola.X = 30;
		elamansuola.Y = 0;
		elamansuola.Tag = "aines";
		tuoteLista.Add (elamansuola);
		Add (elamansuola);

		hksininen = new PhysicsObject (Level.Width * 0.1, Level.Height * 0.05);
		hksininen.Image = LoadImage("hksininen"); 					// Lisätään HK:n sininen eli makkara
		hksininen.X = 240;
		hksininen.Y = -60;
		hksininen.Tag = "aines";
		tuoteLista.Add (hksininen);
		Add (hksininen);


		jackdaniels = new PhysicsObject (Level.Width * 0.07, Level.Height * 0.25);
		jackdaniels.Image = LoadImage("jackdaniels"); 				// Lisätään Jack Daniels viskipullo
		jackdaniels.X = 130;
		jackdaniels.Y = 30;
		jackdaniels.Tag = "aines";
		tuoteLista.Add (jackdaniels);
		Add (jackdaniels);

		kebabkastike = new PhysicsObject (Level.Width * 0.1, Level.Height * 0.2);
		kebabkastike.Image = LoadImage("kebabkastike"); 			// Lisätään kebabkastikepurkit 
		kebabkastike.X = 300;
		kebabkastike.Y = 30;
		kebabkastike.Tag = "aines";
		tuoteLista.Add (kebabkastike);
		Add (kebabkastike);

		lanttu = new PhysicsObject (Level.Width * 0.15, Level.Height * 0.15);
		lanttu.Image = LoadImage("lanttu"); 						// Lisätään kolmen lanttua
		lanttu.X = 450;
		lanttu.Y = -40;
		lanttu.Tag = "aines";
		tuoteLista.Add (lanttu);
		Add (lanttu);

		mustakitaturska = new PhysicsObject (Level.Width * 0.25, Level.Height * 0.1);
		mustakitaturska.Image = LoadImage("mustakitaturska"); 							// Lisätään Mustakita turska
		mustakitaturska.X = 600;
		mustakitaturska.Y = 30;
		mustakitaturska.Tag = "aines";
		tuoteLista.Add (mustakitaturska);
		Add (mustakitaturska);



		// PELIN LOPETTAMINEN
		PhoneBackButton.Listen (ConfirmExit, "Lopeta peli");
		Keyboard.Listen (Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
	}

	void OnkoJoValmista(){
		if (lisattyKinkkuunString.Count >= 4) {
			Widget ruutu1 = new Widget (100.0, 50.0);
			Label lisatytmausteet = new Label ("Hei sul ois jo tarpeeks aineita");
			ruutu1.Add (lisatytmausteet);
			Add (ruutu1);
		} else {
			KuunteleLiiketta ();
		}

	}

	void KuunteleLiiketta2(AnalogState hiirenTila)
	{
		int kohtausNumero = 0;
		foreach (PhysicsObject sana in tuoteLista) {

			//string apu = sana.ToString();
			if (Mouse.IsCursorOn (sana)) {
				kohtausNumero = tuoteLista.IndexOf (sana);
				break;
			}
		}
			switch(kohtausNumero){
			case 0:
				elamansuola.Position = Mouse.PositionOnWorld;

				MessageDisplay.Add ("Käytä ensi kerralla Himalajan suolaa");
				MessageDisplay.MaxMessageCount = 0;
				
				break;
			case 1:
				hksininen.Position = Mouse.PositionOnWorld;

				MessageDisplay.Add ("Vähä kyrsää... Brus suomalaista!");
				MessageDisplay.MaxMessageCount = 0;
				break;
	}
		}



	void OnkoKinkunPaalla(){			//Suolan Lisäys kinkkuun
		if (Mouse.IsCursorOn (kinkku) && Mouse.IsCursorOn (elamansuola)) {
			MultiSelectWindow suolaValikko = new MultiSelectWindow ("paljonko laitetaan?", "kolme kuppii", "reippahasti käypi askel", "10 metrii"); 
			elamansuola.Destroy ();
			Add (suolaValikko);
			lisattyKinkkuunString.Add ("elämänsuola");
			MessageDisplay.Clear ();
			suolaValikko.ItemSelected += SuolanValinnat;
			int i = suolaValikko.SelectedIndex;

		} else if (Mouse.IsCursorOn (kinkku) && Mouse.IsCursorOn (jackdaniels)) {
			MultiSelectWindow suolaValikko = new MultiSelectWindow ("paljonko laitetaan?", "Ujosti", "puolet meni kokkiin", "no alkosta saa lisää"); 
			jackdaniels.Destroy ();
			MessageDisplay.Clear ();
			lisattyKinkkuunString.Add ("Jack Daniels");
			suolaValikko.ItemSelected += SuolanValinnat;
			Add (suolaValikko);
			int i = suolaValikko.SelectedIndex;
			if (i == 0) {
				pisteenLasku = pisteenLasku + 1;
			} else if (i == 1) {
				pisteenLasku += 3;
			}
		
		}

		 else if (Mouse.IsCursorOn (kinkku) && Mouse.IsCursorOn (hksininen)) {
			MultiSelectWindow suolaValikko = new MultiSelectWindow ("paljonko laitetaan?", "yks kyrsä", "metri Heikki", "Palomihetki on kateellisii"); 
			hksininen.Destroy ();
			MessageDisplay.Clear ();
			lisattyKinkkuunString.Add ("HK-Sininen");
			suolaValikko.ItemSelected += SuolanValinnat;
			Add (suolaValikko);
			int i = suolaValikko.SelectedIndex;

			if (i == 0) {
				pisteenLasku = pisteenLasku + 1;
			} else if (i == 1) {
				pisteenLasku += 3;
			}
		}

		else if (Mouse.IsCursorOn (kinkku) && Mouse.IsCursorOn (kebabkastike)) {
			MultiSelectWindow suolaValikko = new MultiSelectWindow ("paljonko laitetaan?", "Liraus", "loraus", "NO nyt on makuu"); 
			kebabkastike.Destroy ();
			MessageDisplay.Clear ();
			lisattyKinkkuunString.Add ("kebakastike");
			suolaValikko.ItemSelected += SuolanValinnat;
			Add (suolaValikko);
			int i = suolaValikko.SelectedIndex;

			if (i == 0) {
				pisteenLasku = pisteenLasku + 2;
			} else if (i == 1) {
				pisteenLasku += 3;
			} else if (i == 2) {
				pisteenLasku += 1;
			}
		}
		else if (Mouse.IsCursorOn (kinkku) && Mouse.IsCursorOn (mustakitaturska)) {
			MultiSelectWindow suolaValikko = new MultiSelectWindow ("paljonko laitetaan?", "Kastikkeen mauste", "vähän eksoottisempi versio", "Jaa et kalakeittoo vai"); 
			mustakitaturska.Destroy ();
			MessageDisplay.Clear ();
			lisattyKinkkuunString.Add ("mustakitaturska");
			suolaValikko.ItemSelected += SuolanValinnat;
			Add (suolaValikko);
			int i = suolaValikko.SelectedIndex;


		}

	}
	void SuolanValinnat(int i){


		switch (i)
		{
		case 0 :
			MessageDisplay.Add ("nössösti lisätty");
			MessageDisplay.MaxMessageCount = 1;
			break;
		case 1:
			MessageDisplay.Add ("Voi veljet");
			MessageDisplay.MaxMessageCount = 1;
			break;
		case 2:
			MessageDisplay.Add ("nö älä nyt innostu");
			MessageDisplay.MaxMessageCount = 1;
			break;
			}
		}



	// HIIREN KUUNTELU ELI MITÄ TAPAHTUU KUN VASEN HIIRI ON PAINETTU POHJAAN
	void KuunteleLiiketta()
	{   

		if (Mouse.IsCursorOn(elamansuola)) 
			{
			elamansuola.Position = Mouse.PositionOnWorld;
				
				MessageDisplay.Add( "Oos nyt sit varovainen sen kanssa" );
				MessageDisplay.MaxMessageCount = 0;
			return;
			}

			else if (Mouse.IsCursorOn(hksininen)) 
			{
			hksininen.Position = Mouse.PositionOnWorld;
				
				MessageDisplay.Add( "Vähä kyrsää... Brus suomalaista!" );
				MessageDisplay.MaxMessageCount = 0;
			return;
			}

			else if (Mouse.IsCursorOn(jackdaniels)) 
			{
			jackdaniels.Position = Mouse.PositionOnWorld;
				

				MessageDisplay.Add( "Sullahan on ihan kehittynyt maku." );
				MessageDisplay.MaxMessageCount = 0;
			return;
			}

			else if (Mouse.IsCursorOn(lanttu)) 
			{
			lanttu.Position = Mouse.PositionOnWorld;
				

				MessageDisplay.Add( "Vähä kyrsää... Brus suomalaista!" );
				MessageDisplay.MaxMessageCount = 0;
			} 

		else if (Mouse.IsCursorOn(mustakitaturska)) 
			{
			mustakitaturska.Position = Mouse.PositionOnWorld;

				MessageDisplay.Add( "Jaa no varmaan Gordon Ramsy ois ottanu saamaa" );
				MessageDisplay.MaxMessageCount = 0;
			}

		else if (Mouse.IsCursorOn(kebabkastike)) 
		{
			kebabkastike.Position = Mouse.PositionOnWorld;

			MessageDisplay.Add( "No nyt on kyllä gurmeé mauste" );
			MessageDisplay.MaxMessageCount = 0;
		}





	}



}

