Proof-of-concept application showing the ability to run any applications and execute express code by modifying the Discord files.

The server and payload applications are designed as a proof-of-concept and are not made for active exploitation. I am not responsible for any misuse of this application.



I haven't seen anyone modify these specific files to do anything so far, which is why I created this application. It allows for an external web socket to execute applications on the clients' computers without the user being able to tell.

This works by modifying the app-#.#.####\\modules\\discord\_desktop\_core-1\\discord\_desktop\_core\\index.js file to connect to a web socket. The script gets executed by Discord when loading the client, and allows for any code to be ran, such as external programs, scripts, etc. completely undetected due to it being in the Discord client.



You can exploit this to do more than just run a web socket in the background, but I'm not filthy so I won't make that stuff. Enjoy and goodbye! ^^

