$(window).on("load", function () {
	let btn = document.getElementById('messengerBtn');
	btn.addEventListener('click', function() {
		$('#messengerList').empty();
		$('#messengerWindow').toggle();
		$('#messengerBtn').toggle();
		$('#messengerCloseBtn').toggle();
		$.ajax({
			url: '/Friends',
			dataType: 'html',
			type: 'GET',
			success: function (response) {
				if (response.indexOf("Error:") == 1) {
					console.log(response);
				}
				else {
					$('#messengerList').append(response);
				}
			}
		});
	});
	let closeBtn = document.getElementById('messengerCloseBtn');
	closeBtn.addEventListener('click', function() {
		$('#messengerWindow').toggle();
		$('#messengerBtn').toggle();
		$('#messengerCloseBtn').toggle();
	})
});

// let userId = $(document.currentScript).attr('data-user_id');

var connection = new signalR.HubConnectionBuilder().withUrl("/messenger").build();
document.getElementById("messengerSendBtn").disabled = true;

connection.on("RecieveMessage", function (user, message) {
	var li = document.createElement("li");
	document.getElementById("messagesList").appendChild(li);
	li.textContent = `${user} says ${message}`;
});
connection.start().then(function () {
	document.getElementById("messengerSendBtn").disabled = false;
}).catch(function (err) {
	return console.error(err.toString());
});

document.getElementById("messengerSendBtn").addEventListener("click", function (event) {
	var otheruser = "abc";
	var message = document.getElementById("messengerInput").value;
	connection.invoke("SendMessage", userId, message).catch(function (err) {
		return console.error(err.toString());
	});
	event.preventDefault();
});