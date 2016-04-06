using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;


	
public class Kuvat : kinkkuHefe
	{



	public string Nimi { get; set;}
	public Image Kuva { get; set;}
	public PhysicsObject Tuote {get; set;}


	public Kuvat(string nimi, double kokoX, double kokoY, int paikkaX, int paikkaY){// : base (nimi, kokoX, kokoY, paikkaX, paikkaY){
		Nimi = nimi;
		//Kuva = LoadImage ("elamansuola");
		Tuote = new PhysicsObject (kokoX, kokoY, Shape.Rectangle);


		Tuote.Image = Kuva;

		Tuote.Y = paikkaY;
		Tuote.X = paikkaX;
		//LuoObjekti (paikkaX, paikkaY);
		Tuote.Image = LoadImage(Nimi);
		//Tuote.Image = Kuva;
		Add(Tuote);

		 



	}

	//Kuvat suola = new Kuvat ("elamanSuola", elamanSuola);
	//suola.LuoObjekti();


	public void AsetaKoordinaatti(){



		//Tuote.Image = Kuva;

		//Add (Tuote);

	
	}

	public void AsetaMaailmaan(){
		
		Add (Tuote);
	}
}
/*
public class Koira : Elain
{

	public double Paino { get; set;}
	public string Rotu { get;}

	public Koira (string nimi, double paino, string rotu)
	{
		Nimi = nimi;
		Paino = paino;
		Rotu = rotu;

	}

	public override void Aantele(){
		Console.WriteLine ("Vuh vuh: sanoo" + Nimi);
	}
*/
/*
mandariini = new PhysicsObject (Level.Width * 0.2, Level.Height * 0.2);
mandariini.Image = LoadImage("mandariini"); 				// Lisätään mandariini
mandariini.X = 600;
mandariini.Y = 100;
mandariini.Tag = "aines";
Add (mandariini);

marsipaani = new PhysicsObject (Level.Width * 0.4, Level.Height * 0.2);
marsipaani.Image = LoadImage("marsipaani"); 				// Lisätään marsipaani
marsipaani.X = 700;
marsipaani.Y = 100;
marsipaani.Tag = "aines";
Add (marsipaani);

rakuuna = new PhysicsObject (Level.Width * 0.2, Level.Height * 0.2);
rakuuna.Image = LoadImage("rakuuna");						// Lisätään rakuuna maustepurkki
rakuuna.X = -100;
rakuuna.Y = 100;
rakuuna.Tag = "aines";
Add (rakuuna);

msmjauhe = new PhysicsObject (Level.Width * 0.2, Level.Height * 0.2);
msmjauhe.Image = LoadImage("msmjauhe"); 					// Lisätään MSM -jauhe kippo 
msmjauhe.X = -200;
msmjauhe.Y = 100;
msmjauhe.Tag = "aines";
Add (msmjauhe);

mustaherukka = new PhysicsObject (Level.Width * 0.2, Level.Height * 0.2);
mustaherukka.Image = LoadImage("mustaherukka"); 			// Lisätään mustaherukat 
mustaherukka.X = -300;
mustaherukka.Y = 100;
mustaherukka.Tag = "aines";
Add (mustaherukka);

mustakitaturska = new PhysicsObject (Level.Width * 0.2, Level.Height * 0.2);
mustakitaturska.Image = LoadImage("mustakitaturska"); 		// Lisätään mustakitaturskan
mustakitaturska.X = -400;
mustakitaturska.Y = 100;
mustakitaturska.Tag = "aines";
Add (mustakitaturska);

mustapippuri = new PhysicsObject (Level.Width * 0.2, Level.Height * 0.2);
mustapippuri.Image = LoadImage("mustapippuri"); 			// Lisätään mustapippurit
mustapippuri.X = -500;
mustapippuri.Y = 100;
mustapippuri.Tag = "aines";
Add (mustapippuri);

sukkahousut = new PhysicsObject (Level.Width * 0.2, Level.Height * 0.2);
sukkahousut.Image = LoadImage("sukkahousut"); 				// Lisätään sukkahousut
sukkahousut.X = -600;
sukkahousut.Y = 100;
sukkahousut.Tag = "aines";
Add (sukkahousut);

tilli = new PhysicsObject (Level.Width * 0.2, Level.Height * 0.2);
tilli.Image = LoadImage("tilli"); 							// Lisätään tilli
tilli.X = -700;
tilli.Y = 100;
tilli.Tag = "aines";
Add (tilli);



// OHJEKENTTÄ PELAAJALLE
Label tekstikentta = new Label(300, 50, "ALAHAN KOKATA POIKA!");
tekstikentta.X = Screen.Right - 250;
tekstikentta.Y = Screen.Top - 50;
tekstikentta.TextColor = Color.DarkGray;
tekstikentta.BorderColor = Color.Black;
Add(tekstikentta);
*/


/*
// MITAT AINEKSIEN LAITTOON
Image ruiskumitta = LoadImage("ruiskumitta"); 				// Lisätään ruiskumitta
Image loylykauha = LoadImage("loylykauha"); 				// Lisätään löylykauha
Image mitta1dl = LoadImage("mitta1dl"); 					// Lisätään desimitan
Image mittaruokalusikka = LoadImage("mittaruokalusikka");	// Lisätään ruokalusikka
Image mittateelusikka = LoadImage("mittateelusikka"); 		// Lisätään teelusikka 
Image soppakauha = LoadImage("soppakauha"); 				// Lisätään soppakauha
*/

// LISÄÄ SOKERISIA JUTKUTUSHÄRPÄKKEITÄ
/*
	Image niittivyo = LoadImage("niittivyo"); 					// Lisätään niittivyö objekti 
	Image pelargonia = LoadImage("pelargonia"); 				// Lisätään pelargonian objekti 
	Image radio = LoadImage("radio"); 							// Lisätään radion objekti 
	Image xboxohjain = LoadImage("xboxohjain"); 				// Lisätään xboxohjain
	Image saunavihta = LoadImage("saunavihta"); 				// Lisätään saunavihta
	*/
/*
foreach (PhysicsObject tuote in tuoteLista) {

	string nimitys = tuote.ToString ();
	LuoTuote (elamansuola, 0.1, 0.2, nimitys);
}



public void LuoTuote(PhysicsObject tuote, double lkerroin, double kkerroin, string kuva){
	tuote = new PhysicsObject (Level.Width * lkerroin, Level.Height * kkerroin, Shape.Circle);

	tuote.Image = LoadImage (kuva);
	tuote.X = 100;
	tuote.Y = 100;
	Add (tuote);
}
*/