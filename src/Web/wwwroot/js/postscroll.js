let page = 0;
let docHeight = $(document).height();
let pageSize = Math.floor(docHeight / 72);
let userId = $(document.currentScript).attr('data-user_id');
let url = "/Post/" + $(document.currentScript).attr('data-url') + "Posts";
let remaining = true;

$(window).on("scroll load", function () {
	let winScrolled = $(window).height() + $(window).scrollTop();
	let docHeight = $(document).height();
	if ((docHeight - winScrolled < 1) && remaining == true) {
		$.ajax({
			url: url,
			data: { page: page, pageSize: pageSize, userId: userId },
			dataType: 'json',
			type: 'GET',
			statusCode: {
				200: function (response) {
					$("#posts").append(`
					${response.map(postTemplate).join("")}
					`);
					response.forEach(function (post) {
						let commentBtn = document.getElementById('comment-btn-' + post.id);

						commentBtn.addEventListener('click', function () {
							toggleCommentForm(commentBtn, post);
						});
						post.comments.forEach(function (comment) {
							let commentBtn = document.getElementById('comment-btn-' + comment.id, false);

							commentBtn.addEventListener('click', function () {
								toggleCommentForm(commentBtn, comment);
							});
							var replyBtns = $('.reply-btn-' + comment.id);
							replyBtns.each(function () {
								replyBtn = $(this);
								this.addEventListener('click', function () {
									toggleCommentForm(replyBtn, comment);
								});
							});
						});
					});
				},
				404: function () {
					remaining = false;
				}
			}
		});
		page = page + 1;
	}
	return false;
});

function postTemplate(post) {
	const datePosted = new Date(post.datePosted);
	var timeString = getTimeString(datePosted);
	var likeString = "Like";
	var postLikes = parseInt(post.likes);

	if (postLikes > 0) {
		if (postLikes == 1) {
			likes = `<div class="post-likes border-top">${post.likes} Like</div>`
		}
		else {
			likes = `<div class="post-likes border-top">${post.likes} Likes</div>`
		}
	}
	else {
		likes = `<div class="post-likes border-top" style="display:none;">${post.likes} Likes</div>`;
	}
	if (post.userLikes) {
		likeString = "Unlike";
	}

	//var text = sanitize(post.text);
	var template = `
		<div class="post card card-outline-primary m-1 p-1" id=${post.id}>
			<div class="post-author p-1">
				<img src="data:image/png;base64,${post.profilePicture}" style="width: 3rem; height: 3rem;" />	
				<a href="/Profile/${post.userPublicId}"> ${post.firstName} ${post.lastName}</a>
				<time class="post-date badge text-muted" title="${datePosted.toLocaleString()}">${timeString}</time>
			</div>
			<div class="post-text p-1" id="post-text">${formatPost(post.text)}</div>
			${likes}
			<div class="container-fluid">
				<div class="row border-top border-bottom" style="margin:auto">
					<div class="col-md-6 m-0 p-0">
						<input type="button" class="like-btn btn text-center text-muted m-0 like-btn" value="${likeString}" style="width: 100%">
					</div>
					<div class="col-md-6 m-0 p-0">
						<input type="button" class="btn text-center text-muted m-0" id="comment-btn-${post.id}" value="Comment" style="width: 100%">
					</div>
				</div>
			</div>
			<div class="comments">
				${post.comments.map(commentTemplate).join('')}
			</div>
		</div>
	`;

	return template;
}

