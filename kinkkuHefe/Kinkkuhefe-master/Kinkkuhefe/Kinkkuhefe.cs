using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

public class Kinkkuhefe : PhysicsGame
{
	
	// TAUSTAKUVAT
	Image valikkoTausta = LoadImage("aloitusValikko"); 								// Ladataan keittiöstä kuva alkuvalikon taustaksi.
	Image pelinTausta = LoadImage("pelinTausta"); 									// Ladataan todellisen toiminnan aikainen näkymä.
	Image kinkkuUunissa = LoadImage("kinkkuUunissa");								// Ladataan kuva kinkusta uunissa.
	Image poyta = LoadImage("poyta");												// Ladataan kuva pelkästä pöydästä.

	// HOK = Hall Of Kinkkuhefe
	ScoreList HOK = new ScoreList(5, false, 0);										// Listalle mahtuu 5 parasta, parhaat pisteet ensin, 0 minimi aluksi.

	// KINKKUUN LISATTYJEN AINESTEN MÄÄRÄ
	int kuinkaMonta = 3;

	// PELAAJAN PISTEET
	int pisteidenLasku = 0;

	// PARI LISTAA
	List<PhysicsObject> ainekset = new List<PhysicsObject>();						// Thö kinkun & aineksien lista
	List<PhysicsObject> kinkkuunLisatyt = new List<PhysicsObject>();				// Aineslista kinkkuun lisätyistä
	List<String> lisattyKinkkuunString = new List<String> (); 						// Kinkkuun lisättyjen tuotteiden lista

	// OBJEKTIT
	PhysicsObject kinkku;					// 1.
	PhysicsObject elamansuola;				// 2.
	PhysicsObject hksininen; 				// 3.
	PhysicsObject jackdaniels;				// 4.
	PhysicsObject kebabkastike;				// 5.
	PhysicsObject lanttu;					// 6.
	PhysicsObject kossu;					// 7.
	PhysicsObject mandariini;				// 8.
	PhysicsObject marsipaani;				// 9.
	PhysicsObject rakuuna;					// 10.
	PhysicsObject msmjauhe;					// 11. 
	PhysicsObject mustaherukka;				// 12.
	PhysicsObject mustakitaturska;			// 13.
	PhysicsObject mustapippuri;				// 14.
	PhysicsObject sukkahousut;				// 15.
	PhysicsObject tilli;					// 16.

	// TEHDÄÄN LOGOISTA JA KINKUISTA OBJEKTEJA, JOTTA NE SAA NÄPPÄRÄSTI POIS RUUDULTA TARVITTAESSA
	PhysicsObject radio;					// Lisätään radio ihan taustalle.
	PhysicsObject logo;						// Logo fysiikkaolioksi
	PhysicsObject logoTeksti;				// Logoteksti fysiikkaolioksi
	PhysicsObject hallOfKinkkuhefe;			// Tekstit Hall of Fameen
	PhysicsObject kinkkuKylma;				// Kylmäks jäänyt kinkku
	PhysicsObject kinkkuSopiva;				// Sopivaksi paistunut kinkku
	PhysicsObject kinkkuPalanut; 			// Palaneeksi asti uunissa ollut kinkku


	// LUODAAN ESC:illä AVATTAVA VALIKKO 
	void Valikko()
	{
		
		// TYHJENNETÄÄN TURHAT & NOLLATAAN
		ClearGameObjects ();														// Ettei jää nappulat ruudulle
		ClearTimers ();																// Nollataan ajastimet
		MessageDisplay.Clear();														// Tyhjennetään tekstiruutu edellisestä viisastelusta.
		lisattyKinkkuunString.Clear();												// NOLLATAAN SE MITÄ KINKKUUN EDELLISESSÄ PELISSÄ LISÄTTIIN

		// PÄÄVALIKKO
		MultiSelectWindow valikko = new MultiSelectWindow("", "JOO JOO KINKKUU TULILLE", "HALL OF KINKKUHEFE", "SYÖN MIELUUMMIN ANANASPIZZAA...");
		Level.Background.Image = pelinTausta; 										// Ladataan keittiöstä kuva valikon taustaksi
		Add(valikko);

		// PELKKÄ LOGON TEKSTI JÄÄ ALKUVALIKKOON
		logoTeksti = PhysicsObject.CreateStaticObject(923, 538);					// Logo alkuvalikon taustalle
		logoTeksti.Image = LoadImage("hefeLogoTeksti");								// Logoteksti
		logoTeksti.Position = new Vector (0, -100);									// Logokuvan sijainti ruudulla
		Add (logoTeksti, 0);														// Lisätään KH logo 0:een kerrokseen

		//HANDLERIT
		valikko.DefaultCancel = 3;													// Kolmas nappi poistuu pelistä, kuten ESC -> YES
		valikko.AddItemHandler(2, Exit);											// Poistu pelistä, kun nappia kaksi painetaan
		valikko.AddItemHandler(0, PeliKayntiin);									// Aloittaa pelin kun ylintä nappia painetaan
		valikko.AddItemHandler(1, HallOfKinkkuhefe);								// Näytetään top-score listaus 

	}
		

