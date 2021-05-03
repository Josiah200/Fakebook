let page = 0;
let docHeight = $(document).height();
let pageSize = Math.floor(docHeight / 72);
let userId = $(document.currentScript).attr('data-user_id');
let url = "/Post/" + $(document.currentScript).attr('data-url')  + "Posts";
let remaining = true;

$(window).on("scroll load", function() {
	let winScrolled = $(window).height() + $(window).scrollTop();
	let docHeight = $(document).height();
	if ((docHeight - winScrolled < 1) && remaining == true)
	{
		$.ajax({
			url: url,
			data: { page: page, pageSize: pageSize, userId: userId },
			dataType: 'json',
			type: 'GET',
			statusCode: {
				200: function(response) {
					$("#posts").append(`
					${response.map(postTemplate).join("")}
					`);
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

function postTemplate(post) {
	const datePosted = new Date(post.datePosted);
	var timeString = getTimeString(datePosted);
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

	var text = sanitize(post.text);

	return `
		<div class="post card card-outline-primary m-1 p-1">
			<span name="PostId" class="post-id" style="display: none;">${post.id}</span>
			<div class="post-author p-1">
				<img src="data:image/png;base64,${post.profilePicture}" style="width: 3rem; height: 3rem;" />
				<a href="/Profile/${post.userPublicId}"> ${post.firstName} ${post.lastName}</a>
				<time class="post-date badge text-muted" title="${datePosted.toLocaleString()}">${timeString}</time>
			</div>
			<div class="post-text p-1" id="post-text">${text}</div>
			${likes}
			<div class="container-fluid">
				<div class="row border-top border-bottom" style="margin:auto">
					<div class="col-md-6 m-0 p-0">
						<input type="button" class="like-btn btn text-center text-muted m-0 like-btn" value="Like" style="width: 100%">
					</div>
					<div class="col-md-6 m-0 p-0">
						<input type="button" class="btn text-center text-muted m-0" value="Comment" style="width: 100%">
					</div>
				</div>
			</div>
			<div class="Comments">
				${post.comments.map(commentTemplate).join("")}
			</div>
		</div>
	`;
}

function commentTemplate(comment)
{
	const datePosted = new Date(comment.datePosted);
	var timeString = getTimeString(datePosted);

	var commentLikes = parseInt(comment.likes);

	if (commentLikes > 0) {
		if (commentLikes == 1) {
			likes = `<div class="comment-likes">${comment.likes} Like</div>`
		}
		else {
			likes = `<div class="comment-likes">${comment.likes} Likes</div>`
		}
	}
	else {
		likes = `<div class="comment-likes" style="display:none;">${comment.likes} Likes</div>`;
	}

	return `
		<div class="comment pt-1">
			<span name="CommentId" class="comment-id" style="display: none;">${comment.id}</span>
			<div class="comment-info">
				<img src="data:image/png;base64,${comment.profilePicture}" style="width: 1.7rem; height: 1.7rem;" />
				<a href="/Profile/${comment.authorPublicId}"> ${comment.author}</a>
				<time class="comment-date badge text-muted" title="${datePosted.toLocaleString()}">${timeString}</time>
			</div>
			<div class="comment-text" style="padding-left: .6em; padding-right: .6em">${comment.text}</div>
			${likes}
			<a type="button" class="comment-like-btn pl-1">Like</a>
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

function getTimeString(datePosted)
{
	const timeSince = (new Date().getTime() - datePosted.getTime());
	const daysSince = Math.round(timeSince / (1000 * 60 * 60 * 24));
	const hoursSince = timeSince / 1000 / 3600;
	const minutesSince = Math.floor((timeSince / 1000) / 60);
	const secondsSince = timeSince / 1000;

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
			return datePosted.toLocaleDateString([], {year: '2-digit', month: '2-digit', day: '2-digit'});
		}
		else {
			return datePosted.toLocaleDateString([], { year: '2-digit', month: '2-digit', day: '2-digit' });
		}
	}
}
