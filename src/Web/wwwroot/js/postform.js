function expandTextArea(id) {
	let form = document.getElementById(id);
	if (form) {
		form.addEventListener('keyup', function () {
			this.style.height = 0;
			this.style.height = this.scrollHeight + 'px';
		}, false);
	}
}

expandTextArea('commentTextForm');
expandTextArea('postTextForm');

document.getElementById('postTextForm').addEventListener('input', function () {
	document.getElementById('markdownOutput').innerHTML = marked.parse(this.value);
});
