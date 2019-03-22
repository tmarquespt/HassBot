# Discord Bot CPHA 

Currently the bot is running in two different channels 
1. https://discord.gg/c5DvZ4e (English Home-Assistant Discord Channel)
2. https://discord.gg/ggU7UBz (Swedish Smart Home Discord Channel)

This is the HassBot (A Discord Bot) I wrote for Home Assistant's Discord Channel. The Bot basically has a bunch of commands - like help, about...etc. It also has custom commands, where the moderators can create commands on the fly and run them as needed. 

The command prefixes are `~` and `.`. That means, the commands can be executed either by `~` or `.`. The following are the list of commands that it supports + custom/default command - which is anything!

```
~about          - Shows information about this bot.
~help           - Displays this message. Usage: ~help
~8ball          - Predicts an answer to a given question. Usage: ~8ball <question> <@optional user1> <@optional user2>...etc
~list           - Shows existing custom command list.
~command        - Create custom commands using: ~command <command name> <command description>
~command        - Run Custom Command. Usage: ~skalavala <@optional user1> <@optional user2>...etc
~lookup         - Provides links to the documentation from sitemap. Usage: ~lookup <search> <@optional user1> <@optional user2>...etc
~deepsearch     - Searches hard, sends you a direct message. Use with caution!
~format         - Shows how to format code. Usage: ~format <@optional user1> <@optional user2>...etc
~share          - Shows how to share code that is more than 10 -15 lines. Usage: ~share <@optional user1> <@optional user2>...etc
~lmgtfy         - Googles content for you. Usage: ~lmgtfy <@optional user1> <@optional user2> <search String>
~ping           - Reply with pong. Use this to check if the bot is alive or not. Usage: ~ping
~pong           - Reply with ping. Use this to check if the bot is alive or not. Usage: ~pong
~update         - Refreshes and updates the lookup/sitemap data. Usage: ~update
~yaml?          - Validates the given YAML code. Usage: ~yaml <yaml code> <@optional user1> <@optional user2>...etc
~welcome        - Shows welcome information. Usage: ~welcome <@optional user1> <@optional user2>...etc
~json2yaml      - Converts JSON code to YAML. Usage `json2yaml <json code>`
~yaml2json      - Converts YAML code to JSON. Usage: `~yaml2json <yaml code>`
~c2f            - Converts a given Celsius value to Fahrenheit
~f2c            - Converts a given Fahrenheit value to Celsius
~hex2dec        - Converts a given hex value to decimal
~dec2hex        - Converts a given decimal value to hex
~bin2dec        - Converts a given binary value to decimal
~dec2bin        - Converts a given decimal value to binary
~base64_encode  - Encodes a given string to base64 format
~base64_decode  - Decodes a given base64 encoded string

Tip: If you put the yaml/json code in the correct format [```yaml <code> ```], or [```json <code> ```], Hassbot will automatically validate the code, and responds using emojis :thumbsup:
```

## Running the Bot as a Windows Service
The program is written in C# and uses Discord.Net warpper for Discord API. THe Bot runs as a Windows Service instead of Command Line Applications you see everywhere. The package consists of one solution, and a Console Application to test the code/bot, and a Windows Service that is deployed to the server to run. There are a bunch of common libraries that are shared between Console App and Windows Service, and you will find most of the code there. The Console App and the Windows Service are basically dummy clients that utilize the common components.


## Default Command is "Lookup"

Apart from the commands listed above, one can also simply search by providing search string as the command. For ex: Even if there is no "pre-defined" command, called "`xyz`", you can call still run the command as `~xyz`. This will check for string `xyz` in the sitemap and if there are any matching entries, it will give you the links to those as a response. If not, an emoji reaction will be added to the original request indicating that there are no entries found with the search string `xyz`. In case if the command already exists, that takes precedence and automatically executes that command. 

## Mentioning Users

Almost all commands allow you to mention users. For ex: If you would like to refer to the output of a command to a user, you can simply pass user name as parameter.

```
~lookup docs @Tinkerer @Ludeeus
```

The above command looks up `docs` in the sitemap, and mentions that to both @Tinkerer and @Ludeeus in the response. You can also say,

```
~docs everyone should read.... especially @Tinkerer
```
This shares the docs url and mentions @Tinkerer


## The features include:

### Welcoming new users
Every time a new user joins the channel, it sends out a public announcement, welcoming the user, and also sends a personal/direct message explaining the rules of the channel.

### Code limit warnings
There is a limit of 10-15 lines of code when posting to prevent code walls. The Bot checks for the number of lines, and issues a citation when violated.

### Automatic YAML code verification
People who come to the Home Assistant Discord channel tend to post their configuration and automation seeking for help. There is an automatic YAML verification in place, where everytime someone posts code, it automatically verifies the code, and responses in the form of emojis whether the code passed the test or it failed the test. Sort of like `yamllint`, except it is realtime.

For the automatic code verification to work, the code must use `~share` format. The share format is:

```
```yaml
code here```
```

### Lookup / Deepsearch in the sitemap document
When the lookup command is issued with a parameter, it searches in the Home Assistant's sitemap url (https://home-assistant.io/sitemap.xml) and points to the right articles and links.

### 8Ball Predictions
A fun command that randomly gives predictions to the questions. The answers are rarely and barely accurate.

### Ping - health check
Used to check the pulse of the Bot. When the `~ping` command is issued, the bot responses `Pong!`.

### Welcome Command
When the command `~welcome` is issued, it reminds the user to follow welcome rules.

...more.

A big shout out to [@Tinkerer](https://github.com/DubhAd/Home-AssistantConfig/) and [@Ludeeus](https://github.com/ludeeus) for the requirements and testing :smile:
