using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EsilvGui;

namespace Console_Application_jeu_de_la_vie_C.A
{
    class Program
    {

        static void AffichageGrille(int[,] Matrice)
        {// Cette fonction permet d'afficher une matrice dans la console.
            for (int indexLigne = 0; indexLigne < Matrice.GetLength(0); indexLigne++)
            {
                for (int indexColonne = 0; indexColonne < Matrice.GetLength(1); indexColonne++)
                {
                    Console.Write(Matrice[indexLigne, indexColonne]);
                }
                Console.WriteLine();
            }
        }

        static int[,] Grille(int ligne, int colonne)
        {// Cette fonction permet la création d'une "Grille de jeu" en fonction du nombre de ligne et de colonne entrer par l'utilisateur et la remplie de 0 soit de cellule morte.
            int[,] Grilledejeu = new int[ligne, colonne];
            for (int Ligne = 0; Ligne < Grilledejeu.GetLength(0); Ligne++)
            {
                for (int Colonne = 0; Colonne < Grilledejeu.GetLength(1); Colonne++)
                {
                    Grilledejeu[Ligne, Colonne] = 0; // Remplissage de la matrice par des cellules mortes pour initialiser
                }
            }
            return Grilledejeu;
        }

        static void Grainedegénération(int[,] Matrice, double TauxDeRemplissage)
        {// Cette fonction permet de générer une population de facon aléatoire dans une matrice entrée en paramètre. La proportion de la population est également définie en paramètre.
            Random generateur = new Random();
            double nombredecelluleinitialle = Matrice.Length * TauxDeRemplissage;
            for (int i = 0; i < nombredecelluleinitialle; i++)
            {
                int CoordonneeLigne = generateur.Next(0, Matrice.GetLength(0));
                int CoordonneeColonne = generateur.Next(0, Matrice.GetLength(1));
                if (Matrice[CoordonneeLigne, CoordonneeColonne] == 1)
                    i--;
                else
                    Matrice[CoordonneeLigne, CoordonneeColonne] = 1;
            }
        }

        static void GraineDeGenerationDe2Populations(int[,] Matrice, double TauxDeRemplissagePop1, double TauxDeRemplissagePop2)
        {// Cette fonction permet de générer 2 populations distinctes de facon aléatoire dans une matrice entrée en paramètre. La proportion de chaque population est également définie en paramètre.
            Random generateur = new Random();
            double nombredecelluleinitialle1 = Matrice.Length * TauxDeRemplissagePop1;
            double nombredecelluleinitialle2 = Matrice.Length * TauxDeRemplissagePop2;
            for (int i = 0; i < nombredecelluleinitialle1; i++)
            {
                int CoordonneeLigne = generateur.Next(0, Matrice.GetLength(0));
                int CoordonneeColonne = generateur.Next(0, Matrice.GetLength(1));
                if (Matrice[CoordonneeLigne, CoordonneeColonne] == 1)
                    i--;
                else
                    Matrice[CoordonneeLigne, CoordonneeColonne] = 1;
            }
            for (int i = 0; i < nombredecelluleinitialle2; i++)
            {
                int CoordonneeLigne = generateur.Next(0, Matrice.GetLength(0));
                int CoordonneeColonne = generateur.Next(0, Matrice.GetLength(1));
                if (Matrice[CoordonneeLigne, CoordonneeColonne] == 1)
                    i--;
                else
                {
                    if (Matrice[CoordonneeLigne, CoordonneeColonne] == 2)
                        i--;

                    else
                        Matrice[CoordonneeLigne, CoordonneeColonne] = 2;
                }
            }

        }

        static int CompteurDeVoisin(int[,] Matrice, int[] Coordonnee, int NumeroPopulation)
        {// Cette fonction compte le nombre de cellule vivante d'une certaine population autour d'une cellule dont les coordonnées ont été entrer en paramètre .
            int compteur = 0;
            for (int indexLigne = 0; indexLigne < 3; indexLigne++)
            {
                for (int indexColonne = 0; indexColonne < 3; indexColonne++)
                {
                    int CoordonneeLigne = (((Coordonnee[0] - 1 + indexLigne) % Matrice.GetLength(0)) + Matrice.GetLength(0)) % Matrice.GetLength(0);
                    int CoordonneeColonne = (((Coordonnee[1] - 1 + indexColonne) % Matrice.GetLength(1)) + Matrice.GetLength(1)) % Matrice.GetLength(1);

                    if ((Coordonnee[0] != CoordonneeLigne) || (Coordonnee[1] != CoordonneeColonne))
                    {

                        if (Matrice[CoordonneeLigne, CoordonneeColonne] == NumeroPopulation)
                            compteur++;
                    }

                }
            }
            return compteur;
        }

