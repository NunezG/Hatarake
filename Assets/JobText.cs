using UnityEngine;
using System.Collections;

public class JobText : MonoBehaviour {

	public string jobLabel;
	private static string[] salut=new string[]{"Salut,","Hey,","Coucou,","Yo,",
							 "Salutations,","Bonjour,","Bonsoir,","Konichiwa,",
							 "Wesh, wesh,", "Konbanwa,"};

    private static string[] intro = new string[]{"c'est le fils de", "c'est le beau-frère de", 
							 "je suis le fantôme de","c'est","c'est moi,","moi être", "je suis"};
    private static string[] nom = new string[]{"Taro,","Yamada,","Iroshi,","Kazuke,","Naruto,","Sasuke,",
						   "Yotto,","le Président du Japon,"};
    private static string[] qualite = new string[]{"ton oncle.","tu te souviens de moi ?","ton frère de sang.",
							   "ton frère.","ton pote de toujours.","un ami de ton père.",
							   "un ami de ton père.","un ami de ton père.","ça va?",
							   "ça faisait longtemps, vieille branche.", "il parait que t'as gagné en grade."};
    private static string[] demande = new string[]{"Il me faudrait","J'ai un truc pour toi,","Je suis charette sur un projet,",
							   "Il me faut","Toi me fournir :", "J'ai besoin de toi pour", "J'ai une idée de cadeau pour ma mère,",
							   "Tiens, comme tu fais rien, je te propose"};
    private static string[] type = new string[]{"un site", "un advergame", "une application",
							"un jeu facebook", "un jeu-vidéo", "un jeu console", 
							"un e-book", "un CandyCrush-like", "une expérience AR",
							"un serious game", "un jeu fun"};
    private static string[] concernant = new string[] { "à propos des", "concernant les", "sur les", "parlant des", "décrivant les" };
    private static string[] sujet = new string[]{"moeurs", "coutumes", "poneys", "phases existentielles de la vie des loutres",
							 "amis"};
    private static string[] adjectif = new string[]{"d'outre-tombe", "en Occident.", "en Orient.","en Ouganda.","aléatoires.",
								"en entreprise.", "en papier maché.", "imaginaires."};
    private static string[] merci = new string[]{"Je t'aime, toi, tu sais,", "T'es un chef,","Voilà,",
							 "T'es le meilleur,","C'est cool de ta part,","Je te revaudrai ça,",
							 "C'est pour offrir,","Ce sera à emporter, s'il-te-plait,"};
    private static string[] aurevoir = new string[]{"à plus, ma couille!", "à plus!", "et magne-toi, s'il-te-plait!",
							 	"ciao!","salut!","à bientôt!","à demain!","bye!","auf wiedersehen!",
								"bon, bah salut!", "... allez, GO!"};

	void Start () {

		salut = new string[]{"Salut,","Hey,","Coucou,","Yo,",
							 "Salutations,","Bonjour,","Bonsoir,","Konnichiwa,",
							 "Wesh, wesh,", "Konbanwa,"};

		intro = new string[]{"c'est le fils de", "c'est le beau-frère de", 
							 "je suis le fantome de","c'est","c'est moi,","moi etre", "je suis"};

		nom = new string[]{"Taro,","Yamada,","Iroshi,","Kazuke,","Naruto,","Sasuke,",
						   "Yotto,","le Président du Japon,"};

		qualite = new string[]{"ton oncle.","tu te souviens de moi ?","ton frère de sang.",
							   "ton frère.","ton pote de toujours.","un ami de ton père.",
							   "un ami de ton père.","un ami de ton père.","ça va?",
							   "ça faisait longtemps, vieille branche.", "il parait que t'as gagné en grade."};

		demande = new string[]{"Il me faudrait","J'ai un truc pour toi,","Je suis charette sur un projet,",
							   "Il me faut","Toi me fournir :", "J'ai besoin de toi pour", "J'ai une idée cadeau pour ma mère,",
							   "Tiens, comme tu fais rien, je te propose"};

		type = new string[]{"un site", "un advergame", "une application",
							"un jeu facebook", "un jeu-vidéo", "un jeu console", 
							"un e-book", "un CandyCrush-like", "une expérience AR",
							"un serious game", "un jeu fun"};

		concernant = new string[]{"à propos des", "concernant les", "sur les", "parlant des", "décrivant les"};

		sujet = new string[]{"moeurs", "coutumes", "poneys", "phases existentielles de la vie des loutres",
							 "amis"};

		adjectif = new string[]{"d'outre-tombe", "en Occident.", "en Orient.","en Ouganda.","aléatoires.",
								"en entreprise.", "en papier maché.", "imaginaires"};

		merci = new string[]{"Je t'aime, toi, tu sais,", "T'es un chef,","Voilà,",
							 "T'es le meilleur,","C'est cool de ta part,","Je te revaudrai ça,",
							 "C'est pour offrir,","Ce sera à emporter, s'il-te-plait,"};

		aurevoir = new string[]{"à plus, ma couille!", "à plus!", "et magne-toi, s'il-te-plait!",
							 	"ciao!","salut!","à bientot!","à demain!","bye!","auf wiedersehen!",
								"bon, bah salut!", "... allez, GO!"};

	}

	void Update(){

		if((int)Time.time % 5 == 0) 
			GenerateJob ();

	}

	void GenerateJob(){

		jobLabel = c(salut) + c(intro) + c(nom) + c(qualite) + c(demande) + c(type) + c(concernant) + c(sujet) + c(adjectif) + c(merci) + c(aurevoir);

	}

    public static string GenerateRandomJob()
    {
        return c(salut) + c(intro) + c(nom) + c(qualite) + c(demande) + c(type) + c(concernant) + c(sujet) + c(adjectif) + c(merci) + c(aurevoir);
    }

	/*string c(string[] tab){

		return tab[(int)Random.Range (0, tab.Length)]+" ";
	}*/

    public static string c(string[] tab)
    {

        return tab[(int)Random.Range(0, tab.Length)] + " ";
    }
}
