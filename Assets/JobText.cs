using UnityEngine;
using System.Collections;

public class JobText : MonoBehaviour {

	public string jobLabel;
	private static string[] salut=new string[]{"Salut","Hey","Coucou,","Yo",
							 "Salutations","Bonjour","Konnichiwa",
							 "Wesh, wesh"};
    private static string[] demande = new string[]{"Il me faudrait","J'ai un truc pour toi,","Je suis charette sur un projet,",
							   "Il me faut", "J'ai besoin de toi pour", "J'ai une idée de cadeau pour ma mère,",
							   "Tiens, comme tu fais rien, je te propose"};
    private static string[] type = new string[]{"un site", "un advergame", "une application",
							"un jeu facebook", "un jeu-vidéo", "un jeu console", 
							"un e-book", "un CandyCrush-like", "une expérience AR",
							"un serious game", "un jeu fun"};
    private static string[] concernant = new string[] {"sur les"};
    private static string[] sujet = new string[]{"enfants.", "coutumes.", "poneys.", "loutres.","amis.","ornythorinques.","placentas.","oiseaux."};
    
    private static string[] aurevoir = new string[]{"A plus, ma couille!", "A plus!", "Et magne-toi, s'il-te-plait!",
							 	"Ciao!","Salut!","A bientôt!","A demain!","Bye!","Auf wiedersehen!",
								"Bon, bah salut!", "Allez, GO!"};

    public static string GenerateRandomJob()
    {
        return c(salut) + ", "+ c(demande) +" "+ c(type) + c(concernant) + c(sujet)+" " + c(aurevoir);
    }

    public static string c(string[] tab)
    {
        return tab[(int)Random.Range(0, tab.Length)] + " ";
    }



    private static string[] goodVictory = new string[]{"Maître de l'Hatarakade","Dragon Hurlant","Tigre Rugissant","Ours Grondant"};
    private static string[] badVictory = new string[]{"Poulet Asthmatique", "Grillon Atone","Grenouille Souffreteuse","Canard Enroué"};

    public static string GenerateRandomGoodVictory()
    {
        return c(goodVictory);
    }
    public static string GenerateRandomBadVictory()
    {
        return c(badVictory);
    }
}
