# Sokoban dojo

Sokoban is a classical puzzle game in which a mover has to push objects to specific places. The mechanics of the game are quite simple, but we can have fun playing it, and also implementing it!

We can tackle several interesting aspects

## Level parsing

Sokoban levels can be represented in plain text files, with the following characters:

| Character | Meaning         |
|-----------|-----------------|
| (space)   | ground          |
| .         | store           |
| $         | object          |
| *         | object on store |
| @         | mover           |
| +         | mover on store  |
| #         | wall            |

You should write a function that parses a string into a representation of a level.

As an exemple, the input string could be the following:
>        ####
    ####  ##
    #   $  #
    #  *** #
    #  . . ##
    ## * *  #
     ##***  #
      # $ ###
      # @ #
      #####

## Level display

Once we have a parsed level, it makes sense to be able to display it, right?

You should write a function that turns a level back to its string representation. You could also to build a better looking visualization, but maybe you should get the game working first...

## Actualy playing

Given a game, we'd like to be able to:

* determine whether the mover has won
* compute the next state of the game, once the mover has moved in a given direction.
* know wich directions are valid as next moves (optional)
