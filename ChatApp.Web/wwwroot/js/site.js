// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(function () {
    var btnSend = '#btn-send';
    var chatMessage = document.getElementById('chat-message');
    var chatContainer = document.getElementById('chat-container');
    var userNameVal = document.getElementById('user-name').innerText;
    var userSurnameVal = document.getElementById('user-surname').innerText;


    var connection = new signalR.HubConnectionBuilder()
        .withUrl("/chathub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.start().then(function () {
        console.log("connected");
        connection.invoke('UserSignedIn', {
            name: userNameVal,
            surname: userSurnameVal,
        });
    });
    connection.on("ChatMesssageRecived", (obj) => {
        var message = obj.message;
        var createdOn = obj.forttedCreatedOn;
        var name = obj.name;
        var surname = obj.surname;

        $(chatContainer).prepend('<li><span class="text-success">[' + createdOn + ']' + name + ' ' + surname + ':</span> '+ message + '</li>');
    });

    connection.on("UserSignedIn", (obj) => {
        var createdOn = obj.formatedCreatedOn;
        var name = obj.name;
        var surname = obj.surname;

        $(chatContainer).prepend('<li class="text-danger">[' + createdOn + '] Nowy użytkownik: ' + name + ' ' + surname + '</li>');
    });
    chatMessage.addEventListener('keyup', function (event) {
        if ($(chatMessage).val().length <= 0) {
            $(btnSend).addClass('disabled');
        }
        else {
            $(btnSend).removeClass('disabled');
        }
        if (event.keyCode === 13) {
            event.preventDefault();
            $(btnSend).click();
        }
    })

    $(btnSend).click(function () {
        var message = $(chatMessage).val();
        if ($(chatMessage).val().length > 0) {
            connection.invoke('SendChatMessage', {
                name: userNameVal,
                surname: userSurnameVal,
                message: message
            });
        }
        
        $(chatMessage).val('');
    })


})();