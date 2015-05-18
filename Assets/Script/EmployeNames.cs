using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EmployeNames {

	public float vitesseDemotivation;
	public float maxVitesseDemotivation = 100;
	public float minVitesseDemotivation = 1;
	public float effetRepos;
	public float maxEffetRepos = 100 ;
	public float minEffetRepos = 1;
	public string nom;
	public bool isMale;
	public List<string> hobbies = new List<string>();
	public Data data;

    public float fatigue = 0;// variable similaire à la vie, diminue qd il se fait engueuler, augmente lors de ses pauses. si == fatigueMAX -> suicidaire = true;
    public float motivation;// variable conditionnant le départ en pause. motivation = 0 -> go to Pause;
    public float fatigueMAX = 100;
    public float motivationMax = 500;// variable conditionnant le départ en pause. motivation = 0 -> go to Pause;
    public float effetEngueulement = 200;

	public void InitializeEmployee (){
		data = new Data();
		//Choix du sexe
		int cho = (int)Random.Range(0, 2);
		isMale = (cho == 1) ? true : false;

		//Choix du nom selon le sexe
		if(isMale){
			cho = (int)Random.Range(0, data.mNames.Length);

			nom = data.mNames[cho];

			cho = (int)Random.Range(0, data.Names.Length);
			nom = nom + " " + data.Names[cho];
		}
		else{
			cho = (int)Random.Range(0, data.fNames.Length);

			nom = data.fNames[cho];
			cho = (int)Random.Range(0, data.Names.Length);
			nom = nom + " " + data.Names[cho];
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
		
		//Determination de l'effet sur la rapidité de taff et la rapidité de glande
		vitesseDemotivation = Mathf.Lerp(minVitesseDemotivation, maxVitesseDemotivation, glandouille / 10.0f);
		effetRepos = Mathf.Lerp(minEffetRepos, maxEffetRepos, 1.0f - (glandouille / 10.0f));


		}


        motivation = motivationMax;

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
		"Organiser des dons de sang"
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
		"les partouzes entre amis",
		"les partouzes entre collègues",
		"se regarder langoureusement dans un miroir",
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
		"se lever à 8:46"
	};

}