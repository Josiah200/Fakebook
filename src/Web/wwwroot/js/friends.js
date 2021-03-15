var friendsFetched = false;
function toggleFriends() {
	var headerHeight = $('.navbar').outerHeight();
	$('#friends_bar').css('margin-top', headerHeight);
	$("#friends_bar").toggleClass('friends-bar-opened friends-bar-closed');
};

$(window).on("load", function()
{
	$.ajax({
		url: '../Friends/Requests',
		dataType: 'html',
		type: 'GET',
		success: function(response) {
			if (response.indexOf("Error:") == 1) {
				console.log(response);
			}
			else {
				$('#requests_num').show();
				$('#friends').append(response);
			}
		}
	});
});