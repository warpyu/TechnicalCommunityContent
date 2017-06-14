# Demo 2.1: Intro to Bot #
This demo should take about 6 minutes
## Objectives ##
The goal of this demonstration is to give you a better idea of the Bot Framework running using different languages (C# & node.js).  It also will explain some of the more advanced techniques available the Microsoft Bot Framework.  

## Requirements ##
- An internet connection
- An account with [http://dev.botframework.com](http://dev.botframework.com "http://dev.botframework.com")

## Setup
2. Copy the Demo2.1.zip file provided in the Solutions folder that goes with this session.
3. Extract it under your documents folder and open any files indicated below from the extracted version. 
4. Open a Command Windows in the same folder as the "app.js" file
5. Type `npm init` to initialize the node.js package manager so that we can download needed bot framework components
6. Type `npm install --save botbuilder` press enter
7. Type `npm install --save restify` press enter
8. Have the app.js file open in Notepad.exe before you begin.
9. Have the Microsoft Bot Emulator already running.
10. Open [http://dev.botframework.com](http://dev.botframework.com "http://dev.botframework.com") and login

## Demo Steps ##
### Node.js ###
1.	Display the app.js file in Notepad

	> Note that the bot framework is not all that different in node.js than in C#.  You still have the same messages endpoint, you still can receive and respond to text.  You can even maintain context and used advanced formatting in your messaging.
	> 
2.	Type `node app.js` to start your app
3.	Open the Microsoft Bot Emulator
4.	Connect to "http://localhost:3978/api/messages"
5.	Type "Hello" press enter.
6.	When prompted for your name, enter your first name then press enter.
7.	Note that you get your name echoed back with a greeting.
8.	Return to our app.js loaded in notepad and do a walk-through of the flow of the bot conversation.

	> Talk about the two dialog levels
	> Showing the app.js file again you can note where the app prompts for your name and again where it displays the greeting.
	> 
	> Let's look at our code.  So the Bot Setup section shows where we are setting up the REST server that will host our bot.
	>
	> The next lines layout how we will connect to our bot.  In this case, just like in C# we won't be connecting it to the internet so no id or password is required.  We also set the bot endpoint of /api/messages just like with C#
	> 
	> The dialogs start with the first entry point and we immediately call into the /askName where we prompt for your name.  Once that is complete we return to our original entry point with the results and echo back a hello.