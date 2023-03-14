Source Code goes Here

Make a branch for each milestone.
Commit work for each milestone to the branch.
Merge the brance back to the master for each Milestone
Produce a Build of each Milestone and add to build folder 


CONCEPT 

Genre:
Rhythm Arcade 
Description Co-op:
A game that can be played by 2 people. The objective is to survive as long as possible, as waves of enemies fly across the screen and try to hit you and your teammate. Rhythm based, so players must shoot on beat. 
Mechanic for MVP:
I want to have two ships that fly around the screen, feel good to control, and the ability to shoot on beat.
Design:
Players will control the aim of each other. There goal is to stay alive as long as possible.
Players can only shoot or reload on beat 
Players can find guns which spawn randomly, different types of guns have a different time signature for shooting and reloading (affects which beats the player must press on) 
Projectiles of guns should have different properties (some go through enemies, some explode and do damage to multiple enemies)

Patterns:
Observer pattern on players to observe the rhythm manager so only one class performs calculations on the audio buffer.
Systems:

Rhythm system – guns need the ability to track different time signatures. Rhythm manager only counts each sixteenth note so guns need to keep track of which ones they can shoot on and which ones they need to reload on.
Guns should account for multi-beat reload times
Spawner – ability to spawn ability and static objects around the screen. 
Collision system – Needs to handle projectiles, players, and walls, abilities. Not sure if this should all be one class, or multiple which inherit from a single class. I want the outcome of different object types colliding to be very modular and easy to change.

Motivation:
High score – highest kills, or longest time alive
