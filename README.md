# SnakesAndLadders

To create this game and make the implementation independent of the platform, I have created a class library that contains the game.
For the board configuration I have created a json file with the number of rows and columns and the list of decorations (snakes and ladders). I use this information to create an array with the decorations.

To instantiate the Game class, it is necessary to send the implementation of 3 interfaces, one is for the action of the next turn (in the console application it is implemented with the space key), and the other is to draw the state of the game. The third one does not need to be implemented since an implementation is included in the class library, but we use the interface to be able to mock the tests.
Dependency injection could have been used in the console application.

The console app simply implements the 2 interfaces above and calls the "Initialize" and "StartGame" functions