        static void Generation(int NombreDeVoisin, int[] Coordonnee, int[,] MatriceLecture, int[,] MatriceEcriture, int NumeroPopulation)
        {//Cette fonction change le statue de la cellule dont les coordonnées ont été entré en paramètre, c'est à dire morte ou vivante.
            switch (NombreDeVoisin)
            {
                case 0:
                case 1:
                    MatriceEcriture[Coordonnee[0], Coordonnee[1]] = 0;
                    break;
                case 2:
                    if (MatriceLecture[Coordonnee[0], Coordonnee[1]] == 0)
                    {
                        MatriceEcriture[Coordonnee[0], Coordonnee[1]] = 0;
                        break;
                    }
                    else
                    {
                        MatriceEcriture[Coordonnee[0], Coordonnee[1]] = NumeroPopulation;
                        break;
                    }
                case 3:
                    MatriceEcriture[Coordonnee[0], Coordonnee[1]] = NumeroPopulation;
                    break;
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    MatriceEcriture[Coordonnee[0], Coordonnee[1]] = 0;
                    break;

            }
        }

        static void Reecriture(int[,] Matrice1, int[,] Matrice2)
        {//Cette fonction change les valeurs presentent dans la Matrice 1 à partir des valeurs de la Matrice 2. 

            for (int indexLigne = 0; indexLigne < Matrice1.GetLength(0); indexLigne++)
            {
                for (int indexColonne = 0; indexColonne < Matrice1.GetLength(1); indexColonne++)
                {
                    Matrice1[indexLigne, indexColonne] = Matrice2[indexLigne, indexColonne];
                }
            }
        }

        static void Tour(int[,] Matrice, bool EtatIntermediaire)
        {//Cette fonction sert à exécuter plusieurs fonctions precedemment définie selon un ordre précis.
            int NombreDeVoisin = 0;
            int[] Coordonnee = new int[2];
            int[,] MatriceTemporaire1 = new int[Matrice.GetLength(0), Matrice.GetLength(1)];
            int[,] MatriceTemporaire2 = new int[Matrice.GetLength(0), Matrice.GetLength(1)];

            for (int indexLigne = 0; indexLigne < Matrice.GetLength(0); indexLigne++)
            {
                for (int indexColonne = 0; indexColonne < Matrice.GetLength(1); indexColonne++)
                {
                    Coordonnee[0] = indexLigne;
                    Coordonnee[1] = indexColonne;
                    NombreDeVoisin = CompteurDeVoisin(Matrice, Coordonnee, 1);
                    Generation(NombreDeVoisin, Coordonnee, Matrice, MatriceTemporaire1, 1);

                }

            }
            for (int indexLigne = 0; indexLigne < Matrice.GetLength(0); indexLigne++)
            {
                for (int indexColonne = 0; indexColonne < Matrice.GetLength(1); indexColonne++)
                {
                    Coordonnee[0] = indexLigne;
                    Coordonnee[1] = indexColonne;
                    NombreDeVoisin = CompteurDeVoisin(Matrice, Coordonnee, 2);
                    Generation(NombreDeVoisin, Coordonnee, Matrice, MatriceTemporaire2, 2);

                }
            }
            int[,] Matrice2 = AdditionDe2matrice(MatriceTemporaire1, MatriceTemporaire2, Matrice);
            // Partie relatif à l'affichage des états intermédiares, peut étre activé ou non
            if (EtatIntermediaire == true)
                AffichageEtatIntermédiaire(Matrice2, Matrice);         
            Reecriture(Matrice, Matrice2);
        }

        static int CompteurDeCelluleVivante(int[,] Matrice)
        {// Cette fonction compte le nombre de cellule vivante sur une matrice entrée en paramètre.
            int NombreDeCelluleVivante = 0;
            for (int indexLigne = 0; indexLigne < Matrice.GetLength(0); indexLigne++)
            {
                for (int indexColonne = 0; indexColonne < Matrice.GetLength(1); indexColonne++)
                {
                    if (Matrice[indexLigne, indexColonne] == 1)
                        NombreDeCelluleVivante++;
                }
            }
            Console.WriteLine(" Il y a " + NombreDeCelluleVivante + " cellule(s) vivante(s)");
            return NombreDeCelluleVivante;
        }

