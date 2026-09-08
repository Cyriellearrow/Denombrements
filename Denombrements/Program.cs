using System;

namespace Denombrements
{
    /// <summary>
    /// Application console permettant d'effectuer des calculs de dénombrement :
    /// - Permutations (n!)
    /// - Arrangements (A_t^n)
    /// - Combinaisons (C_t^n)
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Variable pour stocker le choix de l'utilisateur dans le menu
            int choixMenu = -1;

            // Boucle principale qui maintient le programme ouvert tant que l'utilisateur ne choisit pas d'en sortir (0)
            do
            {
                // Affichage des options du menu
                Console.WriteLine("Permutation ...................... 1");
                Console.WriteLine("Arrangement ...................... 2");
                Console.WriteLine("Combinaison ...................... 3");
                Console.WriteLine("Quitter .......................... 0");
                Console.Write("Choix :                            ");

                // Gestion des erreurs de saisie (ex: si l'utilisateur tape du texte au lieu d'un nombre)
                try
                {
                    // Lecture et conversion de la saisie utilisateur
                    choixMenu = int.Parse(Console.ReadLine());

                    // Si l'utilisateur choisit 0, on ferme directement l'application
                    if (choixMenu == 0)
                    {
                        Environment.Exit(0);
                    }

                    // --- TRAITEMENT DU CHOIX 1 : PERMUTATION ---
                    if (choixMenu == 1)
                    {
                        Console.Write("Nombre total d'éléments à gérer = ");
                        int nombreElements = int.Parse(Console.ReadLine());

                        // Vérification de la contrainte : le nombre doit être supérieur ou égal à 0
                        if (nombreElements < 0)
                        {
                            Console.WriteLine("Erreur : Le nombre d'éléments doit être positif.");
                            continue; // Retourne au début de la boucle sans faire le calcul
                        }

                        // Variable stockant le résultat de la factorielle
                        long resultat = 1;
                        bool depassement = false;

                        // Calcul du produit factoriel : n! = 1 * 2 * 3 * ... * n
                        for (int k = 1; k <= nombreElements; k++)
                        {
                            // Test préventif de dépassement : si le produit va dépasser la limite maximale d'un long
                            if (resultat > long.MaxValue / k)
                            {
                                depassement = true;
                                break; // Arrêt de la boucle car le résultat sera incorrect
                            }
                            resultat *= k;
                        }

                        // Affichage du résultat ou de l'erreur en cas de nombre trop grand
                        if (depassement)
                        {
                            Console.WriteLine("Erreur : Le résultat est trop grand pour être stocké.");
                        }
                        else
                        {
                            Console.WriteLine(nombreElements + "! = " + resultat);
                        }
                    }
                    // --- TRAITEMENT DES CHOIX 2 ET 3 : ARRANGEMENT ET COMBINAISON ---
                    else if (choixMenu == 2 || choixMenu == 3)
                    {
                        Console.Write("Nombre total d'éléments à gérer (t) = ");
                        int totalElements = int.Parse(Console.ReadLine());

                        Console.Write("Nombre d'éléments dans le sous-ensemble (n) = ");
                        int tailleSousEnsemble = int.Parse(Console.ReadLine());

                        // 1. Vérification que les nombres ne sont pas négatifs
                        if (totalElements < 0 || tailleSousEnsemble < 0)
                        {
                            Console.WriteLine("Erreur : Les valeurs doivent être positives.");
                            continue;
                        }

                        // 2. Vérification que la taille du sous-ensemble ne dépasse pas le total d'éléments
                        if (tailleSousEnsemble > totalElements)
                        {
                            Console.WriteLine("Erreur : Le sous-ensemble (n) ne peut pas être plus grand que le total (t).");
                            continue;
                        }

                        // --- Calcul de l'arrangement A(t, n) ---
                        long arrangement = 1;
                        bool depassementArrangement = false;

                        // Produit de (t - n + 1) jusqu'à t
                        for (int k = (totalElements - tailleSousEnsemble + 1); k <= totalElements; k++)
                        {
                            if (arrangement > long.MaxValue / k)
                            {
                                depassementArrangement = true;
                                break;
                            }
                            arrangement *= k;
                        }

                        if (depassementArrangement)
                        {
                            Console.WriteLine("Erreur : Le résultat est trop grand pour être stocké.");
                            continue;
                        }

                        // Affichage si l'utilisateur a demandé un arrangement
                        if (choixMenu == 2)
                        {
                            Console.WriteLine("A(" + totalElements + "/" + tailleSousEnsemble + ") = " + arrangement);
                        }
                        // Calcul complémentaire si l'utilisateur a demandé une combinaison
                        else
                        {
                            // --- Calcul de la factorielle du sous-ensemble n! ---
                            long factorielleSousEnsemble = 1;
                            bool depassementFactorielle = false;

                            for (int k = 1; k <= tailleSousEnsemble; k++)
                            {
                                if (factorielleSousEnsemble > long.MaxValue / k)
                                {
                                    depassementFactorielle = true;
                                    break;
                                }
                                factorielleSousEnsemble *= k;
                            }

                            if (depassementFactorielle)
                            {
                                Console.WriteLine("Erreur : Le résultat est trop grand pour être stocké.");
                            }
                            else
                            {
                                // Calcul de C(t, n) = A(t, n) / n!
                                long combinaison = arrangement / factorielleSousEnsemble;
                                Console.WriteLine("C(" + totalElements + "/" + tailleSousEnsemble + ") = " + combinaison);
                            }
                        }
                    }
                    else
                    {
                        // Cas où l'utilisateur entre un chiffre autre que 0, 1, 2 ou 3
                        Console.WriteLine("Choix invalide, veuillez rechoisir une option.");
                    }
                }
                catch (FormatException)
                {
                    // Intercepte les erreurs si l'utilisateur saisit des lettres ou des symboles au lieu d'un nombre
                    Console.WriteLine("Erreur : Veuillez saisir un nombre entier valide.");
                }

                // Ligne vide pour aérer la console avant le prochain tour de boucle
                Console.WriteLine();

            } while (choixMenu != 0); // Le programme recommence tant que l'utilisateur ne tape pas 0
        }
    }
}