
# BATTLE BOARD GAME

Below are the tasks, the completed ones are marked as DONE.


**Menu**

• The game must have a main menu scene to enter the game (DONE)

• Bonus points if the menu has other options like resolution, sound etc. (DONE)

**Board creation**

• Required board is a simple 16x16 board with square tiles (DONE)

• Bonus points if the creation is dynamic (DONE)

• Bonus points if the tiles have different shapes (hexagon, octagon etc.)

**Players**

• The game needs 2 players (DONE)

• Each character can be controlled on the same keyboard, since it is a turn-based game (DONE)

• Bonus points for AI controlled character 

• Bonus points for an option to choose between AI player or 2 local players on main menu

• Each player can only act on its own turn (DONE)

**Characters**

• Each player controls its own character (DONE)

• Each character has X health points (DONE)

• Each character has Y attack points (DONE)

• The values of X and Y are defined by you (DONE)

• Bonus points if you create more characters with different settings (DONE)

• Bonus points for an option to choose characters for each player on the main menu (DONE)

**Turns**

• Players can move orthogonally to each adjacent tile upon mouse click (DONE)

• Each character cannot occupy tiles where the other is at (DONE)

• Bonus points to highlight possible movements from current tile (DONE)

• Each player has 3 available moves per turn (DONE)

• Provide feedback for the number of available moves (DONE)

• The turn is changed when the player runs out of move (DONE)

**Camera**

• Implement a camera system that smoothly follows the turn player (DONE)

**Tiles**

• Each tile must be populated with a collectable item (DONE)

• Two tiles must be free at the beginning to place  characters (DONE)

• Bonus points for dynamic starting points (DONE)

**Collectables**

• Gain extra move for the turn (DONE)

• Gain extra attack for the rest of the turn (DONE)

• Recover some health (DONE)

• Gain extra dices for the rest of the turn (DONE)

• Players collect each one by walking over its tile (DONE)

• If there are 10% of collectables left, the game auto-fills any available tile with a new collectable (DONE)

• Bonus points if each collectable has tiers with different amounts of gain

**Battle**

• Battle happens when a player is adjacent to the other orthogonally after moving (DONE)

• If a battle happens on the last move of the turn, the turn only ends after the  battle (DONE) 

• Bonus points for diagonal battles (DONE)

• The battle is decided over auto dice roll (DONE)

• Players have dices of different colors (DONE)

• Color choices are yours to make (DONE)

• Each dice is six-sided (DONE)

• Bonus points for any kind of different dice (twelve-sided, ten-sided etc)

• Battle specification (DONE)

**Sounds**

• At least one sound is required for any action in game like moving, attacking, collecting etc. (DONE)

• Bonus points if every action has its own sound (DONE)

**Effects**

• At least one particle effect is required for any action in game like moving, attacking, collecting etc. (DONE)

• Bonus points if every action has its own effect (DONE)

**General rules**

• The game ends when one player runs out of health (DONE)

• Display an end game screen with the winner (DONE)

• Display a replay button and a  back to menu button 

**Profiling**

• The most problematic section in my game was a auxiliar camera created to show the dice rolls, what I would do to improve the performance is disabling it when the dice roll is not happening or evven using just one camera and transitioning the main camera to the dice roll platform