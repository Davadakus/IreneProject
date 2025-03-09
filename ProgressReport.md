# Daily Report
## 08/03/2025
Switched from adding eb.AddForce() to just change the rb.velocity to make jump consistent and fixed.

## 09/03/2025 - 10/3/2025
Added proper collision detection for when touching the ground and allowing only 1 jump if player did their first jump midair. Initially, I just checked for 2Dcollision from the player on to a ground object, however proves issues when simply touching side of wall, it reset the jump anyway.

Possible solution for it exists but not tried since I wanted to try and do ray casting. I drew a line from the player to the ground and checked for collision that way with checking whether its a ground *Layer*. Issue here is its a line and so on a eldge it wont register. Used a boxcollider instead which works perfectly. Works the same way just a box. Took a whole day to figure it out though.