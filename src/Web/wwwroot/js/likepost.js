$("#posts").on("click", "input.like-btn", function(e) {
	var postId = $(e.target).closest('.post').find('.post-id').text();

	$.ajax({
		url: '/Post/Like',
		data: {postId},
		dataType: 'html',
		type: 'POST',
		statusCode: {
			200: function (response) {
				var likes = $(e.target).closest('.post').find('.likes');
				var likecount = likes.text();
				if (response == "Liked") {
					var newLikes = parseInt(likecount)+1;
				}
				else if (response == "Unliked") {
					var newLikes = parseInt(likecount)-1;
				}
				if (newLikes == 0) {
					likes.text('0 Likes')
					likes.hide();
				}
				else if (newLikes == 1) {
					likes.text(newLikes + ' Like');
					likes.show();
				}
				else {
					likes.text(newLikes + ' Likes');
					likes.show();
				}
			}
		}
	});
});