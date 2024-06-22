# Projet .NET/C# 
## Sujet
1. Objectifs :  
Le projet doit refléter votre maîtrise de la plateforme .NET Core. Votre thème fera l’objet d’une validation et sera individuel.

2. Technologies à utiliser
* Backoffice
  * SGBD
     * SQL Server
  * Accès aux données
    * Entity framework
  * Présentation
    * Razor page
* Frontoffice
  * SGBD
    * SQL Server
  * Accès aux données
    * ADO.NET
  * Présentation
    * MVC (core)
    * REST API (qlq partie)

3. Autres requirements
* Pagination
* Export PDF
* Import CSV
* CSS
* Indexation   
...

4. Difficultés
* BDD
* Métier (pas seulement des CRUD)
* Affichage (design convivial)

## Thème
### Call For Bids Platform
Une plateforme d'appel d'offres qui permet de gérer les offres d'une entreprise, les fournisseurs et les candidatures. Les utilisateurs ont la possibilité de consulter la liste des offres et de les exporter. Les fournisseurs ont la possibilité de s'inscrire et de soumettre des candidatures. Le back-office se focalise sur la gestion des offres, le suivi des fournisseurs, et la validation des candidatures.  

1. Front-office : 
- inscription et login utilisateur (les utilisateurs sont des fournisseurs)
- Liste paginée avec recherche des offres en cours
- soumettre à un offre
- ajouter des documents à une soumission
- suivi de l'état de la soumission avec possibilité d’annulation
(pending, accepted, rejected)
- Export des offres en PDF
- Notification après confirmation d'une soumission (à voir)
  
2. Back-office : 
- login admin
- gestion des offres (CRUD)
- Liste et détails de chaque fournisseur
- statistique de réponses par offre/fournisseur (à voir)
- traitement des soumissions/candidatures (accept, reject)
- Historique des propositions d’offre des fournisseurs (offre, date, etat de la proposition (acceptée, refusée) )
- importation de fichier CSV pour l’insertion de nouveaux projets

3. Difficultés : 
- un appel est actif pendant une période et la candidature est automatiquement rejetée si le fournisseur dépasse la limite
- l'état de d’une candidature varie (pending, accepted, rejected)
- difficulté du côté affichage sur la fonctionnalité de notification (à voir)
- ajout multiple de projets à partir d'un fichier CSV

### Installation du projet:
* `git clone`
* Changer  “Server” et “Database” dans `appsettings.json`
* Outils > Gestionnaire de package NuGet > Console de gestionnaire de package
    * Add-Migration InitialCreate
    * Update-Database

### MCD Initial de l'application
![Alt text](MCD.png?raw=true "MCD")
