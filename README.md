DNN Simple Messaging
==================
Simple Messages Module for DotNetNuke

Compatible with DNN 6.2.6+

This is a simple module that uses DNN's AddModuleMessage to send messages to other users. 



There are some features that have not been fully developed yet which I may come back to later, like sending to Roles.
You can also customize the module somewhat in the SQL stored procs.




View    
The View will iterate through a list of messages for the currently logged in user and display them individually.
If the user has any messages to be show, each message will be displayed in the appropriate DIV box.

It will only display messages based on Effective Date and Expiration Date
It also takes the Max # Views module setting into consideration
Get Messages:  pdx_CITMessages_GetMessages  


 

Edit  
The Edit module interface allows a user to send messages by defining the recipient, messagetext, type, and category. It also stores the number of times the message has been viewed by the recipient.
This is only available to users with EDIT permissions.  You can click 'Manage' -> 'Edit Module' and you will be presented with an interface that will allow you to Create, Update, or Delete messages.

If you choose a user, it will filter the Grid by that user's messages

To Create a message, you must choose a user first, then you can add the message.
 

User DropDownList:  pdx_CITMessages_GetUsers  
Create Message:  pdx_CITMessages_AddMessage  
Delete Message:  pdx_CITMessages_DeleteMessage  
Update Message:  pdx_CITMessages_UpdateMessage  
Populate GridView in EDIT mode:  pdx_CITMessages_GetMessages_ForEditor  
Categories DropDownList:  pdx_CITMessages_GetCategories  


 

Settings  
There are a few settings for the module like Max Number(default 4) of Messages to display and Max Message Characters(default 100), so the message doesn't get too long.
There are several settings that can be used, but these settings are on the entire Module.

Max Messages - Allows you to set a maximum # of messages to be displayed on the Home page.
Max Chars per Message - Allow you to set the maximum length of each message so the message doesn't get too long.
Max # Views - Set the number of times a user will view the message before it stops being displayed to the user.

 

 

Tables   
CIT_Messages
CIT_Messages_Categories
