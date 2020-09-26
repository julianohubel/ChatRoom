// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
class Message {
    constructor(username, text) {
        this.userName = username;
        this.text = text;
        this.date = new Date();
    }
}


const username = userName;
const textInput = document.getElementById('messageText');
//const dateInput = document.getElementById('date');
const chat = document.getElementById('chat');
const messagesQueue = [];

document.getElementById('submitButton').addEventListener('click', () => {
    var currentdate = new Date();    
});

function clearInputField() {
    messagesQueue.push(textInput.value);
    textInput.value = "";
}

function sendMessage() {
    let text = messagesQueue.shift() || "";
    if (text.trim() === "") return;
    
    let message = new Message(username, text);
    sendMessageToHub(message);
}

function addMessageToChat(message) {
    let isCurrentUserMessage = message.userName === username;
    

    let row = document.createElement('div');
    row.className = "row"

    let offset = document.createElement('div');
    offset.className = isCurrentUserMessage ? "col-md-6 offset-md-6" : "";

    let container = document.createElement('div');
    container.className = isCurrentUserMessage ? "container darker bg-primary" : "container bg-light";

    let sender = document.createElement('p');
    sender.className = isCurrentUserMessage ? "sender text-right text-white" : "sender text-left";
    sender.innerHTML = message.userName;
    let text = document.createElement('p');
    text.className = isCurrentUserMessage ? "text-right text-white" : "text-left";
    text.innerHTML = message.text;

    let date = document.createElement('span');
    date.className = isCurrentUserMessage ? "time-right text-light" : "time-left";
    var currentdate = new Date();
    date.innerHTML =
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: false })

    container.appendChild(sender);
    container.appendChild(text);
    container.appendChild(date);
    offset.appendChild(container);
    row.appendChild(offset);
    chat.appendChild(row);
}


var connection = new signalR.HubConnectionBuilder().withUrl('Home/Index').build();

connection.on("Receive", addMessageToChat);

connection.start()
    .catch(error => {
        console.error(error.message);
    })

function sendMessageToHub(message) {
    connection.invoke("sendMessage", message);

    if (message.text.includes('/stock'))
        getStock(message.text.replace("/stock=", ""));

}

function getStock(stock_code) {
    $.ajax({
        url: "stock/get/" + stock_code,
        method: "GET",
        data: stock_code,
        success: function () {
            
        }
    });
}

function GetMessagesFromQueue() {
    setTimeout(function () {        
        $.ajax({
            url: "rabbitMQ/GetMessages/",
            method: "GET",            
            success: function (data) {
                console.log(data);
                if (data != null && data.success) {
                    let message = new Message("Bot", data.symbol + " quote is $" + data.close + " per share");
                    sendMessageToHub(message);
                }
            }
        });
        GetMessagesFromQueue();
    }, 1000);
}

GetMessagesFromQueue();