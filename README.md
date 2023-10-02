# pharaoh-new-era-scoreboard
A mod to the game Pharaoh a New Era, that logs the stats of your game to compare with friends like a scoreboard.

## current goal
- save the game during the victory screen with a meaningful name (e.g. "Victory-familyname-mapname-20231002")
- write a file on the pc with a name similar to the savegame name, that contains useful numbers from the game such as how long it took to complete, population etc.
- score file in a common format like json, xml etc
- generate a full image of the completed map

## future goal
- website that contains the stats of your recent games and how you compare against your friends
- on victory, auto upload the stats file to the website

## setup pharaoh for modding
Go to https://github.com/BepInEx/BepInEx/releases and drag and drop the contents of the download into your Pharaoh folder.

The modloader should load automatically however you normally start the game.

%gamefolder%\BepInEx\plugins\ is the folder where you place your mods, such as this one.


## build this mod from source
- download this github repo
- copy all the files from %gamefolder%\Pharaoh_Data\Managed to %repo install location%\Pharaoh_Data\Managed
I do not distribute these, they change with each release of the game.
- now build the project and copy/paste the files in the bin folder to the plugins folder.