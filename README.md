# Cave simulator
This project is a simulation of a cellular automaton, where cells interact with each other based on various rules and states. Different cell states are used to simulate elements and rules are used to simulate interaction between these elements. 

First a random cave is generated using the rules of cellular automation which serves as the environment and limiter of the simulation.

Then  different elements (cell states) using the simple user interface can be placed inside the cave that interect with each other with different rules. This is a simlification of the rules used to simulate elements:
* ![#f03c15](https://placehold.co/15x15/191919/191919.png) Cave Wall: Elements can't pass through it.
* ![#f03c15](https://placehold.co/15x15/783f04/783f04.png) Wood: Burns if there is a fire in the adjacent field. Floats on water.
* ![#f03c15](https://placehold.co/15x15/f8e2a1/f8e2a1.png) Sand: Can move diagonally. Displaces Water.
* ![#f03c15](https://placehold.co/15x15/f03c15/f03c15.png) Fire: Turns to dark smoke if it lands on Wood and turns to white smoke if it lands on Water or Sand.
* ![#f03c15](https://placehold.co/15x15/3d85c6/3d85c6.png) Water: Simulates liquid. Each cell has a certain amount of water that it can move into another cell of water. It can move upwards given enough volume.

![image](https://github.com/GrujicBard/CaveSimulator/assets/33715866/6de237ea-cf76-4dff-8e73-d58292a732be)
