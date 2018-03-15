Procedural Cable Simple v1.0
DrinkingWindGames



NOTE:  All you need from the downloaded asset package is "Cable_Procedural_Simple"



How To Use:

1.	Attach "Cable_Procedural_Simple" component to an empty, a lineRenderer will be automatically added.

2.	Assign any other transform you desire into the endPointTransform in the inspector.
	(using another empty is recommended)

3.	You need to set up the lineRenderer as you desire, YOU DO NOT NEED TO DO ANYHING WITH THE POISITONS.

4.	Assign the Cable_Procedural's values as desired:
		Points In Line Renderer = how many points will define the cable (odd numbers are encouraged to give the middle of the cable a point and less points means better performance).
		Sag Amplitude = how far the cable will sag in the middle in Unity units (meters).
		Sway Multiplier = how far the cable will sway side to side and up and down in Unity units.
		Sway X Multiplier = this will change how much the cable will sway in the local X direction.
		Sway Y Multiplier = this will change how much the cable will sway in the local Y direction.
		Sway Frequency = how many times the cable will cycle per second (Hertz).


Tips:

1. For Chains, set the lineRenderer's "Texture Mode" to "Repeat Per Segment".  And use the chain material's "tiling" to correct for stretching the chain.