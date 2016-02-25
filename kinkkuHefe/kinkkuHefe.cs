using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;



public class kinkkuHefe : PhysicsGame
{
	
	PhysicsObject suolaa;
	PhysicsObject jackD;

	Image taustaKuva = LoadImage ("keittio_v5_piirretty_versio.jpg");
	Image elamanSuola = LoadImage ("elamansuola.png");
	Image hkSininen = LoadImage ("hksininen");
	Image kinkku = LoadImage("kinkku"); 						// Lisätään kinkku
	Image elamansuola = LoadImage("elamansuola"); 				// Lisätään suolapurkki 
	Image hksininen = LoadImage("hksininen"); 					// Lisätään HK:n sininen eli makkara
	Image jackdaniels = LoadImage("jackdaniels"); 				// Lisätään Jack Daniels viskipullo
	Image kebabkastike = LoadImage("kebabkastike"); 			// Lisätään kebabkastikepurkit 
	Image kossu = LoadImage("kossu"); 							// Lisätään Koskenkorva viinapullo
	Image lanttu = LoadImage("lanttu"); 						// Lisätään kolmen lantun
	Image mandariini = LoadImage("mandariini"); 				// Lisätään mandariini 
	Image marsipaani = LoadImage("marsipaani"); 				// Lisätään marsipaani 
	Image rakuuna = LoadImage("rakuuna");						// Lisätään rakuuna maustepurkki
	Image msmjauhe = LoadImage("msmjauhe"); 					// Lisätään MSM -jauhe kippo 
	Image mustaherukka = LoadImage("mustaherukka"); 			// Lisätään mustaherukat 
	Image mustakitaturska = LoadImage("mustakitaturska"); 		// Lisätään mustakitaturskan
	Image mustapippuri = LoadImage("mustapippuri"); 			// Lisätään mustapippureiden
	Image sukkahousut = LoadImage("sukkahousut"); 				// Lisätään sukkahousut
	Image tilli = LoadImage("tilli"); 							// Lisätään tilli


	public void luoObjekti(Image nimi, PhysicsObject objekti){
		
		objekti = new PhysicsObject (Level.Width * 0.09, Level.Height * 0.09);
		objekti.Image = nimi;
		objekti.X =  -70.0;
		objekti.Y =  180;
		Add (objekti);
	}

	public override void Begin ()
	{
		


		PhysicsObject kinkkuK = new PhysicsObject (Level.Width*0.09, Level.Height*0.09, Shape.Rectangle);
		kinkkuK.Image = kinkku;

		kinkkuK.Y =  -70;
		kinkkuK.X =  -180;
		kinkkuK .Tag = "ginkku";
		Add (kinkkuK);


		jackD = new PhysicsObject (Level.Width*0.04, Level.Height*0.15 , Shape.Rectangle);
		jackD.Image = jackdaniels;
		jackD.Y =  -70;
		jackD.X =  180;
		Add (jackD);


		luoObjekti (elamanSuola, suolaa);
		//luoObjekti (jackdaniels,jackD);
		
		IsMouseVisible = true;
		IsFullScreen = true;
		Level.Background.Image = taustaKuva;


		Mouse.IsCursorVisible = true;

		Mouse.Listen (MouseButton.Left, ButtonState.Down, KuunteleLiiketta, "liikkuukos?");

		PhoneBackButton.Listen (ConfirmExit, "Lopeta peli");
		Keyboard.Listen (Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
	}


	void KuunteleLiiketta(AnalogState hiirenTila)
	{     
		if(Mouse.IsCursorOn(jackD))
		{
			jackD.X = Mouse.PositionOnWorld.X;
		jackD.Y = Mouse.PositionOnWorld.Y;
		}

	}






		
		

}

