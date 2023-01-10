$(window).on("load", function () {
	document.getElementById('messengerSendBtn').disabled = true;
	var selectedUserId = ""
	const connection = new signalR.HubConnectionBuilder().withUrl("/messenger").withAutomaticReconnect().build();

	connection.start().then(function () {
		document.getElementById("messengerSendBtn").disabled = false;
	}).catch(function (err) {
		return console.error(err.toString());
	});

	connection.on("RecieveMessage", (user, message) => {
		var li = document.createElement("li");
		li.textContent = message;
		document.getElementById('messages-messenger-friend-' + user).appendChild(li);
		console.log("message recieved");
	});
	
	connection.on("SendSuccess", () => {
		console.log("message sent");
	});

	document.getElementById("messengerSendBtn").addEventListener("click", function (event) {
		var message = document.getElementById("messengerInput").value;
		connection.invoke("SendMessage", selectedUserId, message).catch(function (err) {
			return console.error(err.toString());
		});
		event.preventDefault();
	});

	let btn = document.getElementById('messengerBtn');
	btn.addEventListener('click', function() {
		$('#messengerList').empty();
		$('#messengerWindow').toggle();
		$('#messengerBtn').toggle();
		$('#messengerCloseBtn').toggle();
		$.ajax({
			url: '/Friends/Messenger',
			dataType: 'html',
			type: 'GET',
			success: function (response) {
				if (response.indexOf("Error:") == 1) {
					console.log(response);
				}
				else {
					let messengerList = $('#messengerList');
					messengerList.append(response);
					$('.messenger-friend').each(function(index) {
						$(this).on("click", function() {
							$('#chatArea').children().css({
								'display' : 'none'
							});
							$('#messages-' + $(this).attr('Id')).show();
						});
						var idstring = $(this).attr('Id');
						selectedUserId = idstring.substr(17);
						if ($('#chatArea').has('#messages-' + idstring).length) {

						}
						else {
							$('#chatArea').append('<ul id="messages-' + idstring + '"class="messages"></ul>');
						}
					});
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