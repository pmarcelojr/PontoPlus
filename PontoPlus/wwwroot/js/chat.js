
var chatHeader = document.getElementById("chat-header");
chatHeader.onclick = toggleChat;

function toggleChat() {
    var chat = document.getElementById("chat");

    if (chat.className.includes("chat-closed"))
        chat.className = "chat-box";
    else
        chat.className = "chat-box chat-closed";
}