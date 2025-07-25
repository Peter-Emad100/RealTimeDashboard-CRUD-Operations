
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/DashboardHub").build();
document.getElementById("sendButton").disabled = true;
document.getElementById("clearButton").disabled = true;

connection.on("ReceiveItems", function (items) {
    console.log("Received items:", items);
    var list = document.getElementById("itemsList");
    list.innerHTML = "";

    items.forEach(function (item) {
        var li = document.createElement("li");
        li.textContent = `Name: ${item.name}, Price: ${item.price}, Quantity: ${item.quantity} `;

        var updateBtn = document.createElement("button");
        updateBtn.textContent = "Update";
        updateBtn.onclick = function () {
                var action = "update";
            var payload = {
                    id: item.id,
                    name: document.getElementById("nameInput").value,
                    price: parseFloat(document.getElementById("priceInput").value),
                    quantity: parseInt(document.getElementById("quantityInput").value)
                };

                var messageJson = JSON.stringify({
                    action: action,
                    payload: payload
                });
                console.log("Sending message:", messageJson);

                connection.invoke("DashboardAction", messageJson).catch(function (err) {
                    return console.error(err.toString());
                });
        };

        var deleteBtn = document.createElement("button");
        deleteBtn.textContent = "Delete";
        deleteBtn.onclick = function () {
            var action = "delete";
            var payload = {
                id: item.id
            };

            var messageJson = JSON.stringify({
                action: action,
                payload: payload
            });
            console.log("Sending message:", messageJson);

            connection.invoke("DashboardAction", messageJson).catch(function (err) {
                return console.error(err.toString());
            });
        };

        li.appendChild(updateBtn);
        li.appendChild(deleteBtn);
        list.appendChild(li);
    });
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    document.getElementById("clearButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    console.log("Insert button clicked"); 
    var action = "insert";
    var payload = {
        name: document.getElementById("nameInput").value,
        price: parseFloat(document.getElementById("priceInput").value),
        quantity: parseInt(document.getElementById("quantityInput").value)
    };

    var messageJson = JSON.stringify({
        action: action,
        payload: payload
    });
    console.log("Sending message:", messageJson);

    connection.invoke("DashboardAction", messageJson).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("clearButton").addEventListener("click", function (event) {
    console.log("clear button clicked");
    var action = "clear";
    var payload = {};
    var messageJson = JSON.stringify({
        action: action,
        payload: payload
    });
    console.log("Sending message:", messageJson);

    connection.invoke("DashboardAction", messageJson).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
