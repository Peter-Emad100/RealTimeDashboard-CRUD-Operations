
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/DashboardHub").build();
document.getElementById("sendButton").disabled = true;
document.getElementById("clearButton").disabled = true;
function sendDashboardAction(action, payload) {
    const messageJson = JSON.stringify({ action, payload });
    connection.invoke("DashboardAction", messageJson).catch(err => console.error(err.toString()));
}
function clearInputFields() {
    document.getElementById("nameInput").value = "";
    document.getElementById("priceInput").value = "";
    document.getElementById("quantityInput").value = "";
}

connection.on("ReceiveItems", function (items) {
    var list = document.getElementById("itemsList");
    list.innerHTML = "";

    items.forEach(function (item) {
        var li = document.createElement("li");
        li.textContent = `Name: ${item.name}, Price: ${item.price}, Quantity: ${item.quantity} `;

        var updateBtn = document.createElement("button");
        updateBtn.textContent = "Update";
        updateBtn.onclick = function () {
            var action = "update";
            const payload = { id: item.id };

            const nameValue = document.getElementById("nameInput").value;
            const priceValue = document.getElementById("priceInput").value;
            const quantityValue = document.getElementById("quantityInput").value;

            if (nameValue) payload.name = nameValue;
            if (priceValue) payload.price = parseFloat(priceValue);
            if (quantityValue) payload.quantity = parseInt(quantityValue);
            sendDashboardAction(action, payload);
            clearInputFields();
        };

        var deleteBtn = document.createElement("button");
        deleteBtn.textContent = "Delete";
        deleteBtn.onclick = function () {
            var action = "delete";
            var payload = {
                id: item.id
            };
            sendDashboardAction(action, payload);
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
    var action = "insert";
    var payload = {
        name: document.getElementById("nameInput").value,
        price: parseFloat(document.getElementById("priceInput").value),
        quantity: parseInt(document.getElementById("quantityInput").value)
    };
    if (payload.name && payload.price && payload.quantity) {
        sendDashboardAction(action, payload);
        document.getElementById("errorMessage").textContent = "";
    }
    else {
        document.getElementById("errorMessage").textContent = "Please fill in Name, Price, and Quantity before inserting an item.";
    }
    clearInputFields();
    event.preventDefault();
});

document.getElementById("clearButton").addEventListener("click", function (event) {
    var action = "clear";
    var payload = {};
    sendDashboardAction(action, payload);
    event.preventDefault();
});