	// PÄÄOHJELMA
	public override void Begin ()
	{
		
		// YLEISET IKKUNAN ASETUKSET & MUUT LATAAMISET
		SetWindowSize(1920, 1080);													// Ikkunan koko sama kuin taustakuvien resoluutio
		IsFullScreen = true; 														// Peli asetetaan kokonäytölle.	
		Camera.ZoomToLevel(100);													// Koko tausta näkyvillä.
		Mouse.IsCursorVisible = true; 												// Hiiri näkyviin.
		SmoothTextures = false;														// Reunojen pehmennys pois käytöstä.
		Level.Background.Image = valikkoTausta; 									// Ladataan pelistä tausta


		// LOGO NÄKYVIIN 
		logo = PhysicsObject.CreateStaticObject(923, 538);							// Logo alkuvalikon taustalle
		logo.Image = LoadImage("hefeLogo");											// Logokuva
		logo.Position = new Vector (0, -100);										// Logokuvan sijainti ruudulla
		Add (logo, 0);																// Lisätään KH logo 0:een kerrokseen

		// ALOITUKSEN AJASTIN
		Timer aikaa = new Timer();
		aikaa.Start(1);
		aikaa.Interval = 2.5;
		aikaa.Timeout += Valikko;

	}
		

	// ITSE PELIIN
	void PeliKayntiin()
	{
		
		// TYHJENNETÄÄN TURHAT
		ClearGameObjects ();														// Ettei jää nappulat ruudulle
		ClearTimers ();																// Nollataan ajastimet
		MessageDisplay.Clear();														// Tyhjennetään tekstiruutu edellisestä viisastelusta.

		Level.Background.Image = pelinTausta; 										// Ladataan keittiöstä kuva pelin taustaksi
		Ainekset(ainekset);															// Lisätään ainekset kentälle, kun on valittu, että lähdetään paistamaan kinkkua.

		// HIIREN KÄYTTÖ OBJEKTIEN LIIKUTTELUUN & TUTKIMISEEN
		Mouse.Listen (MouseButton.Left, ButtonState.Down, OnkoJoValmista, "Jos kinkussa on jo tarpeeksi aineksia / Lisää aineksia kinkkuun mausteeksi.");
		Mouse.Listen (MouseButton.Left, ButtonState.Released, OnkoKinkunPaalla, null);

		// VALIKKOON MENEMINEN
		Keyboard.Listen (Key.Escape, ButtonState.Pressed, Valikko, "Avaa valikko");

	}


	// PISTEIDEN TALLENNUS HALL OF KINKKUHEFEÄ VARTEN
	void TallennaPisteet(Window sender)
	{
		
		DataStorage.Save<ScoreList>(HOK, "pojot.xml");

	}


	// HIGHSCORE TAULUKKO
	void HallOfKinkkuhefe()
	{
		
		HOK = DataStorage.TryLoad<ScoreList>(HOK, "pojot.xml" );					// Hall of fame listaukselle data
		// TYHJENNETÄÄN TURHAT
		ClearGameObjects ();														// Ettei jää nappulat ruudulle
		ClearTimers ();																// Nollataan ajastimet
		//MessageDisplay.Clear();														// Tyhjennetään tekstiruutu edellisestä viisastelusta.

		// LOGO NÄKYVIIN 
		hallOfKinkkuhefe = PhysicsObject.CreateStaticObject(923, 700);				// Logo alkuvalikon taustalle
		hallOfKinkkuhefe.Image = LoadImage("HallOfKinkkuhefe");						// Logokuva
		hallOfKinkkuhefe.Position = new Vector (0, -19);							// Logokuvan sijainti ruudulla
		Add (hallOfKinkkuhefe, 0);													// Lisätään KH logo 0:een kerrokseen



		// VALIKKOON MENEMINEN
		Keyboard.Listen (Key.Escape, ButtonState.Pressed, Valikko, "Avaa valikko");

	}
		

	// KINKKUU UUNIIN
	void KinkkuUuniin()
	{
		
		// TYHJENNETÄÄN TURHAT
		ClearGameObjects ();														// Ettei jää nappulat ruudulle
		ClearTimers ();																// Nollataan ajastimet
		MessageDisplay.Clear();														// Tyhjennetään tekstiruutu edellisestä viisastelusta.
	//	lisattyKinkkuunString.Clear();

		MessageDisplay.Clear();														// Tyhjennetään tekstiruutu edellisestä viisastelusta.
		MultiSelectWindow kinkunPaisto = new MultiSelectWindow("Pitkäänkö ajattelit paistaa?", "Nopeesti vaan ku on jo nälkä!",
															   "Kai semmonen reipas kolmisen tuntia riittää.", "Jätän yön yli paistuun.");
		Add(kinkunPaisto);
		
		Level.Background.Image = kinkkuUunissa; 									// Kuva kinkusta uunissa


		//HANDLERIT
		kinkunPaisto.AddItemHandler(0, KylmaKinkku);								// Lyhyt paisto
		kinkunPaisto.AddItemHandler(1, SopivaKinkku);								// Juuri se oikea aika
		kinkunPaisto.AddItemHandler(2, PalanutKinkku);								// Nyt menee jo överiks :-D

	}


	// KYLMÄKSI JÄÄNYT KINKKU
	void KylmaKinkku()
	{

		ClearGameObjects ();														// Ettei jää nappulat ruudulle
		Level.Background.Image = poyta; 											// Kuva pöydästä ja näytetään vielä ne objektit, jotka meni kinkkuun

		Label loppuvinoilu = new Label("Mutsiski oli lämpimämpi viime kesänä.");
		loppuvinoilu.Y = Screen.Top - 100;
		loppuvinoilu.TextColor = Color.Blue;
		Add(loppuvinoilu);

		kinkkuKylma = PhysicsObject.CreateStaticObject(Level.Width * 0.5, Level.Height * 0.35);
		kinkkuKylma.Image = LoadImage("kinkkuKylma");								// Lisätään kylmäksi jäänyt kinkku
		kinkkuKylma.Position = new Vector (-20, -170);



		Add (kinkkuKylma, 0);

		MitaLisatty ();

		Timer aikaa = new Timer();
		aikaa.Start(1);
		aikaa.Interval = 5;
		aikaa.Timeout += MitaLisatty;												// Näytetään pelaajalle mitä hän lisäsi


	}


