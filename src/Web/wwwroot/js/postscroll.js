let page = 0;
let docHeight = $(document).height();
let blockSize = Math.floor(docHeight / 72);
let userId = $(document.currentScript).attr('data-user_id');
let url = "/Post/" + $(document.currentScript).attr('data-url')  + "Posts";
let remaining = true;

$(window).on("scroll load", function()
{
	let winScrolled = $(window).height() + $(window).scrollTop();
	let docHeight = $(document).height();

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