function commentTemplate(comment) {
	const datePosted = new Date(comment.datePosted);
	var timeString = getTimeString(datePosted);
	var likeString = "Like";

	var commentLikes = parseInt(comment.likes);

	if (commentLikes > 0) {
		if (commentLikes == 1) {
            likes = `<div class="comment-likes" id="likes-count-${comment.id}">${comment.likes} Like</div>`
		}
		else {
			likes = `<div class="comment-likes" id="likes-count-${comment.id}">${comment.likes} Likes</div>`
		}
	}
	else {
		likes = `<div class="comment-likes" id="likes-count-${comment.id}" style="display:none;">${comment.likes} Likes</div>`;
	}

	if (comment.userLikes) {
		likeString = "Unlike";
	}

	return `
		${comment.isReply ? `<div class="cmnt reply pl-4 pt-1" id=${comment.id}>` : `<div class="comment cmnt pt-1" id=${comment.id}>`}
			<div class="comment-info pb-1">
				<img src="data:image/png;base64,${comment.profilePicture}" style="width: 1.7rem; height: 1.7rem;" />
				<a href="/Profile/${comment.authorPublicId}"> ${comment.author}</a>
				<time class="comment-date badge text-muted" title="${datePosted.toLocaleString()}">${timeString}</time>
			</div>
			<div class="comment-text" style="padding-left: .6em; padding-right: .6em">${formatPost(comment.text)}</div>
			${likes}
			<a type="button" class="comment-like-btn pl-1" id="like-${comment.id}">${likeString}</a>
			${comment.isReply ? `<a type="button" class="reply-btn reply-btn-${comment.parentCommentId}">Reply</a>` : `<a type="button" id="comment-btn-${comment.id}" class="reply-btn">Reply</a>`}
		${comment.replies ? comment.replies.map(commentTemplate).join("") : ''}
		</div>
	`
}

function sanitize(string) {
	const map = {
		'&': '&amp;',
		'<': '&lt;',
		'>': '&gt;',
		'"': '&quot;',
		"'": '&#x27;',
		"/": '&#x2F;',
	};
	const reg = /[&<>"'/]/ig;
	return string.replace(reg, (match) => (map[match]));
}

function getTimeString(datePosted) {
	const msPosted = new Date(datePosted);
	const msNow = Date.now();
	const msSince = (msNow - msPosted);
	const daysSince = Math.round(msSince / (1000 * 60 * 60 * 24));
	const hoursSince = Math.floor(msSince / 1000 / 3600);
	const minutesSince = Math.floor((msSince / 1000) / 60);
	const secondsSince = Math.floor(msSince / 1000);

	if (daysSince < 1) {
		if (minutesSince < 1) {
			return `${Math.floor(secondsSince)}s`;
		}
		else if (hoursSince < 1) {
			return `${String(Math.floor(minutesSince))}m`
		}
		else {
			return `${Math.floor(hoursSince)}h`;
		}
	}
	else if (daysSince < 7) {
		return `${Math.floor(daysSince)}d`
	}
	else {
		if (datePosted.getFullYear == Date.now.getFullYear) {
			return datePosted.toLocaleDateString([], { year: '2-digit', month: '2-digit', day: '2-digit' });
		}
		else {
			return datePosted.toLocaleDateString([], { year: '2-digit', month: '2-digit', day: '2-digit' });
		}
	}
}

function toggleCommentForm(commentBtn, post) {
	let commentForm = document.getElementById('newComment');
	let commentId = "";
	let dest;
	let postId = "";
	if (post.hasOwnProperty('comments')) {
		dest = commentBtn;
		$(dest).closest('.post').append(commentForm);
		// dest = $(document.getElementById(post.id));
		postId = post.id;
	}
	else {
		dest = $(document.getElementById(post.id));
		commentId = post.id;
		$(dest).append(commentForm);
		postId = post.postId;
	}

	$(commentForm).find('#comment-id').val(commentId);
	$(commentForm).find('#post-id').val(postId);
	if ($(commentForm).is(':visible') == false) {
		$(commentForm).toggle();
	}

	let commentTextBox = document.getElementById('commentTextForm');
	commentTextBox.value = '';
	commentTextBox.scrollIntoView({ behavior: 'smooth', block: 'center' });
	commentTextBox.focus();
	commentTextBox.select();
}

function formatPost(post)
{
	html = marked.parse(post);
	return html;
}