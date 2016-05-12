# FlowPath
Visualizing runtime ﬂowpath in the context of interactive circuits formed of ﬁnite primitive elements. Creating an interactive VR sandbox for the creation of digital logic circuits and graphical scripting.

The "Flowpath" directory is a unity project folder. It can be opened in unity. Various test scenes show development in stages. The most current is the "VisualConnections" which has also been duplicated into the "main" scene.

# Source Overview:
Assets/Circuit/Scripts/:
  - Contains various classes that comprise the backend logic for the circuit solver. 
  - "circuit.cs": a representation of a circuit for the backend. It uses a directed graph maintained by adjacency lists. Nodes are represented generically with a custom Node class, with subclasses for various types of nodes. The circuit is solved by sorting the graph with topological sort (see "circuit.cs") and then moving through the sorted list evaluating nodes based on their inputs. This way all nodes are evaluated after their parents. 
  - "CircuitManager.cs": A static class that manages the backend circuit. It uses events to transmits information about the circuit to the frontend and provides methods to modify the the circuit. This way the frontend and backend can stay in sync.
  - "NodeComponent.cs": A component that can be attached to a gameobject. Provides a means of representing a node on the frontend as a gameobject. The component holds a reference to its associated backend node so that its active state can be querried without having to access the circuit, or incase the node is not part of the circuit.
  -"ReferenceNodeRemover.cs": Removes any specified frontend nodes from the circuit. This is a workaround for the reference nodes that are present in the scene at start time. These nodes are used as templates for the frontend nodes spawned at runtime. They need to be removed from the circuit as they are just reference templates.
  
Assets/InteractiveObjects/Spawning+Moving/Scripts/:
  - Contains scripts that define the relevant scripts object interactions that involve spawning or moving a gameobject. 
  - "ItemInteraction.cs": A component. The base class for item interactions containing shared behavior relevant to various types of interactive objects. Detects when a user attempts to pinch-grab interact with the object. Provides overridable methods for beginning and ending the interaction such that derived classes can define the behavior accordingly. 
  - "ItemPickup.cs": An ItemInteraction component that allows a gameobject to picked up using pinch grab.
  - "ItemSpawner.cs": An ItemInteraction component that allows for new game objects to be spawned using pinch grab. (used for the spawn volumes in the parts bins).
  - "InteractionEventManager.cs": A manager for events related to object interaction. Used for informing interactive objects about existing interactions to make interaction more robust. For example, interactive objects not involved in the current interaction can shut themselves off for the duration of the interaction which is useful in avoiding accidental pickups etc.

Assets/InteractiveObjects/Mode Selection/Scripts/:
  - Contains scripts relevant to mode management. Permits the use of various modes for different interactions etc. Everything is event driven and extendable with any number of new custom modes.
  - "ModeManager.cs": Manages events for the mode selection and provides functionality to change the mode. 
  - "ModeDisplayUpdate.cs" and "InstructionsDisplayUpdate.cs": Example components that subscribe to events from the modemanager to dynamically change gameobjects according to mode changes.
  - "ButtonInteraction.cs": Example component for creating an interactive button in the gameworld to change modes.
 
Assets/InteractiveObjects/Connections/Scripts/:
  - Contains scripts relevant to the creation and visualization of connections between frontend nodes.
  - "ProspectiveConnectionManager.cs": Manager for all things related to the building of a new connection. Keeps track of the building progress from the front end and notifies subscribers of any changes. Also communicates completed connections to the backend via another intermediate manager. Also communicates completed connections to yet another manager which handles the visualization of a completed connection.
  - "ConnectionPort.cs": A component that serves as a port for connections, specifiable as input or output.
  - "RopeManager.cs": Manages the visualization of all completed connections. Maintains a list of existing connections and updates the visualization for moving entities. (Needs a re-write for event driven updates on node movement). When a new rope is created at runtime, a special hierarchy of gameobjects is generated and custom tuberenderer and rope_tube components are instantiated to render the physics driven rope. 
  - "RopeClass.cs": A data class with necessary variables for representing a visual connection. Used by the RopeManager. 
  - "ControlSwitch.cs": A component that acts as a switch on a front end source node to toggle its active state
  - "ColorPulser.cs": Calculates a pulsing color. Used by the RopeManager to update the color of active connections. 
  - "ConnectionCreator.cs": Deprecated... left for reference.