	// SOPIVASTI PAISTUNUT KINKKU
	void SopivaKinkku()
	{

		ClearGameObjects ();														// Ettei jää nappulat ruudulle
		Level.Background.Image = poyta; 											// Kuva pöydästä ja näytetään vielä ne objektit, jotka meni kinkkuun
		
		Label loppuvinoilu = new Label("Täähän näyttää ihan sopivasti paistetulta.");
		loppuvinoilu.Y = Screen.Top - 100;
		loppuvinoilu.TextColor = Color.Gold;
		Add(loppuvinoilu);

		MitaLisatty ();

		kinkkuSopiva = PhysicsObject.CreateStaticObject(Level.Width * 0.5, Level.Height * 0.35);
		kinkkuSopiva.Image = LoadImage("kinkkuSopiva");								// Lisätään sopivaksi kypsennetty kinkku
		kinkkuSopiva.Position = new Vector (-20, -170);
		Add (kinkkuSopiva, 0);



		Timer aikaa = new Timer();
		aikaa.Start(1);
		aikaa.Interval = 5;
		aikaa.Timeout += MitaLisatty;												// Näytetään pelaajalle mitä hän lisäsi



	}


	// AIVAN PALANUT KINKKU
	void PalanutKinkku()
	{

		ClearGameObjects ();														// Ettei jää nappulat ruudulle
		Level.Background.Image = poyta; 											// Kuva pöydästä ja näytetään vielä ne objektit, jotka meni kinkkuun
		
		Label loppuvinoilu = new Label("Jos saat samalla mitalla ku tää kinkku niin palat helvetin liekeissä!");
		loppuvinoilu.Y = Screen.Top - 100;
		loppuvinoilu.TextColor = Color.Black;
		Add(loppuvinoilu);

		MitaLisatty ();

		kinkkuPalanut = PhysicsObject.CreateStaticObject(Level.Width * 0.5, Level.Height * 0.35);
		kinkkuPalanut.Image = LoadImage("kinkkuPalanut");							// Lisätään palanut kinkku
		kinkkuPalanut.Position = new Vector (-20, -170);
		Add (kinkkuPalanut, 0);



		Timer aikaa = new Timer();
		aikaa.Start(1);
		aikaa.Interval = 5;
		aikaa.Timeout += MitaLisatty;												// Näytetään pelaajalle mitä hän lisäsi

	}


	// MITÄ PELAAJA LISÄSI KINKUN SEKAAN
	void MitaLisatty()
	{
		int k = -20;
		int apu = 0;
		foreach (PhysicsObject lisa in kinkkuunLisatyt) {
			lisa.X = -500;
			Add (lisa);
		}

		if (pisteidenLasku <= 4) {
			if (lisattyKinkkuunString.Contains ("sukkahousut")) {
				Label tekstikentta = new Label (500, 100, "villahousut on tillilihamauste ei kinkun");
				tekstikentta.Color = Color.White;
				tekstikentta.Y = -380;
				Add (tekstikentta);
			} else {
				Label tekstikentta = new Label (400, 100, "No kinkku ainakin maistuu \n mut vaatii kyl sinappii");
				tekstikentta.Color = Color.White;
				tekstikentta.Y = -380;
				Add (tekstikentta);
			}
		} else if (pisteidenLasku >= 5 && pisteidenLasku <= 6) {
			Label tekstikentta = new Label (700, 100, "Jes ihan niinkun mummon tekmää aikoinaan");
			tekstikentta.Color = Color.White;
			tekstikentta.Y = -380;
			Add (tekstikentta);
		} else if (pisteidenLasku >= 7) {
			if (lisattyKinkkuunString.Contains ("Jack Daniels viskiä")) {
				Label tekstikentta = new Label (700, 100, "jaa ettei ois vaan lotrattu danielssin kanssa :/");
				tekstikentta.Color = Color.White;
				tekstikentta.Y = -380;
				Add (tekstikentta);
			} else {
				Label tekstikentta = new Label (700, 100, "no on aika eksoottista ja kinkkukin vois maistuu enemmän");
				tekstikentta.Color = Color.White;
				tekstikentta.Y = -380;
				Add (tekstikentta);
			}
		}


		Timer aikaa = new Timer();
		aikaa.Start(1);
		aikaa.Interval = 5;
		aikaa.Timeout += AddToKinkkuFame;
	}

	void AddToKinkkuFame() {
		HighScoreWindow hallOfKinkku = new HighScoreWindow("", "Pääsit kinkunpaiston all-staareihin pistein %p! Anna nickisi:", HOK, pisteidenLasku);
		hallOfKinkku.NameInputWindow.Message.Text = "Onneksi olkoon! Sait {0:0} pistettä. Anna nimesi";
		hallOfKinkku.List.ScoreFormat = "{0:0}";
		hallOfKinkku.Closed += TallennaPisteet;
		Add(hallOfKinkku);

		hallOfKinkku.Closed += delegate (Window sender) 
		{
			Valikko();
		};
	}


