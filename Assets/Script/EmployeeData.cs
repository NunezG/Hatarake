using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EmployeeData {

	public float vitesseDemotivation;
	public float maxVitesseDemotivation = 1f;
	public float minVitesseDemotivation = 20.0f;

	public float effetRepos;
    public float maxEffetRepos = 20.0f;
    public float minEffetRepos = 1f;

	public string firstName;
	public string surname;
	public bool isMale;

	public int[] physicalCaracteristics; // 0:figure, 1:hair, 2:eyes, 3:nose, 4:mouth, 5:top, 6:background
	
	private Color[] skinColors;
	private Color[] hairColors;
	
	public Color skinColor;
	public Color hairColor;
	public Color topColor;
	public Color backColor;

	public List<string> hobbies = new List<string>();

	private Data data;

    public float fatigue = 0;// variable similaire à la vie, augmente qd il se fait engueuler, diminue lors de ses pauses. si == fatigueMAX -> suicidaire = true;
    public float motivation;// variable conditionnant le départ en pause. motivation = 0 -> go to Pause;
    public float fatigueMAX = 100;
    public float motivationMax = 500;// variable conditionnant le départ en pause. motivation = 0 -> go to Pause;
    public float effetEngueulement = 40;
    public float vitesseTravail = 0.5f;

	public void InitializeEmployee (){
		data = new Data();

		//Choix du sexe
		int cho = (int)Random.Range(0, 2);
		isMale = (cho == 1) ? true : false;

		//Choix du nom selon le sexe
		if(isMale){
			cho = (int)Random.Range(0, data.mNames.Length);

			firstName = data.mNames[cho];

			cho = (int)Random.Range(0, data.Names.Length);
			surname = data.Names[cho];
		}
		else{
			cho = (int)Random.Range(0, data.fNames.Length);

			firstName = data.fNames[cho];
			cho = (int)Random.Range(0, data.Names.Length);
			surname = data.Names[cho];
		}
		
		//Choix des occupations
		int glandouille = 0;
		for (int i = 0 ; i < 5 ; i++){
			cho = (int)Random.Range(0, 3);

			switch(cho){
				case 0:
					cho = (int)Random.Range(0, data.lowClassHobbies.Count);
					glandouille += 2;
					hobbies.Add(data.lowClassHobbies[cho]);
					data.lowClassHobbies.RemoveAt(cho);
					break;
			
				case 1:
					cho = (int)Random.Range(0, data.mediumClassHobbies.Count);
					hobbies.Add(data.mediumClassHobbies[cho]);
					data.mediumClassHobbies.RemoveAt(cho);
					glandouille ++;
					break;

				case 2:
					cho = (int)Random.Range(0, data.highClassHobbies.Count);
					hobbies.Add(data.highClassHobbies[cho]);
					data.highClassHobbies.RemoveAt(cho);
					break;
			}
		
		//Determination de l'effet des hobbies sur la rapidité de taff et la rapidité de glande
		vitesseDemotivation = Mathf.Lerp(minVitesseDemotivation, maxVitesseDemotivation, glandouille / 10.0f);
		effetRepos = Mathf.Lerp(minEffetRepos, maxEffetRepos, 1.0f - (glandouille / 10.0f));


		}

		//Randomizing caracteristics
		physicalCaracteristics = new int[7];
		for (int i = 0; i < physicalCaracteristics.Length; i++) {
			
			physicalCaracteristics[i] = (int) Random.Range(0, 8);

			// If the employee is a woman, shift right index by 8
			if(!isMale) {
					physicalCaracteristics[i] += 8;
			}
		}
		 

		//Assigning colors
		skinColor = HSVToRGB(Random.Range (0.1f, 0.15f), Random.value, Random.Range (0.3f, 1.0f));

		hairColor = HSVToRGB(Random.Range (0.1f, 0.15f), Random.value, Random.value);

		topColor = HSVToRGB(Random.value, 1.0f, 1.0f);

		backColor = HSVToRGB(Random.value, 0.4f, 1.0f);

		//On met la motiv' à une valeur aléatoire en début de jeu !
        motivation = Random.Range(0, motivationMax/2);

	}


     public Color HSVToRGB(float H, float S, float V)
 {
     if (S == 0f)
         return new Color(V,V,V);
     else if (V == 0f)
         return new Color(0,0,0);
     else
     {
         Color col = Color.black;
         float Hval = H * 6f;
         int sel = Mathf.FloorToInt(Hval);
         float mod = Hval - sel;
         float v1 = V * (1f - S);
         float v2 = V * (1f - S * mod);
         float v3 = V * (1f - S * (1f - mod));
         switch (sel + 1)
         {
         case 0:
             col.r = V;
             col.g = v1;
             col.b = v2;
             break;
         case 1:
             col.r = V;
             col.g = v3;
             col.b = v1;
             break;
         case 2:
             col.r = v2;
             col.g = V;
             col.b = v1;
             break;
         case 3:
             col.r = v1;
             col.g = V;
             col.b = v3;
             break;
         case 4:
             col.r = v1;
             col.g = v2;
             col.b = V;
             break;
         case 5:
             col.r = v3;
             col.g = v1;
             col.b = V;
             break;
         case 6:
             col.r = V;
             col.g = v1;
             col.b = v2;
             break;
         case 7:
             col.r = V;
             col.g = v3;
             col.b = v1;
             break;
         }
         col.r = Mathf.Clamp(col.r, 0f, 1f);
         col.g = Mathf.Clamp(col.g, 0f, 1f);
         col.b = Mathf.Clamp(col.b, 0f, 1f);
         return col;
     }
 }

}	

