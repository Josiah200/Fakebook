var page = 0;
var docHeight = $(document).height();
var blockSize = Math.floor(docHeight / 72);
$(window).on("scroll load", function()
{
	var winScrolled = $(window).height() + $(window).scrollTop();
	var docHeight = $(document).height();
	if (window.location.href.toLowerCase().includes("/profile/"))
	{
		var userid = window.location.href.substring(window.location.href.lastIndexOf("/") + 1, window.location.href.length)
	}
	if (docHeight - winScrolled < 1)
	{
		$.ajax({
			url: '/Post/PostScroll',
			data: { page: page, blockSize: blockSize, userPublicId: userid },
			dataType: 'html',
			type: 'GET',
			success: function(response) {
				if (response.indexOf("Error:") == 1) {
					console.log(response);
				}
				else {
					$("#posts").append(response);
				}
			}
		});
		page = page + 1;	
	}
	return false;
});