"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user.bold() + " says: " + msg;
    var li = document.createElement("li");
    li.className = "list-group-item";

    var dateChat = new Date();
    var timeChat = document.createElement("span");
    timeChat.className = "mr-5";
    timeChat.innerText = dateChat.getHours() + ":" + dateChat.getMinutes() + ":" + dateChat.getSeconds();

    li.innerHTML = timeChat.outerHTML + encodedMsg;

    var list = document.getElementById("messagesList");
    list.insertBefore(li, list.childNodes[0]);

    if (list.childElementCount > 50) {
        list.removeChild(list.lastChild);
    }
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("messageInput")
    .addEventListener("keyup", function (event) {
        if (event.keyCode === 13) {
            event.preventDefault();
            document.getElementById("sendButton").click();
        }
    });

document.getElementById("sendButton")
    .addEventListener("click", function (event) {
        var user = document.getElementById("userInput").innerText;
        var message = document.getElementById("messageInput").value;
        connection
            .invoke("SendMessage", user, message)
            .then(u => requestBot(message))
            .catch(function (err) {
                return console.error(err.toString());
            });

        document.getElementById("messageInput").value = "";
        event.preventDefault();
    });

var requestBot = function (message) {
    var url = "https://localhost:44371/api/messages";
    var data = {
        id: uuidv4(),
        channelId: "chat-bot",
        locale: "en-US",
        text: message,
        type: "message"
    };

    fetch(url, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: {
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json'
        },
        mode: 'cors'
    }).then(res => res.json())
        .then(response => console.log('Success', response))
        .catch(error => console.error(error));

}

function uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}