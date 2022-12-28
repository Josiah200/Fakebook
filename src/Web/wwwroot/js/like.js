$("#posts").on("click", "input.like-btn", function(e) {
    var postId = $(e.target).closest('.post').attr('id');
    var likeBtn = $(e.target).closest('.like-btn');
	$.ajax({
		url: '/Post/Like',
		data: { postId },
		dataType: 'html',
		type: 'POST',
		statusCode: {
			200: function (response) {
				var likes = $(e.target).closest('.post').find('.post-likes');
				var likecount = likes.text();
				if (response == "Liked") {
					var newLikes = parseInt(likecount) + 1;
					likeBtn.val('Unlike');
				}
				else if (response == "Unliked") {
					var newLikes = parseInt(likecount) - 1;
					likeBtn.val('Like');
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
					likes.show()
				}
			}
		}
	});
});

$("#posts").on("click", "a.comment-like-btn", function (e) {
	// var commentId = $(e.target).closest('.cmnt').attr('id');
	var commentId = $(e.currentTarget).attr('id').substring(5);
	$.ajax({
		url: '/Comment/Like',
		data: { commentId },
		dataType: 'html',
		type: 'POST',
		statusCode: {
			200: function (response) {
				var btn = $(`#like-${commentId}`);
				var likes = $(`#likes-count-${commentId}`);
				var likecount = likes.text();
				if (response == "Liked") {
					var newLikes = parseInt(likecount) + 1;
					btn.text('Unlike');
				}
				else if (response == "Unliked") {
					var newLikes = parseInt(likecount) - 1;
					btn.text('Like');
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