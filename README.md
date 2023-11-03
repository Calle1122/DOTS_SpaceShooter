# DOTS_Asteroids
 
This project was made for the FG Computer Technology Course 2023 using Unity DOTS & ECS.

In this project i met the requirements by implementing a version of Asteroids using Unity Entities (ECS), Jobs System & Burst Compiler. When using ECS, I split all logic into smaller components which could be reused on any entity as fit. This allowed for better modularity where a bullet & an asteroid could use many of the same components to move, collide & be destroyed for example. This also meant for faster bug-fixing as running into an error often meant I only had to change a particular thing on a particular component instead of how the whole system works.

The main performance boots came from using ECS in combination with the Jobs System & the Burst Compiler. ECS works by implementing the games components in structs that then allocate on the stack compared to a MonoBehaviour implementation which utilizes heap memory a lot more frequently. Stack allocation is faster which meant my performance was greatly increased. Using Jobs & Burst Compiler I schedule the game logic to work in parallel on multiple threads for greater efficency.

During this project I tested two different collision implementations; Distance checking to determine when two entities collide & using triggers (Physics Shapes). Below is snapshots from the memory profiler showing managed object memory between the two implementations:
(A = Trigger Colliders, B = Distance Checking)

![image](https://github.com/Calle1122/DOTS_SpaceShooter/assets/60339644/d26fff51-00a1-428e-8581-c79e7b5a7e0a)

---------------------------------------------------------------

Memory comparison (Same committed memory 12.75 GB):

A
Trigger Colliders:
Managed memory = 6.92 GB: 4.81 GB Objects

B
Distance-check Colliders:
Managed memory = 1.19 GB: 28.3 MB Objects

---------------------------------------------------------------
Worth to note:
During the memory profiling the snapshot of the game using Trigger Colliders had around 500 fewer asteroids than the snapshot of the game using Distance-check Colliders. Still the Distance-check one seemed to perform better.

After this I chose to go with the distance checking since it clearly managed less memory. I also saw an FPS boost around 50-100 FPS with this solution compared to using Trigger Colliders (The game runs steadily at around 750 FPS compared to around 650-700 FPS).

Another improvement I later did was to remove entities that exited the screen (bullets). Altough it was barely noticable performance wise, it did clear up some usage of memory.

During development I occasionaly ran into some memory leaks. This was mostly due to my lacking understanding of how Jobs were handeled. But after reading up on the Unity Documentation i found that by manually scheduling jobs with certain dependencies and then making sure those dependencies were completed before doing further scheduling, these issues could be avoided. See images below for code examples:

![image](https://github.com/Calle1122/DOTS_SpaceShooter/assets/60339644/7202f5f4-39ff-4c51-8a56-6bcaae18abfc)
![image](https://github.com/Calle1122/DOTS_SpaceShooter/assets/60339644/ca217751-6f52-4122-973b-4715555eeb69)

---------------------------------------------------------------

Finally I want to say that this course has been very interesting. Learning the basics of Unity DOTS, ECS, Jobs & Burst Compiling lead to me having to think in a new way when developing. I will certainly continue to research the topic!
Thank you!