	// KUN HALUAT LISÄTÄ VIELÄ MAUSTEEN
	void LisaMauste()
	{
		
		if (kuinkaMonta == 4) 
		{
			MessageDisplay.Clear ();

			MessageDisplay.Add("Nyt menee jo kinkku uuniin!");
			Timer aikaa = new Timer();
			aikaa.Start(1);
			aikaa.Interval = 2;
			aikaa.Timeout += KinkkuUuniin;
		}

		else 
		{
			kuinkaMonta++;
		}

	}


	// ONKO KINKUSSA TARPEEKS AINEKSIA
	void OnkoJoValmista()
	{
		MessageDisplay.Clear ();													// Tyhjennetään tekstiruutu edellisestä viisastelusta.

		if (lisattyKinkkuunString.Count >= kuinkaMonta) 
		{
			MultiSelectWindow kinkkuUuniinValikko = new MultiSelectWindow ("Mausteiden puolesta aika laittaa kinkku uuniin.", 
				"Kinkkuu uuniin!", "Vielä vähän lisää mausteita...", "Pidä kinkkus!");
			Add (kinkkuUuniinValikko);

			//HANDLERIT
			kinkkuUuniinValikko.AddItemHandler(0, KinkkuUuniin);					// Kinkku menee uuniin paistumaan
			kinkkuUuniinValikko.AddItemHandler(1, LisaMauste);						// Jatketaan maustamista
			kinkkuUuniinValikko.AddItemHandler(2, Valikko);							// Poistuu pelistä alkuun
		}

		else 
		{
			KuunteleLiiketta ();
		}
	}


