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
