document.querySelectorAll("#photoform, #editform").forEach(form => {
	const document = form.ownerDocument;
	form.addEventListener('click', evt => {
		form.submitter = evt.target.closest('[type=submit]')
	});

	form.addEventListener('submit', evt => {
		evt.preventDefault();
	
		const formData = new FormData(form);
		if (form.submitter) {
			formData.append(form.submitter.name, form.submitter.value);
			form.submitter.setAttribute('disabled', 'disabled');
			form.submitter.classList.add('loading');
			$(form.submitter).attr("btn-data", $(form.submitter).html());
			$(form.submitter).html('<span class="spinner-border spinner-border-sm" role="status" aria-hiden="true"></span> Loading...');
		}
		form.child
		fetch(form.action, {
			method: 'POST',
			headers: {
				'X-Requested-With': 'XmlHttpRequest'
			},
			body: formData
		})
		.then(response => {
			if (form.submitter) {
				form.submitter.removeAttribute('disabled');
				form.submitter.classList.remove('loading');
				$(form.submitter).html($(form.submitter).attr("btn-data"));
		}
		if (!response.ok) throw response;
			return response.text()
		}).then(html => {
			const updateTarget = form.querySelector('#updateresponse')
			if (updateTarget) {
				const updateType = form.dataset.updateType || 'replace';
				if (updateType === 'replace') {
					updateTarget.innerHTML = html
				}
				else /* append */ {
					const div = document.createElement('div');
					div.innerHTML = html;
					updateTarget.appendChild(div.firstChild);
				}
			}
		})
	})
});
document.querySelectorAll('.editformclose')
	.forEach(btn => {
		btn.addEventListener('click', evt => {
			if($("#updateresponse").contents().length) {
				window.location.href = '../Profile';
			}
		});
});