	// AINEKSIEN LISÄÄMINEN KINKKUUN
	void OnkoKinkunPaalla()
	{
		MessageDisplay.Clear ();													// Tyhjennetään tekstiruutu edellisestä viisastelusta.

		if (Mouse.IsCursorOn (kinkku) && Mouse.IsCursorOn (elamansuola)) 			// Suolan lisäys kinkkuun
		{
		MultiSelectWindow suolaValikko = new MultiSelectWindow ("Kuinka suolaista meinasit?", "Ripaus sinne tänne", "Kourallinen", "Kilpirauhasen räjäytys"); 
		elamansuola.Destroy ();
		kinkkuunLisatyt.Add (elamansuola);
		lisattyKinkkuunString.Add ("suolaa");
		suolaValikko.ItemSelected += KommentitAineksista;
			pisteidenLasku += 3;
		Add (suolaValikko);

		int i = suolaValikko.SelectedIndex;

			Timer aikaLaskuri = new Timer();
			aikaLaskuri.Interval = 30;
			aikaLaskuri.Start(1);


		if (i == 0) 
			{
				pisteidenLasku = pisteidenLasku + 1;
			} 
			else if (i == 1) {
				pisteidenLasku += 3;
			}
		}

		else if (Mouse.IsCursorOn (kinkku) && Mouse.IsCursorOn (jackdaniels)) 		// Jack Danielssin lisäys kinkkuun
		{
			MultiSelectWindow jackdanielsValikko = new MultiSelectWindow ("Kinkku uimaan viskiin?", "No ei, ihan ujosti päälle", "Puolet meni jo kokkiin", "Järvisuomi"); 
			kinkkuunLisatyt.Add (jackdaniels);
			jackdaniels.Destroy ();
			lisattyKinkkuunString.Add ("Jack Daniels viskiä");
			jackdanielsValikko.ItemSelected += KommentitAineksista;
			pisteidenLasku += 2;
			Add (jackdanielsValikko);

			int i = jackdanielsValikko.SelectedIndex;
			if (i == 0) {
				pisteidenLasku = pisteidenLasku + 1;
			} 
			else if (i == 2) {
				pisteidenLasku += 3;
			}
		}

		else if (Mouse.IsCursorOn (kinkku) && Mouse.IsCursorOn (hksininen)) 		// Makkaran lisäys kinkkuun
		{
			MultiSelectWindow hksininenValikko = new MultiSelectWindow ("Ootsää mies vai hanhi?", "Yks kyrsä ny alkuun", "Metri-Heikki", "Norsunsuoli"); 
			kinkkuunLisatyt.Add (hksininen);
			hksininen.Destroy ();
			lisattyKinkkuunString.Add ("makkaraa");
			hksininenValikko.ItemSelected += KommentitAineksista;
			pisteidenLasku += 1;
			Add (hksininenValikko);

			int i = hksininenValikko.SelectedIndex;
			if (i == 0) {
				pisteidenLasku = pisteidenLasku + 1;
			} 
			else if (i == 1) {
				pisteidenLasku += 3;
			}
		}

		else if (Mouse.IsCursorOn (kinkku) && Mouse.IsCursorOn (kebabkastike)) 		// Kebabkastikkeen lisäys kinkkuun
		{
			MultiSelectWindow kebabkastikeValikko = new MultiSelectWindow ("Ottaako kastike?", "Ei kiitos.", "Vähä koristeeks.", "Mina laitta jo!"); 
			kebabkastike.Destroy ();
			lisattyKinkkuunString.Add ("kebab kastiketta");
			kebabkastikeValikko.ItemSelected += KommentitAineksista;
			pisteidenLasku += 5;
			Add (kebabkastikeValikko);

			int i = kebabkastikeValikko.SelectedIndex;
			if (i == 1) {
				pisteidenLasku = pisteidenLasku + 1;
			} 
			else if (i == 2) {
				pisteidenLasku += 3;
			}
		}

		else if (Mouse.IsCursorOn (kinkku) && Mouse.IsCursorOn (lanttu)) 			// Lantun lisäys kinkkuun
		{
			MultiSelectWindow lanttuValikko = new MultiSelectWindow ("Ethän sä tästä voi tykätä?", "Sano HYI!", "Niinku mämmi, tää toimii.", "Lanttu on mun isän nimi."); 
			lanttu.Destroy ();
			lisattyKinkkuunString.Add ("lanttua");
			lanttuValikko.ItemSelected += KommentitAineksista;
			pisteidenLasku += 2;
			Add (lanttuValikko);

			int i = lanttuValikko.SelectedIndex;
			if (i == 2) {
				pisteidenLasku = pisteidenLasku + 3;
			} 
			else if (i == 0) {
				pisteidenLasku += 3;
			}
		}

		else if (Mouse.IsCursorOn (kinkku) && Mouse.IsCursorOn (kossu)) 			// Kossun lisäys kinkkuun
		{
			MultiSelectWindow kossuValikko = new MultiSelectWindow ("Oletkos viinamäen miehiä?", "Juon vain siidereitä.", "Huomenna vapaapäivä...", "Keitän aamupuuron kossuun."); 
			kossu.Destroy ();
			lisattyKinkkuunString.Add ("kosanderia");
			kossuValikko.ItemSelected += KommentitAineksista;
			Add (kossuValikko);

			int i = kossuValikko.SelectedIndex;
			if (i == 2) {
				pisteidenLasku = pisteidenLasku + 1;
			} 
			else if (i == 1) {
				pisteidenLasku += 3;
			}
		}

		else if (Mouse.IsCursorOn (kinkku) && Mouse.IsCursorOn (mandariini)) 		// Mandariinin lisäys kinkkuun
		{
			MultiSelectWindow mandariiniValikko = new MultiSelectWindow ("Taidat tietää mitä teet?", "En syö hedelmiä paitsi kinkun kanssa.", "Haistellaan.", "MANDARIINIGIINALAISTA SIGAA d:-D"); 
			mandariini.Destroy ();
			lisattyKinkkuunString.Add ("mandariini");
			mandariiniValikko.ItemSelected += KommentitAineksista;
			pisteidenLasku -= 2;
			Add (mandariiniValikko);

			int i = mandariiniValikko.SelectedIndex;
			if (i == 1) {
				pisteidenLasku = pisteidenLasku + 1;
			} 
			else if (i == 2) {
				pisteidenLasku += 3;
			}
		}

		else if (Mouse.IsCursorOn (kinkku) && Mouse.IsCursorOn (marsipaani)) 		// Marsipaanin lisäys kinkkuun
		{
			MultiSelectWindow marsipaaniValikko = new MultiSelectWindow ("Haluatko meille töihin?", "En. Ujutetaan vähä tohon päälle.", "Vihdoin sokerihumalaan.", "Marsi-MADAFAKIN-PAANIA!!!1"); 
			marsipaani.Destroy ();
			lisattyKinkkuunString.Add ("marsipaani");
			marsipaaniValikko.ItemSelected += KommentitAineksista;
			pisteidenLasku += 1;
			Add (marsipaaniValikko);

			int i = marsipaaniValikko.SelectedIndex;
			if (i == 1) {
				pisteidenLasku = pisteidenLasku + 1;
			} 
			else if (i == 0) {
				pisteidenLasku += 3;
			}
		}

		else if (Mouse.IsCursorOn (kinkku) && Mouse.IsCursorOn (rakuuna)) 			// Rakuunan lisäys kinkkuun
		{
			MultiSelectWindow rakuunaValikko = new MultiSelectWindow ("Kääri tää sätkään, päriC!", "Voi vilperi nyt poltellaan!", "Todellista mausteiden aatelia.", "Rakuuna-harppuuna-kanuuna"); 
			rakuuna.Destroy ();
			lisattyKinkkuunString.Add ("rakuuna");
			rakuunaValikko.ItemSelected += KommentitAineksista;
			pisteidenLasku += 2;
			Add (rakuunaValikko);

			int i = rakuunaValikko.SelectedIndex;
			if (i == 2) {
				pisteidenLasku = pisteidenLasku + 1;
			} 
			else if (i == 1) {
				pisteidenLasku += 3;
			}
		}

		else if (Mouse.IsCursorOn (kinkku) && Mouse.IsCursorOn (msmjauhe)) 			// Msmjauhen lisäys kinkkuun
		{
			MultiSelectWindow msmjauheValikko = new MultiSelectWindow ("MSM jauhe on hyväksi kynsille.", "Olen puutarhuri.", "Silti sisälläni on aina ollut Megan Fox.", "Tyra Banxx jää mun kynsille toiseks."); 
			msmjauhe.Destroy ();
			lisattyKinkkuunString.Add ("msmjauhe");
			msmjauheValikko.ItemSelected += KommentitAineksista;
			pisteidenLasku += 1;
			Add (msmjauheValikko);

			int i = msmjauheValikko.SelectedIndex;
			if (i == 1) {
				pisteidenLasku = pisteidenLasku + 1;
			} 
			else if (i == 2) {
				pisteidenLasku += 3;
			}
		}

		else if (Mouse.IsCursorOn (kinkku) && Mouse.IsCursorOn (mustaherukka)) 		// Mustaherukkan lisäys kinkkuun
		{
			MultiSelectWindow mustaherukkaValikko = new MultiSelectWindow ("Näistä saisi myös viiniä.", "Jäbä koittaa kusettaa!", "Nyt tehdään nevöföget-kinkku.", "On rasismia aina syödä vaaleaa kinkkua."); 
			mustaherukka.Destroy ();
			lisattyKinkkuunString.Add ("mustaherukka");
			mustaherukkaValikko.ItemSelected += KommentitAineksista;
			pisteidenLasku += 3;
			Add (mustaherukkaValikko);

			int i = mustaherukkaValikko.SelectedIndex;
			if (i == 1) {
				pisteidenLasku = pisteidenLasku + 1;
			} 
			else if (i == 2) {
				pisteidenLasku += 3;
			}
		}

		else if (Mouse.IsCursorOn (kinkku) && Mouse.IsCursorOn (mustakitaturska)) 	// Mustakitaturskan lisäys kinkkuun
		{
			MultiSelectWindow mustakitaturskaValikko = new MultiSelectWindow ("Jos käyt kalassa tiedät mikä on paskahauki.", "Jep, ja tämä on vielä syvemmältä.", "Täähän on tonnikala!", "Tämä on sitä kuuluisaa Jäämeren kaviaaria."); 
			mustakitaturska.Destroy ();
			lisattyKinkkuunString.Add ("mustakitaturska");
			mustakitaturskaValikko.ItemSelected += KommentitAineksista;
			Add (mustakitaturskaValikko);

			int i = mustakitaturskaValikko.SelectedIndex;
			if (i == 1) {
				pisteidenLasku = pisteidenLasku + 1;
			} 
			else if (i == 0) {
				pisteidenLasku += 3;
			}
		}

		else if (Mouse.IsCursorOn (kinkku) && Mouse.IsCursorOn (mustapippuri)) 		// Mustapippurin lisäys kinkkuun
		{
			MultiSelectWindow mustapippuriValikko = new MultiSelectWindow ("Pimeimmän Afrikan mustinta pippuria.", "No offense Afrikka, mutta pidän vain vaahtokarkeista.", "Elämä on yhtä tyhjän kanssa ilman pippuria.", "MUSTABEPEE!!!"); 
			mustapippuri.Destroy ();
			lisattyKinkkuunString.Add ("mustapippuri");
			mustapippuriValikko.ItemSelected += KommentitAineksista;
			pisteidenLasku += 3;
			Add (mustapippuriValikko);

			int i = mustapippuriValikko.SelectedIndex;
			if (i == 1) {
				pisteidenLasku = pisteidenLasku + 1;
			} 
			else if (i == 2) {
				pisteidenLasku += 3;
			}
		}

		else if (Mouse.IsCursorOn (kinkku) && Mouse.IsCursorOn (sukkahousut)) 		// Sukkahousujen lisäys kinkkuun
		{
			MultiSelectWindow sukkahousutValikko = new MultiSelectWindow ("Taitaa olla vaimokkeen sukkahousut jäänyt pöydälle...", "Laitanpa nämä jalkaan.", "Pysyy ainakin kinkku mehevänä!"); 
			sukkahousut.Destroy ();
			lisattyKinkkuunString.Add ("sukkahousut");
			sukkahousutValikko.ItemSelected += KommentitAineksista;
			pisteidenLasku -= 6;

			Add (sukkahousutValikko);

			int i = sukkahousutValikko.SelectedIndex;
			if (i == 1) {
				pisteidenLasku = pisteidenLasku + 1;
			} 
			else if (i == 0) {
				pisteidenLasku += 3;
			}
		}

		else if (Mouse.IsCursorOn (kinkku) && Mouse.IsCursorOn (tilli)) 			// Tillin lisäys kinkkuun
		{
			MultiSelectWindow tilliValikko = new MultiSelectWindow ("Tämä kasvi tuo muistoja mieleen nuoruudesta...", "Kun polttelimme salaa kasvihuoneen takana.", "Voisihan tätä vähän laittaa... Nenään.", "Silloin tehtiin tilliä kinkulla!!!"); 
			tilli.Destroy ();
			lisattyKinkkuunString.Add ("tilli");
			tilliValikko.ItemSelected += KommentitAineksista;
			Add (tilliValikko);

			int i = tilliValikko.SelectedIndex;
			if (i == 1) {
				pisteidenLasku = pisteidenLasku + 1;
			} 
			else if (i == 2) {
				pisteidenLasku += 3;
			}
		}

	}
		

