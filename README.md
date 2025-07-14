# SpaceCarrier

#### A WIP Prototype of a Unity strategy game aiming for a 2d top-down map-based ww2-like naval carrier vs carrier combat.

An exercise in ECS-based architecture - granular behavior components, centralized systems, data-driven entities; coupled through Zenject. Eventually settled into a mix of ECS and MVC to create a custom "MVCS" (Model View Controller System) architecture:
- **Model**: Entity declarations and DTOs
- **View**: MonoBehaviours responsible for displaying game state to the player (denoted as `MB` or `Widget`)
- **Controller**: MonoBehaviours responsible for processing and responding to the player's input (denoted as `Controller`)
- **System**: Centralized systems processing the Entities (denoted as `System`)

Ultimately to be ported into the Burst complier with the Jobs system.
<br><br>
### Currently features:
- entity select-deselect (by click and by box select) with variable shift modifer
- entity display
- entity movement
- entity widgets
- camera movement
- unit move orders
