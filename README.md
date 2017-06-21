# Enter The House
Summer 2016 UCSD Research Experiment Game for Frontiers Science Journal

This experiment was to test if a subject's creativity could be affected by a game with two different scenarios. The game's premise is to find a way inside the building while the player does not have the keys. The first scenario primes a narrow form of attention and prompts the player to undergo a search for the lost keys in order to unlock the door and enter the building. This narrow form of attention where the player is focused on finding the keys is also expected to limit the subject's creativity for a short period of time. In contrast, the second scenario primes a broad form of attention and prompts the player to find an alternate form of entry (breaking a window, climbing down the chimney, etc.). This broad form of attention where the player is analyzing the environment for multiple paths is expected to boost a subject's creativity for a short period of time. 
  
Speculation of whether or not the narrow scenario "limits" or "less effectively boosts" will be looked at after data has been collected before, after, and during the game experiment.

**TO RUN:** In the "Data Files" directory of this repository, there are two zip files that correspond to the two scenarios. Note that these are made for 64-bit machines. Unzip the files wherever you'd like and run the executable inside. This should work as long as the data folder is in the same directory as the executable if you need to move them around.

# SNAP: Simulation and Neuroscience Application Platform
Link to repository: https://github.com/sccn/SNAP

I created a simple test module using SNAP that gives a visual for the RAT and AUT (RAUT) tests that are used in this experiment. Here are two links for information about the Remote Associates Test (RAT) and the Alternative Uses Test (AUT) respectively.
* RAT: http://www.creativehuddle.co.uk/the-remote-associates-test
* AUT: http://www.creativehuddle.co.uk/the-alternative-uses-test
 
# LSL: Lab Streaming Layer
Link to repository: https://github.com/sccn/labstreaminglayer

Using the LSL library, we were able to achieve real-time sensor data streaming between Unity 3D and the Emotiv EEG headset that participants wore during the experiment.

# Scenario Scripts
Both: PlayerController, CameraController_Mouse, pickMeUp, doorUnlock, trackPlayer

Broad: entryPoints, broadPickup, iThrowYou, impactSounds, axeSwing, liftController, liftMeUp, trapdoorBlocked, windowBreak, LadderSnap

Narrow: iPickYouUp, narrowSounds

# Unity Assets Used
* Abandoned Buildings by Aleksey Kozhemyakin 
* Action RPG Melee Axe by Vibrant Core
* Alchemy Station by David Stenfors
* Construction Site Pack by Alex McDonnell
* Damaged Old Car by VIS Games
* Decrepit Dungeon LITE by Prodigious Creations
* Dumpster by Rakshi Games
* Free Steel Ladder Pack by Surpent
* Handpainted Keys by RoboCG 
* Industrial Objects Pack by Arkham Interactive
* Junk by UCSD
* Medieval Gold by Mister Necturus
* PBS Materials Variety Pack by Integrity Software & Games 
* Post-Apocalyptic Truck by Valga Games
* Props for the Classroom by VR
* Ruined Car by 000734
* Small Town America - Streets by MultiFlagStudios
* Storage Building by Gooseman's Graphics