public class Data{

		public string[] fNames = {
		"Adriana",
		"Alexandra",
		"Alexandria",
		"Alexis",
		"Alicia",
		"Allison",
		"Alyssa",
		"Amanda",
		"Amber",
		"Amy",
		"Ana",
		"Andrea",
		"Angela",
		"Angelica",
		"Anna",
		"April",
		"Ashley",
		"Bianca",
		"Brenda",
		"Brianna",
		"Brittany",
		"Brittney",
		"Brooke",
		"Caitlin",
		"Cassandra",
		"Catherine",
		"Chelsea",
		"Christina",
		"Christine",
		"Courtney",
		"Crystal",
		"Cynthia",
		"Danielle",
		"Diana",
		"Elizabeth",
		"Erica",
		"Erika",
		"Erin",
		"Hanna",
		"Heather",
		"Holly",
		"Jacqueline",
		"Jamie",
		"Jasmine",
		"Jenna",
		"Jennifer",
		"Jessica",
		"Jordan",
		"Julie",
		"Karen",
		"Katelyn",
		"Katherine",
		"Kathryn",
		"Katie",
		"Kayla",
		"Kelly",
		"Kelsey",
		"Kimberly",
		"Kristen",
		"Kristin",
		"Kristina",
		"Krystal",
		"Laura",
		"Lauren",
		"Leslie",
		"Lindsay",
		"Lindsey",
		"Lisa",
		"Maria",
		"Marissa",
		"Mary",
		"Mayra",
		"Megan",
		"Melanie",
		"Melissa",
		"Michelle",
		"Monica",
		"Monique",
		"Morgan",
		"Nancy",
		"Natalie",
		"Natasha",
		"Nicole",
		"Patricia",
		"Rachel",
		"Rebecca",
		"Samantha",
		"Sandra",
		"Sara",
		"Sarrah",
		"Shannon",
		"Stephanie",
		"Tara",
		"Taylor",
		"Tiffany",
		"Vanessa",
		"Veronica",
		"Victoria",
		"Whitney"
	};

		public string[] mNames = {
		"Aaron",
		"Adam",
		"Adrian",
		"Alejandro",
		"Alex",
		"Alexander",
		"Anakin",
		"Andrew",
		"Antonio",
		"Austin",
		"Benjamin",
		"Bradley",
		"Brandon",
		"Brett",
		"Brian",
		"Bryan",
		"Cameron",
		"Carlos",
		"Casey",
		"Chad",
		"Charles",
		"Christian",
		"Christopher",
		"Cody",
		"Corey",
		"Cory",
		"Daniel",
		"David",
		"Derek",
		"Dustin",
		"Edgar",
		"Eduardo",
		"Edward",
		"Eric",
		"Erik",
		"Evan",
		"Francisco",
		"Gabriel",
		"Garrett",
		"George",
		"Gregory",
		"Ian",
		"Jacob",
		"James",
		"Jared",
		"Jason",
		"Jeffrey",
		"Jeremy",
		"Jesse",
		"John",
		"Jonathan",
		"Jordan",
		"Jorge",
		"Jose",
		"Joseph",
		"Joshua",
		"Juan",
		"Justin",
		"Kenneth",
		"Kevin",
		"Kyle",
		"Luis",
		"Manuel",
		"Marcus",
		"Mario",
		"Mark",
		"Martin",
		"Matthew",
		"Michael",
		"Miguel",
		"Nathan",
		"Nicholas",
		"Patrick",
		"Paul",
		"Peter",
		"Phillip",
		"Raymond",
		"Ricardo",
		"Richard",
		"Robert",
		"Ryan",
		"Samuel",
		"Scott",
		"Sean",
		"Shane",
		"Shawn",
		"Spencer",
		"Stephen",
		"Steven",
		"Thomas",
		"Timothy",
		"Travis",
		"Trevor",
		"Tyler",
		"Tylor",
		"Victor",
		"Vincent",
		"William",
		"Zachary"
	};

