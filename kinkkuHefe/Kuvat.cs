using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;


	
	public class Kuvat : PhysicsGame
	{



	public string Nimi { get; set;}
	public Image Kuva { get; set;}


	public Kuvat(String nimi, Image kuva){
		Nimi = nimi;
		Kuva = kuva;


	}

	//Kuvat suola = new Kuvat ("elamanSuola", elamanSuola);
	//suola.LuoObjekti();


	public void LuoObjekti(){

		PhysicsObject ob = new PhysicsObject (50, 90, Shape.Rectangle);

		ob.Image = Kuva;
		ob.Y = -80;
		Add (ob);

	
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

