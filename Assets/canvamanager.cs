using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


using System.Diagnostics;
using TMPro;

public class canvamanager : MonoBehaviour
{

    public Image diap1;
    public TextMeshProUGUI texte;
    public Sprite[] diapo;
    public int nbdiap = 0;
    public static readonly List<String> Dico = new List<String>() { "Le banana virus se diffuse de plus en plus parmi la population !", "Les symptomes sont effroyables : apres deux semaines d’incubation..", "Une banane pousse sur le front ou sur le dos du contamine !",
        "Pour guerir les infectes nos chercheurs ont elabore deux medicaments : le premier s’appelle Dolifront..", "il enleve la banane du front, le deuxieme Aspidos enleve la banane du dos.", "Malheureusement ces medicaments necessitent une production alambiquees et les gestionnaires de la chaine logistique sont morts de la maladie.","Nous avons besoin de toi pour surmonter cette crise sanitaire et logistique ! Pour produire les medicaments, nous avons besoin de deux principes actifs : le Dolinium",
        "et l’Aspinium.","Pour t’en procurer, tu dois passer commande aux fournisseurs","et les envoyer dans tes usines pharmaceutiques pour pouvoir lancer la production !","Le Dolifront demande 3 fois plus de Dolinium que d’Aspinium","et c’est l’inverse pour l’Aspidos (il lui faut 3 fois plus d’Aspidos que de Dolinium).","Tu dois essayer de repondre a la demande en medicament du mieux que tu peux en depensant le moins d’argent inutilement !","Maintenant vas-y, on a besoin de toi !"
    };
    // Start is called before the first frame update
    void Start()
    {
        diap1.sprite = diapo[nbdiap];
        texte.text = Dico[nbdiap];
    }


  public void Pressbutton()
    {
        
        nbdiap = nbdiap + 1;
        if (nbdiap < 19)
        {
            diap1.sprite = diapo[nbdiap];
        }
        if (nbdiap < 14)
        {
            texte.text = Dico[nbdiap];
        }
        if (nbdiap == 19)
        {
            SceneManager.LoadScene("Level1");
        }
    }

   
}
