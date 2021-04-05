$(window).on("load", function()
{
	let btn = document.getElementById('friendsBtn');
	let friendsrequested = false;
	btn.addEventListener('click', function() {
		let headerHeight = $('.navbar').outerHeight();
		$('#friends_bar').css('margin-top', headerHeight);
		$("#friends_bar").toggleClass('friends-bar-opened friends-bar-closed');
		if (friendsrequested == false)
		{
			friendsrequested = true;
			$.ajax({
				url: '/Friends',
				dataType: 'html',
				type: 'GET',
				success: function(response) {
					if (response.indexOf("Error:") == 1) {
						console.log(response);
					}
					else {
						$('#friends').append(response);
					}
				}
			});
		}
	});

	$.ajax({
		url: '/Friends/Requests',
		dataType: 'html',
		type: 'GET',
		success: function(response) {
			if (response.indexOf("Error:") == 1) {
				console.log(response);
			}
			else {
				if (response.length > 10)
				{
					$('#requests_num').show();
					$('#friends').append(response);
					$('#requests_num').html($('#friends > #request').length);
				}
			}
		}
	});
});