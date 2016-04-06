using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

public class hefeTestei : Game
{
	public override void Begin ()
	{
		MultiSelectWindow alkuValikko = new MultiSelectWindow("Pelin alkuvalikko", "Aloita peli", "Parhaat pisteet", "Lopeta");
		Add(alkuValikko);


		PhoneBackButton.Listen (ConfirmExit, "Lopeta peli");
		Keyboard.Listen (Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
	}
}

