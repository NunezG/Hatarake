using UnityEngine;
using System.Collections;

public class ArithmeticShit : MonoBehaviour {
 
	float SpreadOutRandom(Vector2[] ranges){

		//Initialize Spread for Random max value
		float spread = 0.0f;

		//Add up all the ranges
		for (int i = 0; i < ranges.Length; i++) {
			spread += (ranges [i].y - ranges [i].x);
		}

		//Initialize the randomly chosen value
		float value = Random.Range(0, spread);

		//Go through all the ranges
		for(int i = 0; i < ranges.Length ; i++){ 

			//If you can subtract the current range from the value, do it!
			if((value - (ranges[i].y - ranges[i].x)) > 0.0f ){
				value -= (ranges[i].y - ranges[i].x);
			}

			//If the value is inferior to the current range, add it to the left limit!
			else{ 
				value += ranges[i].x;
				break;
			}
		}

		return value;

	}
}