        static void AffichageEtatIntermédiaire(int[,] MatriceTransition, int[,] Matrice)
        {// Cette fonction permet d'afficher l'état intermédiaire entre 2 générations de cellule. Pour cela, elle compare les changement entre 2 matrices fournis. 
            int[,] MatriceEtatIntermediaire = new int[Matrice.GetLength(0), Matrice.GetLength(1)];
            for (int indexLigne = 0; indexLigne < Matrice.GetLength(0); indexLigne++)
            {
                for (int indexColonne = 0; indexColonne < Matrice.GetLength(1); indexColonne++)
                {
                    if (Matrice[indexLigne, indexColonne] == MatriceTransition[indexLigne, indexColonne])
                        MatriceEtatIntermediaire[indexLigne, indexColonne] = Matrice[indexLigne, indexColonne];
                    if (MatriceTransition[indexLigne, indexColonne] == 1 && Matrice[indexLigne, indexColonne] == 0)
                        MatriceEtatIntermediaire[indexLigne, indexColonne] = 3;
                    if (MatriceTransition[indexLigne, indexColonne] == 2 && Matrice[indexLigne, indexColonne] == 0)
                        MatriceEtatIntermediaire[indexLigne, indexColonne] = 4;
                    if (MatriceTransition[indexLigne, indexColonne] == 0 && Matrice[indexLigne, indexColonne] == 1)
                        MatriceEtatIntermediaire[indexLigne, indexColonne] = 5;
                    if (MatriceTransition[indexLigne, indexColonne] == 0 && Matrice[indexLigne, indexColonne] == 2)
                        MatriceEtatIntermediaire[indexLigne, indexColonne] = 6;
                }
            }

            PassageEntierSymbole(MatriceEtatIntermediaire);


        }

        static void PassageEntierSymbole(int[,] Matrice)
        {//Cette fonction permet de passer à un affiche par symbole sur la console à partir d'une matrice d'entier.
            string[,] MatriceSymbole = new string[Matrice.GetLength(0), Matrice.GetLength(1)];
            for (int indexLigne = 0; indexLigne < Matrice.GetLength(0); indexLigne++)
            {
                for (int indexColonne = 0; indexColonne < Matrice.GetLength(1); indexColonne++)
                {
                    if (Matrice[indexLigne, indexColonne] == 0)
                        MatriceSymbole[indexLigne, indexColonne] = ".";
                    if (Matrice[indexLigne, indexColonne] == 1)
                        MatriceSymbole[indexLigne, indexColonne] = "#";
                    if (Matrice[indexLigne, indexColonne] == 2)
                        MatriceSymbole[indexLigne, indexColonne] = "O";
                    if (Matrice[indexLigne, indexColonne] == 3)
                        MatriceSymbole[indexLigne, indexColonne] = "*";
                    if (Matrice[indexLigne, indexColonne] == 4)
                        MatriceSymbole[indexLigne, indexColonne] = "°";
                    if (Matrice[indexLigne, indexColonne] == 5)
                        MatriceSymbole[indexLigne, indexColonne] = "-";
                    if (Matrice[indexLigne, indexColonne] == 6)
                        MatriceSymbole[indexLigne, indexColonne] = ",";


                }
            }
            AffichageGrille2(MatriceSymbole);
        }

        static void AffichageGrille2(string[,] Matrice)
        {//Cette fonction permet d'afficher une matrice de caractère dans la console.
            for (int indexLigne = 0; indexLigne < Matrice.GetLength(0); indexLigne++)
            {
                for (int indexColonne = 0; indexColonne < Matrice.GetLength(1); indexColonne++)
                {
                    Console.Write(Matrice[indexLigne, indexColonne]);
                }
                Console.WriteLine();
            }
        }

