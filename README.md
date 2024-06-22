# Projet .NET/C# 
## Sujet
1. Objectifs :  
Le projet doit refl�ter votre ma�trise de la plateforme .NET Core. Votre th�me fera l�objet d�une validation et sera individuel.

2. Technologies � utiliser
* Backoffice
  * SGBD
     * SQL Server
  * Acc�s aux donn�es
    * Entity framework
  * Pr�sentation
    * Razor page
* Frontoffice
  * SGBD
    * SQL Server
  * Acc�s aux donn�es
    * ADO.NET
  * Pr�sentation
    * MVC (core)
    * REST API (qlq partie)

3. Autres requirements
* Pagination
* Export PDF
* Import CSV
* CSS
* Indexation   
...

4. Difficult�s
* BDD
* M�tier (pas seulement des CRUD)
* Affichage (design convivial)

## Th�me
### Call For Bids Platform
Une plateforme d'appel d'offres qui permet de g�rer les offres d'une entreprise, les fournisseurs et les candidatures. Les utilisateurs ont la possibilit� de consulter la liste des offres et de les exporter. Les fournisseurs ont la possibilit� de s'inscrire et de soumettre des candidatures. Le back-office se focalise sur la gestion des offres, le suivi des fournisseurs, et la validation des candidatures.  

1. Front-office : 
- inscription et login utilisateur (les utilisateurs sont des fournisseurs)
- Liste pagin�e avec recherche des offres en cours
- soumettre � un offre
- ajouter des documents � une soumission
- suivi de l'�tat de la soumission avec possibilit� d�annulation
(pending, accepted, rejected)
- Export des offres en PDF
- Notification apr�s confirmation d'une soumission (� voir)
  
2. Back-office : 
- login admin
- gestion des offres (CRUD)
- Liste et d�tails de chaque fournisseur
- statistique de r�ponses par offre/fournisseur (� voir)
- traitement des soumissions/candidatures (accept, reject)
- Historique des propositions d�offre des fournisseurs (offre, date, etat de la proposition (accept�e, refus�e) )
- importation de fichier CSV pour l�insertion de nouveaux projets

3. Difficult�s : 
- un appel est actif pendant une p�riode et la candidature est automatiquement rejet�e si le fournisseur d�passe la limite
- l'�tat de d�une candidature varie (pending, accepted, rejected)
- difficult� du c�t� affichage sur la fonctionnalit� de notification (� voir)
- ajout multiple de projets � partir d'un fichier CSV

### Installation du projet:
* `git clone`
* Changer  �Server� et �Database� dans `appsettings.json`
* Outils > Gestionnaire de package NuGet > Console de gestionnaire de package
    * Add-Migration InitialCreate
    * Update-Database

### MCD Initial de l'application
![Alt text](MCD.png?raw=true "MCD")
