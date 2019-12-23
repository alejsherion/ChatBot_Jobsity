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
            .catch(function (err) {
                return console.error(err.toString());
            });

        document.getElementById("messageInput").value = "";
        event.preventDefault();
    });
