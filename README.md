# SafeZoneVR

SafeZoneVR est un projet Unity en réalité virtuelle (XR Interaction Toolkit) qui met le joueur dans un scénario d’urgence en bureau : détecter des anomalies, réagir à un départ de feu, utiliser un extincteur et prendre la bonne décision selon la situation.

## Objectif du projet
- Proposer une expérience VR courte et interactive autour de la sécurité incendie
- Mettre en avant la prise de décision (fuir vs. intervenir) selon les scènes
- Mesurer la performance globale (temps total de la partie) et afficher un feedback final personnalisé

## Fonctionnalités principales
- **Menu de début** : génération d’un pseudo aléatoire (Player####) + bouton Start
- **Mécaniques XR** :
  - Interaction avec les objets (grab)
  - Extincteur utilisable (spray) et feux interactifs (extinction)
- **Gestion de feu** :
  - Countdown avant démarrage du feu
  - Alarme sonore au déclenchement
  - Contrôle des feux et arrêt de l’alarme quand tout est éteint
- **Scènes** :
  - Scènes de gameplay avec objectifs différents (fuite ou extinction selon la scène)
  - Scène de fin : message personnalisé basé sur le temps total (sans afficher le temps)
- **Session persistante** :
  - `GameSession` conserve le pseudo et le temps total entre les scènes (`DontDestroyOnLoad`)

## Structure des scènes (exemple)
- `BeginScene` : pseudo + démarrage
- `Scene1` : objectifs de conformité (objets à trouver)
- `Scene2` : scénario feu + bonne décision = sortir
- `Scene3` : scénario feu + bonne décision = éteindre
- `EndScene` : message de fin personnalisé

## Technologies
- Unity (URP)
- XR Interaction Toolkit (OpenXR)
- TextMeshPro (UI)

## Installation / Lancement
1. Cloner le repo :
   ```bash
   git clone https://github.com/YasmineChettah/SafeZone.git
