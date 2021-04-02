var page = 0;
var docHeight = $(document).height();
var blockSize = Math.floor(docHeight / 72);
var userId = $(document.currentScript).attr('data-user_id');
var url = "/Post/" + $(document.currentScript).attr('data-url')  + "Posts";
var remaining = true;

$(window).on("scroll load", function()
{
	var winScrolled = $(window).height() + $(window).scrollTop();
	var docHeight = $(document).height();

	if ((docHeight - winScrolled < 1) && remaining == true)
	{
		$.ajax({
			url: url,
			data: { page: page, blockSize: blockSize, userId: userId },
			dataType: 'html',
			type: 'GET',
			statusCode: {
				200: function(response) {
					$("#posts").append(response);
				},
				404: function() {
					remaining = false;
				}
			}
		});
		page = page + 1;	
	}
	return false;
	});
