Todos

remove unwanted things from the project✅

--------------Basic project structure-----------------

GameManager (game state: menu, playing, paused, game over)✅
ARManager (AR session setup, raycast/spawn point handling)✅
InputManager (touch input abstraction)✅
UIManager (HUD updates)✅

**4) Prefabs + basic wiring**

- A `Player` prefab (camera + shooting origin)
- An `Enemy` prefab (simple placeholder cube + collider)
- A `HUD` prefab (crosshair, score, health)
- Basic references connected in the scene (Player ↔ Input, UI ↔ GameManager, AR ↔ Spawn point)

**5) Naming and conventions**

- Consistent names like `EnemyBasic`, `WeaponPistolSO`, `UI_HUD`
- One place for constants/tags/layers (example layers: `Enemy`, `ARPlane`, `Projectile`)
- Keep scripts grouped by feature: `AR/`, `Player/`, `Enemies/`, `UI/`, `Core/`


-----------------------shooting------------------------------------
--take mouse input or phone touch input--✅
--instantiate bullet prefab at spawn point--✅
--destroy bullet after 10 seconds--✅
--on bullet collision with enemy, destroy enemy and bullet--✅
--use object pooling for bullets--✅


--ui-
--add crosshair--✅
--Fire particle effect when bullet is fired
--also add particle effect hit location
--add reload feature✅


----------------------------enemies System----------------------
--spawn enemies at random intervals and locations in the AR space--✅
--enemies should shoot back at the player✅
--add health system for player and enemies--✅
--add score system for killing enemies--✅
--add different enemy types with varying behaviors and health--✅
----------------------------game states----------------------
--implement game states: menu, playing, paused, game over--
--add UI for each state (main menu, pause menu, game over screen)--
--handle transitions between states (start game, pause/resume, end game)--
--implement a simple main menu with a start button--
--implement a pause menu with resume and quit options--
--implement a game over screen that shows the final score and a restart button--
--keep track of player health and end the game when health reaches zero--
--implement a scoring system that increases when enemies are killed and displays the score on the HUD--
--implement a simple win condition (e.g., survive for a certain time or reach a score threshold)--
--implement a simple lose condition (e.g., player health reaches zero)--
--implement a simple restart mechanism that resets the game state and allows the player to play again without restarting the app--

-------gun Store----------------------
--implement a simple gun store where players can spend points to buy new weapons--
--add different weapon types with varying stats (e.g., damage, fire rate, reload time)--
--implement a simple UI for the gun store that shows available weapons and their stats--
--allow players to purchase weapons using points earned from killing enemies--