	// KOMMENTTI AINEKSISTA KUN NE LISÄTÄÄN
	void KommentitAineksista(int i)
	{
		
		switch (i)
		{
		case 0 :
			MessageDisplay.Add ("Nössösti lisätty!");
			MessageDisplay.MaxMessageCount = 0;
			break;
		case 1:
			MessageDisplay.Add ("Voi veljet q:-D");
			MessageDisplay.MaxMessageCount = 0;
			break;
		case 2:
			MessageDisplay.Add ("Hautaan asti!");
			MessageDisplay.MaxMessageCount = 0;
			break;
		}

	}


	// HIIREN KUUNTELU ELI MITÄ TAPAHTUU KUN VASEN HIIRI ON PAINETTU POHJAAN
	void KuunteleLiiketta()
	{   
		
		MessageDisplay.Clear();														// Tyhjennetään tekstiruutu edellisestä viisastelusta.

		if (Mouse.IsCursorOn (elamansuola)) 
		{
			elamansuola.Position = Mouse.PositionOnWorld;
			MessageDisplay.Add ("Käytä ensi kerralla Himalajan suolaa");
			MessageDisplay.MaxMessageCount = 0;
		}

		else if (Mouse.IsCursorOn (hksininen)) 
		{
			hksininen.Position = Mouse.PositionOnWorld;
			MessageDisplay.Add ("Vähä kyrsää... Perus suomalaista!");
			MessageDisplay.MaxMessageCount = 0;
		}

		else if (Mouse.IsCursorOn (jackdaniels)) 
		{
			jackdaniels.Position = Mouse.PositionOnWorld;
			MessageDisplay.Add ("Sullahan on ihan kehittynyt maku.");
			MessageDisplay.MaxMessageCount = 0;
		}

		else if (Mouse.IsCursorOn (kebabkastike)) 
		{
			kebabkastike.Position = Mouse.PositionOnWorld;
			MessageDisplay.Add ("Oi että tää ei koskaan jätä kylmäks.");
			MessageDisplay.MaxMessageCount = 0;
		}

		else if (Mouse.IsCursorOn (lanttu)) 
		{
			lanttu.Position = Mouse.PositionOnWorld;
			MessageDisplay.Add ("Meidän kunnioitettu puheenjohtaja.");
			MessageDisplay.MaxMessageCount = 0;
		}

		else if (Mouse.IsCursorOn (kossu)) 
		{
			kossu.Position = Mouse.PositionOnWorld;
			MessageDisplay.Add ("RAI RAI!");
			MessageDisplay.MaxMessageCount = 0;
		}

		else if (Mouse.IsCursorOn (mandariini)) 
		{
			mandariini.Position = Mouse.PositionOnWorld;
			MessageDisplay.Add ("Ootsää mies vai hanhi?");
			MessageDisplay.MaxMessageCount = 0;
		}

		else if (Mouse.IsCursorOn (marsipaani)) 
		{
			marsipaani.Position = Mouse.PositionOnWorld;
			MessageDisplay.Add ("Herkkuperse!");
			MessageDisplay.MaxMessageCount = 0;
		}

		else if (Mouse.IsCursorOn (rakuuna)) 
		{
			rakuuna.Position = Mouse.PositionOnWorld;
			MessageDisplay.Add ("Ei susta kyllä kokkia tule.");
			MessageDisplay.MaxMessageCount = 0;
		}

		else if (Mouse.IsCursorOn (msmjauhe)) 
		{
			msmjauhe.Position = Mouse.PositionOnWorld;
			MessageDisplay.Add ("Et taida tietää mitä tää on...");
			MessageDisplay.MaxMessageCount = 0;
		}

		else if (Mouse.IsCursorOn (mustaherukka)) {
			mustaherukka.Position = Mouse.PositionOnWorld;
			MessageDisplay.Add ("Rohkee veto, sekaan vaan!");
			MessageDisplay.MaxMessageCount = 0;
		}

		else if (Mouse.IsCursorOn (mustakitaturska)) 
		{
			mustakitaturska.Position = Mouse.PositionOnWorld;
			MessageDisplay.Add ("Olen erittäin pahanmakuinen.");
			MessageDisplay.MaxMessageCount = 0;
		}

		else if (Mouse.IsCursorOn (mustapippuri)) 
		{
			mustapippuri.Position = Mouse.PositionOnWorld;
			MessageDisplay.Add ("Turvallisin vaihtoehto... Vässykkä.");
			MessageDisplay.MaxMessageCount = 0;
		}

		else if (Mouse.IsCursorOn (sukkahousut)) 
		{
			sukkahousut.Position = Mouse.PositionOnWorld;
			MessageDisplay.Add ("Kannattaa kääriä kinkku tähän ettei kuivu.");
			MessageDisplay.MaxMessageCount = 0;
		}

		else if (Mouse.IsCursorOn (tilli)) 
		{
			tilli.Position = Mouse.PositionOnWorld;
			MessageDisplay.Add ("Vaimo taas laittanu tillilihaa...");
			MessageDisplay.MaxMessageCount = 0;
		}

		else if (Mouse.IsCursorOn (radio)) 
		{
			MessageDisplay.Add ("JOHN CENAAA");
			MessageDisplay.MaxMessageCount = 0;
		}

	}
		