        static int[,] AdditionDe2matrice(int[,] Matrice1, int[,] Matrice2, int[,] MatriceTourPrecedent)
        {// Cette fonction additionne 2 matrices entrées en paramètre en suivant les règles de remplissage.
            int[,] Matricefinale = new int[Matrice1.GetLength(0), Matrice1.GetLength(1)];
            for (int indexLigne = 0; indexLigne < Matrice1.GetLength(0); indexLigne++)
            {
                for (int indexColonne = 0; indexColonne < Matrice1.GetLength(1); indexColonne++)
                {
                    if (Matrice1[indexLigne, indexColonne] == Matrice2[indexLigne, indexColonne])
                        Matricefinale[indexLigne, indexColonne] = Matrice1[indexLigne, indexColonne];
                    else
                    {
                        if (Matrice1[indexLigne, indexColonne] == 0 && Matrice2[indexLigne, indexColonne] != 0)
                        {
                            Matricefinale[indexLigne, indexColonne] = Matrice2[indexLigne, indexColonne];
                        }
                        else
                        {
                            if (Matrice2[indexLigne, indexColonne] == 0 && Matrice1[indexLigne, indexColonne] != 0)
                            {
                                Matricefinale[indexLigne, indexColonne] = Matrice1[indexLigne, indexColonne];
                            }
                            else
                            {
                                Matricefinale[indexLigne, indexColonne] = CasParticulier(indexLigne, indexColonne, MatriceTourPrecedent);
                            }

                        }
                    }
                }
            }
            return Matricefinale;
        }

        static int CasParticulier(int CoordonneeLigne, int CoordonneeColonne, int[,] MatriceTourPrecedent)
        {// Cette fonction gère les cas particulier pour savoir si une cellule appartient à l'une ou l'autre population
            int[] Coordonnee = { CoordonneeLigne, CoordonneeColonne };
            int Population1 = CompteurDeVoisinDe2ndDegrès(MatriceTourPrecedent, Coordonnee, 1);
            int Population2 = CompteurDeVoisinDe2ndDegrès(MatriceTourPrecedent, Coordonnee, 2);
            //On regarde si la cellule n'appartenait deja pas à une population
            if (MatriceTourPrecedent[CoordonneeLigne, CoordonneeColonne] == 1)
                return 1;
            if (MatriceTourPrecedent[CoordonneeLigne, CoordonneeColonne] == 2)
                return 2;
            //Si ce n'est pas le cas, donc la cellule était morte au tour precedent, on va affecter cette cellule à une population suivant les règles établies.
            else
            {
                // On regarde les voisins de 2nd degrès
                if (Population1 > Population2)
                    return 1;
                if (Population2 > Population1)
                    return 2;
                else
                {
                    // On regarde le nombre total de cellule par population
                    int TotalPopulation1 = CompteurPopulation(MatriceTourPrecedent, 1);
                    int TotalPopulation2 = CompteurPopulation(MatriceTourPrecedent, 2);
                    if (TotalPopulation1 > TotalPopulation2)
                        return 1;
                    else
                    {
                        if (TotalPopulation2 > TotalPopulation1)
                            return 2;
                        // En cas dégalité la cellule reste morte comme convenue dans les règles.
                        else
                            return 0;
                    }
                }
            }

        }

        static int CompteurDeVoisinDe2ndDegrès(int[,] Matrice, int[] Coordonnee, int NumeroPopulation)
        {// Cette fonction compte le nombre de voisin de second degrès pour une cellule dont les coordonnées ont été entrer en paramètre.
            int compteur = 0;
            for (int indexLigne = 0; indexLigne < 9; indexLigne++)
            {
                for (int indexColonne = 0; indexColonne < 9; indexColonne++)
                {
                    int CoordonneeLigne = (((Coordonnee[0] - 1 + indexLigne) % Matrice.GetLength(0)) + Matrice.GetLength(0)) % Matrice.GetLength(0);
                    int CoordonneeColonne = (((Coordonnee[1] - 1 + indexColonne) % Matrice.GetLength(1)) + Matrice.GetLength(1)) % Matrice.GetLength(1);

                    if ((Coordonnee[0] != CoordonneeLigne) || (Coordonnee[1] != CoordonneeColonne))
                    {

                        if (Matrice[CoordonneeLigne, CoordonneeColonne] == NumeroPopulation)
                            compteur++;
                    }

                }
            }
            return compteur;
        }

