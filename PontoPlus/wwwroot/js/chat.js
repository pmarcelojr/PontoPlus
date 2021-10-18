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