	// LUODAAN OBJEKTEISTA LISTA & LISÄTÄÄN KAIKKI OBJEKTIT PELIIN
	void Ainekset(List<PhysicsObject> ainekset)
	{



		// THÖ RADIO
		radio = PhysicsObject.CreateStaticObject(Level.Width * 0.3, Level.Height * 0.2);
		radio.Image = LoadImage("radio");											// Lisätään radio taustalle
		radio.Position = new Vector (240, 80);
		Add (radio, 0);

		// THÖ KINKKU
		kinkku = PhysicsObject.CreateStaticObject(Level.Width * 0.25, Level.Height * 0.175);
		kinkku.Image = LoadImage("kinkku");											// 1. Lisätään kinkku
		kinkku.Position = new Vector (-260, -20);
		Add (kinkku, 0);

		elamansuola = new PhysicsObject (Level.Width * 0.05, Level.Height * 0.1);
		elamansuola.Image = LoadImage("elamansuola"); 								// 2. Lisätään suolapurkki
		elamansuola.Position = new Vector (100, -10);
		elamansuola.Tag = "elamansuola";
		ainekset.Add (elamansuola);
		Add (elamansuola, 1);

		hksininen = new PhysicsObject (Level.Width * 0.125, Level.Height * 0.075);
		hksininen.Image = LoadImage("hksininen"); 									// 3. Lisätään HK:n sininen eli makkara
		hksininen.Position = new Vector (380, -60);
		hksininen.Tag = "hksininen";
		ainekset.Add (hksininen);
		Add (hksininen, 1);

		jackdaniels = new PhysicsObject (Level.Width * 0.075, Level.Height * 0.25);
		jackdaniels.Image = LoadImage("jackdaniels"); 								// 4. Lisätään Jack Daniels viskipullo
		jackdaniels.Position = new Vector (-40, 80);
		jackdaniels.Tag = "jackdaniels";
		ainekset.Add (jackdaniels);
		Add (jackdaniels, 1);

		kebabkastike = new PhysicsObject (Level.Width * 0.15, Level.Height * 0.20);
		kebabkastike.Image = LoadImage("kebabkastike"); 							// 5. Lisätään kebabkastikepurkit 
		kebabkastike.Position = new Vector (550, 50);	
		kebabkastike.Tag = "kebabkastike";
		ainekset.Add (kebabkastike);
		Add (kebabkastike, 1);

		lanttu = new PhysicsObject (Level.Width * 0.1, Level.Height * 0.1);
		lanttu.Image = LoadImage("lanttu"); 										// 6. Lisätään kolmen lanttua
		lanttu.Position = new Vector (180, -60);
		lanttu.Tag = "lanttu";
		ainekset.Add (lanttu);
		Add (lanttu, 1);

		kossu = new PhysicsObject (Level.Width * 0.065, Level.Height * 0.225);
		kossu.Image = LoadImage("kossu"); 											// 7. Lisätään Koskenkorva viinapullo
		kossu.Position = new Vector (430, 80);
		kossu.Tag = "kossu";
		ainekset.Add (kossu);
		Add (kossu, 1);

		mandariini = new PhysicsObject (Level.Width * 0.075, Level.Height * 0.075);
		mandariini.Image = LoadImage("mandariini"); 								// 8. Lisätään mandariini
		mandariini.Position = new Vector (540, -70);
		mandariini.Tag = "mandariini";
		ainekset.Add (mandariini);
		Add (mandariini, 1);

		marsipaani = new PhysicsObject (Level.Width * 0.075, Level.Height * 0.1);
		marsipaani.Image = LoadImage("marsipaani"); 								// 9. Lisätään marsipaani
		marsipaani.Position = new Vector (70, -70);
		marsipaani.Tag = "marsipaani";
		ainekset.Add (marsipaani);
		Add (marsipaani, 1);

		rakuuna = new PhysicsObject (Level.Width * 0.03, Level.Height * 0.1);
		rakuuna.Image = LoadImage("rakuuna");										// 10. Lisätään rakuuna maustepurkki
		rakuuna.Position = new Vector (60, 0);
		rakuuna.Tag = "rakuuna";
		ainekset.Add (rakuuna);
		Add (rakuuna, 1);

		msmjauhe = new PhysicsObject (Level.Width * 0.05, Level.Height * 0.05);
		msmjauhe.Image = LoadImage("msmjauhe"); 									// 11. Lisätään MSM -jauhe kippo 
		msmjauhe.Position = new Vector (20, -100);	
		msmjauhe.Tag = "msmjauhe";
		ainekset.Add (msmjauhe);
		Add (msmjauhe, 1);

		mustaherukka = new PhysicsObject (Level.Width * 0.075, Level.Height * 0.075);
		mustaherukka.Image = LoadImage("mustaherukka"); 							// 12. Lisätään mustaherukat 
		mustaherukka.Position = new Vector (270, -20);
		mustaherukka.Tag = "mustaherukka";
		ainekset.Add (mustaherukka);
		Add (mustaherukka, 1);

		mustakitaturska = new PhysicsObject (Level.Width * 0.25, Level.Height * 0.1);
		mustakitaturska.Image = LoadImage("mustakitaturska"); 						// 13. Lisätään mustakitaturskan
		mustakitaturska.Position = new Vector (240, 165);
		mustakitaturska.Tag = "mustakitaturska";
		ainekset.Add (mustakitaturska);
		Add (mustakitaturska, 1);

		mustapippuri = new PhysicsObject (Level.Width * 0.075, Level.Height * 0.075);
		mustapippuri.Image = LoadImage("mustapippuri"); 							// 14. Lisätään mustapippurit
		mustapippuri.Position = new Vector (-600, -70);
		mustapippuri.Tag = "mustapippuri";
		ainekset.Add (mustapippuri);
		Add (mustapippuri, 1);

		sukkahousut = new PhysicsObject (Level.Width * 0.1, Level.Height * 0.15);
		sukkahousut.Image = LoadImage("sukkahousut"); 								// 15. Lisätään sukkahousut
		sukkahousut.Position = new Vector (-510, -65);
		sukkahousut.Tag = "sukkahousut";
		ainekset.Add (sukkahousut);
		Add (sukkahousut, 1);

		tilli = new PhysicsObject (Level.Width * 0.1, Level.Height * 0.075);
		tilli.Image = LoadImage("tilli"); 											// 16. Lisätään tilli
		tilli.Position = new Vector (380, -105);
		tilli.Tag = "tilli";
		ainekset.Add (tilli);
		Add (tilli, 1);
	}
		
}