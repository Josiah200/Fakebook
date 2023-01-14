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
		var recievedWrapper = document.createElement("div");
		recievedWrapper.classList.add("message-wrapper", "friend-message-wrapper");
		var recievedHeader = document.createElement("div");
		recievedHeader.innerHTML = $('#name-' + user).text().split(" ")[0];
		recievedHeader.classList.add("message-header");
		var msg = document.createElement("div");
		msg.textContent = message;
		msg.classList.add("friend-message", "message")
		document.getElementById('chat-' + user).appendChild(recievedWrapper);

		recievedWrapper.appendChild(recievedHeader);
		recievedWrapper.appendChild(msg);
		document.getElementById('chat-' + selectedUserId).scrollTop = document.getElementById('chat-' + selectedUserId).scrollHeight;
	});
	
	connection.on("SendSuccess", () => {
		console.log("message sent");
	});

	document.getElementById("messengerSendBtn").addEventListener("click", function (event) {
		event.preventDefault();
		var message = document.getElementById("messengerInput").value;
		if (message !== "")
		{
			$('#messengerInput').val("");
			connection.invoke("SendMessage", selectedUserId, message).catch(function (err) {
				return console.error(err.toString());
			});
			var sentwrapper = document.createElement("div");
			sentwrapper.classList.add("message-wrapper", "sent-message-wrapper");

			var sentheader = document.createElement("div");
			sentheader.innerHTML = $(".first-name").text();
			sentheader.classList.add("message-header");

			var sentmsg = document.createElement("div");
			sentmsg.textContent = message;
			sentmsg.classList.add("sent-message", "message")

			document.getElementById('chat-' + selectedUserId).appendChild(sentwrapper);
			sentwrapper.appendChild(sentheader);
			sentwrapper.appendChild(sentmsg);

			document.getElementById('chat-' + selectedUserId).scrollTop = document.getElementById('chat-' + selectedUserId).scrollHeight;
		}
	});
	document.getElementById("messengerInput").addEventListener("keydown", function (event) {
		if (event.key === 'Enter') {
			event.preventDefault();
			if (document.getElementById("messengerInput").value !== "")
			{
				document.getElementById("messengerSendBtn").click();
			}
		}
	})
	let btn = document.getElementById('messengerBtn');
	btn.addEventListener('click', function() {
		$('#messengerWindow').toggle();
		$('#messengerBtn').toggle();
		$('#messengerCloseBtn').toggle();
	});

	let closeBtn = document.getElementById('messengerCloseBtn');
	closeBtn.addEventListener('click', function() {
		$('#messengerWindow').toggle();
		$('#messengerBtn').toggle();
		$('#messengerCloseBtn').toggle();
	})

	$.ajax({
		url: 'Friends/Messenger',
		dataType: 'html',
		type: 'GET',
		success: function (response) {
			if (response.indexOf("Error:") == 1) {
				console.log(response);
			}
			else {
				let messengerList = $('#messengerList');
				messengerList.append(response);
				$('.messenger-friend').each(function (index) {
					$(this).on("click", function () {
						selectedUserId = $(this).attr('Id');
					});

					var idstring = $(this).attr('Id');
					if ($('#chatArea').has('#chat-' + idstring).length) {

					}
					else {
						document.getElementById('chatArea').appendChild(document.getElementById('chat-' + idstring));
						document.getElementById('chat-' + idstring).scrollTop = document.getElementById('chat-' + idstring).scrollHeight;
					}

					var triggerTabList = [].slice.call(document.querySelectorAll('.messenger-friend'))
					triggerTabList.forEach(function (triggerEl) {
						var tabTrigger = new bootstrap.Tab(triggerEl)

						triggerEl.addEventListener('click', function (event) {
							event.preventDefault()
							tabTrigger.show()
						})
					});
				});
			}
		}
	});
});