		public string[] Names = {
		"Adams",
		"Allen",
		"Anderson",
		"Andrews",
		"Baker",
		"Bings",
		"Brown",
		"Buendia",
		"Campbell",
		"Carter",
		"Clark",
		"Collins",
		"Davis",
		"Drake",
		"Edwards",
		"Evans",
		"Flikeblük",
		"Garcia",
		"Gonzalez",
		"Green",
		"Hall",
		"Harris",
		"Hernandez",
		"Hill",
		"Holmes",
		"Jackson",
		"Johnson",
		"Jones",
		"King",
		"Lee",
		"Lewis",
		"Lopez",
		"Martin",
		"Martinez",
		"Molyneux",
		"McFly",
		"McGuffin",
		"Miller",
		"Mitchell",
		"Moore",
		"Nelson",
		"O'theworst",
		"Parker",
		"Perez",
		"Peterson",
		"Phillips",
		"Roberts",
		"Robinson",
		"Rodriguez",
		"Scott",
		"Smith",
		"Solo",
		"Skywalker",
		"Taylor",
		"Thomas",
		"Thompson",
		"Turner",
		"Vador",
		"Walker",
		"Watson",
		"White",
		"Williams",
		"Williamson",
		"Wilson",
		"Wright",
		"Young"
	};

		public List<string> highClassHobbies = new List<string>{

		"lire des encyclopédies",
		"lire des notices Ikea",
		"lire des essais scientifiques",
		"lire des rubriques politiques",
		"lire des thèses",
		"lire des tragédies grecques",
		"lire des partitions de Bach",
		"perfectionner son swing",
		"perfectionner son karaoké",
		"perfectionner son japonais",
		"perfectionner son latin",
		"perfectionner son hébreu",
		"perfectionner son ninjutsu",
		"perfectionner son archerie",
		"perfectionner son judo",
		"aider les vieux à traverser",
		"Organiser des kermesses",
		"Organiser des bals",
		"Organiser des dons de sang",
        "Boire du thé",
        "faire des virées en Porsche",
        "coder"
	};
	
		public List<string> mediumClassHobbies = new List<string>{
		"la menuiserie",
		"le bricolage",
		"la mécanique",
		"la pêche",
		"le macramé",
		"la couture",
		"la dentelle",
		"cuisiner indien",
		"cuisiner chinois",
		"cuisiner des choses",
		"cuisiner des légumes",
		"cuisiner exotique",
		"cuisiner péruvien",
		"cuisiner guatémaltèque",
		"cuisiner français",
		"le foot",
		"le basket",
		"le volley",
		"le handball",
		"la danse",
		"la varappe",
		"le bowling",
		"jouer aux jeux de société",
		"jouer aux cartes",
		"jouer de la guitare",
		"jouer du saxophone",
		"jouer du piano",
		"jouer du triangle"
	};

		public List<string> lowClassHobbies = new List<string>{
		"sortir en boîte",
		"sortir au bar",
		"les parties fines entre amis",
		"les parties fines entre collègues",
		"s'admirer durant des heures",
		"écarter ses doigts de pieds",
		"jouer aux jeux-vidéo",
		"jouer au flipper",
		"se masturber pendant des heures",
		"surfer sur Facebook",
		"mater des vidéos sur Youtube",
		"courir dans les champs au ralenti",
		"ne rien faire",
		"passer sa journée devant la télé",
		"manger des choses grasses",
		"boire de façon massive",
		"frapper des gens",
		"se droguer",
		"fumer des joints",
		"faire des jeux-vidéos",
		"se lever à 8:46",
        "regarder les murs"
	};

}