        static int CompteurPopulation(int[,] Matrice, int NumeroPopulation)
        {// Cette fonction compte le nombre de cellule vivante pour une population donnée dans une matrice.
            int Compteur = 0;
            for (int indexLigne = 0; indexLigne < Matrice.GetLength(0); indexLigne++)
            {
                for (int indexColonne = 0; indexColonne < Matrice.GetLength(1); indexColonne++)
                {
                    if (Matrice[indexLigne, indexColonne] == NumeroPopulation)
                        Compteur++;
                }
            }
            return Compteur;
        }

        [System.STAThreadAttribute()]
        static void Main(string[] args)
        {
            string Mode=" ";
            do
            { Console.WriteLine("Jeu de la vie, Veuillez selectionner votre mode de jeu ( taper la lettre correspondante puis entrer)");
                Console.WriteLine("a) Mode de jeu avec 1 population");
                Console.WriteLine("b) Mode de jeu avec 2 population");
            Mode = Console.ReadLine();
            } while (Mode != "a" && Mode != "b");


            if (Mode == "a")
// RELATIF AU MODE DE JEU 1 POPULATION
            {
                // 1e partie, ici est défini toute les instructions permettants de demander à l'utilisateur les paramètres choisies
                Console.WriteLine("Jeu de la vie");
                Console.WriteLine("Merci de bien vouloir intialiser les paramètres de génération");
                Console.WriteLine("Entrer taux de remplissage de cellules vivantes au départ c'est à dire une valeur réelle comprise entre [0.1,0.9] ");
                double TauxDeRemplissage = Convert.ToDouble(Console.ReadLine());
                while ((TauxDeRemplissage <= 0.1) || (TauxDeRemplissage > 0.9))
                {
                    Console.WriteLine("Erreur valeur saisie mauvaise !");
                    Console.WriteLine("Merci de bien vouloir entrer taux de remplissage de cellules vivantes au départ c'est à dire une valeur réelle comprise entre [0.1,0.9] ");
                    TauxDeRemplissage = Convert.ToDouble(Console.ReadLine());
                }

                Console.WriteLine("Entrer la valeur de la longueur de la grille");
                int Longueur = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Entrer la valeur de la largeur de la grille");
                int Largeur = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Voulez vous afficher les etats intermediaire entre les tours ?");
                Console.WriteLine("Si oui taper 1 sinon sur n'importe quelle autre touche ?");

                string Valeur = Console.ReadLine();
                bool AffichageEtatIntermediaire = false;
                if (Valeur == "1")
                    AffichageEtatIntermediaire = true;

                int[,] tableaudetest = Grille(Longueur, Largeur);
                Grainedegénération(tableaudetest, TauxDeRemplissage);
                int CaseRemplie = Convert.ToInt32(tableaudetest.Length * TauxDeRemplissage);
                Console.WriteLine("Le taux de remplissage initiale est de " + TauxDeRemplissage + " soit de " + CaseRemplie + " cellule sur " + tableaudetest.Length);
                Console.WriteLine();
                AffichageGrille(tableaudetest);
                Console.WriteLine("Appuyer sur entrer pour passer au tour suivant ");
                Console.WriteLine("Appuyer sur espace puis entrer pour quitter");


                // 2eme zone, ici est paramétré la GUI
                Fenetre gui = new Fenetre(tableaudetest, 15, 0, 0, "Jeu de la vie");
                String Touche = "0";
                int NumeroDeLaGeneration = 0;
                Console.WriteLine("Appuyer sur une touche pour lancer la 1e generation.");
                Console.ReadKey();


                // 3eme zone, ici est défini la boucle qui permet de passer d'une génération à une autre
                while (Touche != " ")
                {
                    Tour(tableaudetest, AffichageEtatIntermediaire);
                    Console.WriteLine();
                    AffichageGrille(tableaudetest);
                    Console.WriteLine();
                    NumeroDeLaGeneration++;
                    gui.Rafraichir();
                    gui.ChangerMessage("Génération " + NumeroDeLaGeneration + " Il y a " + CompteurDeCelluleVivante(tableaudetest) + " cellules vivantes");
                    Touche = Console.ReadLine();
                }

            }
        
    
// RELATIF AU MODE DE JEU 2 POPULATIONS 



            else
            {
                // 1e partie, ici est défini toute les instructions permettants de demander à l'utilisateur les paramètres choisies
                Console.WriteLine("Jeu de la vie");
                Console.WriteLine("Merci de bien vouloir intialiser les paramètres de génération");
                Console.WriteLine("Entrer taux de remplissage de cellules vivantes pour la population 1 au départ c'est à dire une valeur réelle comprise entre [0.1,0.8] ");
                double TauxDeRemplissage1 = Convert.ToDouble(Console.ReadLine());
                while ((TauxDeRemplissage1 < 0.1) || (TauxDeRemplissage1 > 0.8))
                {
                    Console.WriteLine("Erreur valeur saisie mauvaise !");
                    Console.WriteLine("Merci de bien vouloir entrer taux de remplissage de cellules vivantes pour la population 1 au départ c'est à dire une valeur réelle comprise entre [0.1,0.8] ");
                    TauxDeRemplissage1 = Convert.ToDouble(Console.ReadLine());
                }
                Console.WriteLine("Entrer taux de remplissage de cellules vivantes pour la population 2 au départ c'est à dire une valeur réelle comprise entre [0.1," + (0.9 - TauxDeRemplissage1) + "]");
                double TauxDeRemplissage2 = Convert.ToDouble(Console.ReadLine());
                while (((TauxDeRemplissage2 < 0.1) || (TauxDeRemplissage2 > 0.9 - TauxDeRemplissage1)) && TauxDeRemplissage2 != 0.1)
                {
                    Console.WriteLine("Erreur valeur saisie mauvaise !");
                    Console.WriteLine("Merci de bien vouloir entrer taux de remplissage de cellules vivantes pour la population2 au départ c'est à dire une valeur réelle comprise entre [0.1," + (0.9 - TauxDeRemplissage1) + "]");
                    TauxDeRemplissage2 = Convert.ToDouble(Console.ReadLine());
                }

                Console.WriteLine("Entrer la valeur de la longueur de la grille");
                int Longueur = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Entrer la valeur de la largeur de la grille");
                int Largeur = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Voulez vous afficher les etats intermediaire entre les tours ?");
                Console.WriteLine("Si oui taper 1 sinon sur n'importe quelle autre touche ?");

                string Valeur = Console.ReadLine();
                bool AffichageEtatIntermediaire = false;
                if (Valeur == "1")
                    AffichageEtatIntermediaire = true;


                int[,] tableaudetest = Grille(Longueur, Largeur);


                GraineDeGenerationDe2Populations(tableaudetest, TauxDeRemplissage1, TauxDeRemplissage2);
                int CaseRemplie = Convert.ToInt32(tableaudetest.Length * TauxDeRemplissage1) + Convert.ToInt32(tableaudetest.Length * TauxDeRemplissage2);
                Console.WriteLine("Le taux de remplissage initiale est de " + TauxDeRemplissage1 + " pour la pop 1 et de " + TauxDeRemplissage2 + " pour la pop 2 soit de " + CaseRemplie + " cellule sur " + tableaudetest.Length);
                Console.WriteLine();

                AffichageGrille(tableaudetest);
                Console.WriteLine("Appuyer sur entrer pour passer au tour suivant");
                Console.WriteLine("Appuyer sur espace puis entrer pour quitter");
                string Touche = "0";
                Console.ReadKey();


                // 2eme zone, ici est paramétré la GUI
                Fenetre gui = new Fenetre(tableaudetest, 15, 0, 0, "Jeu de la vie");
                int NumeroDeLaGeneration = 0;
                 gui.Rafraichir();
                 gui.ChangerMessage("Génération " + NumeroDeLaGeneration);
               

                // 3eme zone, ici est défini la boucle qui permet de passer d'une génération à une autre
                while (Touche != " ")
                {
                    Tour(tableaudetest, AffichageEtatIntermediaire);
                    Console.WriteLine();
                    AffichageGrille(tableaudetest);
                    Console.WriteLine();
                    NumeroDeLaGeneration++;
                    int Pop1 = CompteurPopulation(tableaudetest, 1);
                    int Pop2 = CompteurPopulation(tableaudetest, 2);
                    Console.WriteLine("Il y a " + Pop1 + " Cellule vivante pour la population 1");
                    Console.WriteLine("Il y a " + Pop2 + " Cellule vivante pour la population 2");
                    gui.Rafraichir();
                    gui.ChangerMessage("Génération " + NumeroDeLaGeneration);
                    Touche = Console.ReadLine();

                }
            }
        }
    }
}
