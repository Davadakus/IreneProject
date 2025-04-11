# Daily Report
## 08/03/2025
Switched from adding eb.AddForce() to just change the rb.velocity to make jump consistent and fixed.

## 09/03/2025 - 10/3/2025 (Late Night)
Added proper collision detection for when touching the ground and allowing only 1 jump if player did their first jump midair. Initially, I just checked for 2Dcollision from the player on to a ground object, however proves issues when simply touching side of wall, it reset the jump anyway.

Possible solution for it exists but not tried since I wanted to try and do ray casting. I drew a line from the player to the ground and checked for collision that way with checking whether its a ground *Layer*. Issue here is its a line and so on a eldge it wont register. Used a boxcollider instead which works perfectly. Works the same way just a box. Took a whole day to figure it out though.

## 10/03/2025
I've made gliding work with this [video](https://www.youtube.com/watch?v=s8lQKlp-EJo&ab_channel=PitiIT) as help. Additionally, I fixed the double logging that was happening which happened because there were 2 "PlyaerMovement" components on the player object causing it to log to the console twice. Also optimized a bit of code like moving JumpStop to Jump and having context.canceled handled there instead of a seperate method.

Also, Might need to experiment with gliding trigger; currently set to 0.1 when 'w' is held down but might need tweaks. Also have made the LayerMask to auto detect the ground layer on awake(). 

As for coding, I think this is a sufficient baseline to start working on other things that interact with the player. I think it would be great to consider the enemies and difuring out how sprites work during animation.

## 09/04/2025
Took a bit of a break but I have made the Irene artwork that I think I will use for her talking on screen. Next I want to try experiment doing demo sprite work to get a hang of what I should do when drawing Irene actually attacking. Probably just do some dummy sprite with from the asset store to see how the process is.

## 11/04/2025
So I've successfully implemented the animations, at least for running, jumping, falling, and idle. I sort of know how it works now and I want to create the Irene sprites now. However, I have some interaction issues with checking when the player is grounded. The player is getting checked every FixedUpdate but this in turn causes the grounded check to keep triggering since technically the box collider under the player is still touching when they lift off the ground. I actually forced the value 2 when the player jumps a second time to overlook this issue but this needs to be fixed. Maybe adding a delay to when it gets checked or making the check tighter? Will have to figure out but good progress. Also, the sprite is technically not centred so maybe add an offset for the box collider to be more accurate later. This [video](https://www.youtube.com/watch?v=Sg_w8hIbp4Y&t=18s&ab_channel=GameCodeLibrary) helped me mostly figure out how it works. 

I made a the sprite flip in `FixedUpdate()` when the rb is detected to have positive or negative velocity too.  