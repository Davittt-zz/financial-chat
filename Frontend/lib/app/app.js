
function appendLine(message, name) {

    let nameElement = document.createElement('strong');
    let msgElement = document.createElement('em');

    nameElement.innerText = `User:` + name + `: ` + JSON.stringify(message.text);
    msgElement.innerText = ` SendDate:` + JSON.stringify(message.sendDate);

    let li = document.createElement('li');
    li.appendChild(nameElement);
    li.appendChild(msgElement);

    $('#messages').append(li);
};