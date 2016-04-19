using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

public class Tuote : PhysicsObject
{
	public string Nimi { get; set;}


	public Tuote (string nimi, double leveys, double korkeus) : base (leveys, korkeus)
	{
		Nimi = nimi;

	}
}

