var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

var userMessage = document.getElementById('chat-input-text');
var userId = document.getElementById('chat-receiver-container').innerText;
var receiverId;

connection.start()
    .then(function () {
        document.getElementById('chat-send-button').addEventListener('click', function (event) {
            if (userMessage.value != "") {
                var dateFormatted = formatDateNow();

                var message = userMessage.value;
                userMessage.value = "";

                var messagesList = document.getElementById("chat-messages");
                messagesList.innerHTML += `
                    <div class="chat-my-message">
                        <p>${message}</p>
                        <br />
                        <span class="chat-my-message-time">${dateFormatted}</span>
                    </div>`;

                messagesList.scrollTop = messagesList.scrollHeight;

                connection.invoke('SendMessage', userId, receiverId, message);
                event.preventDefault();
            }
        });
    })
    .catch(error => {
        console.error(error.message);
    });

connection.on("ReceiveMessage", function (message) {
    if (message.receiverId == userId) {
        var messageFormatted = message.message.replace(/&/g, "&amp;")
            .replace(/</g, "&lt;")
            .replace(/>/g, "&gt;");

        var dateFormatted = message.sendAt.substring(11, 16);
        var messagesList = document.getElementById("chat-messages");

        messagesList.innerHTML += `
                    <div class="chat-sender-message">
                        <p>${messageFormatted}</p>
                        <br />
                        <span class="chat-sender-message-time">${dateFormatted}</span>
                    </div>`;

        messagesList.scrollTop = messagesList.scrollHeight;
    }
});

var chatHeader = document.getElementById("chat-header");
chatHeader.onclick = toggleChat;

function toggleChat() {
    var chat = document.getElementById("chat");

    if (chat.className.includes("chat-open"))
        chat.className = "chat-box";
    else
        chat.className = "chat-box chat-open";

    scrollChatToBottom();
}

function scrollChatToBottom() {
    var chat = document.getElementById("chat-messages");
    chat.scrollTop = chat.scrollHeight;
}

window.onload = addStartConversationToContacts;

function addStartConversationToContacts() {
    var contacts = document.getElementsByClassName("chat-contact-list-item");

    for (var i = 0; i < contacts.length; i++) {
        var userId = formatUserId(contacts[i].id);
        var username = formatUsername(contacts[i].innerText);

        addStartConversationEventToContact(contacts[i], userId, username);
    }
}

function addStartConversationEventToContact(contact, userId, username) {
    contact.addEventListener('click', function () {
        startConversation(userId, username);
    });
}

function startConversation(userId, username) {
    changeHeaderValue(username);
    setReceiverId(userId);
    clearMessages();
    getMessages();

    var contactList = document.getElementById("chat-contacts");
    contactList.style = "display: none";

    var chatMessage = document.getElementById("chat-messages-box");
    chatMessage.style = "display: block";

    scrollChatToBottom();
}

var backButton = document.getElementById("chat-back-button");
backButton.onclick = backConversation;

function backConversation() {
    changeHeaderValue("Suporte");
    var contactList = document.getElementById("chat-contacts");
    contactList.style = "display: block";

    var chatMessage = document.getElementById("chat-messages-box");
    chatMessage.style = "display: none";
}

function formatUsername(username) {
    var usernameFormatted = username
        .replace("\n", "")
        .trimStart()
        .trimEnd();

    return usernameFormatted;
}

function formatUserId(userId) {
    var userIdFormatted = userId
        .replace("user-", "");

    return userIdFormatted;
}

function changeHeaderValue(value) {
    var header = document.getElementById("chat-header");
    header.innerHTML = '<h6><strong>' + value + '</strong></h6>';
}

function setReceiverId(id) {
    receiverId = id;
}

function clearMessages() {
    var messages = document.getElementById("chat-messages");
    messages.innerHTML = "";
}

function formatDateNow() {
    var hours = new Date().getHours();
    var minutes = new Date().getMinutes();

    if (hours < 10)
        hours = '0' + hours;

    if (minutes < 10)
        minutes = '0' + minutes;

    return hours + ':' + minutes;
}

function formatDate(date) {
    return date.substring(11, 16);
}

function getMessages() {
    var url = `/api/messages/${receiverId}/${userId}`;

    $.ajax({
        method: 'GET',
        url: url,
        data: null,
        complete: function (data) {
            var messageOBJ = JSON.parse(data.responseText);

            clearMessages();

            var messagesList = document.getElementById("chat-messages");
            for (var i = 0; i < messageOBJ.length; i++) {
                var dateFormatted = formatDate(messageOBJ[i].sendAt);

                if (messageOBJ[i].senderId != userId) {

                    messagesList.innerHTML += `
                            <div class="chat-sender-message">
                                <p>${messageOBJ[i].message}</p>
                                <br />
                                <span class="chat-sender-message-time">${dateFormatted}</span>
                            </div>`;
                }
                else {
                    messagesList.innerHTML += `
                            <div class="chat-my-message">
                                <p>${messageOBJ[i].message}</p>
                                <br />
                                <span class="chat-my-message-time">${dateFormatted}</span>
                            </div>`;
                }

                messagesList.scrollTop = messagesList.scrollHeight;
            }
        }
    });
}