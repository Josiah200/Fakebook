document.querySelectorAll("#photoform, #editform, #pidform").forEach(form => {
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
			const updateTarget = form.querySelector('.updateresponse')
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

$('#profileEditModal').on('hidden.bs.modal', function () {
	if($("#photoupdateresponse").contents().length) {
		window.location.href = '../Profile';
	}
})

$(function ($) {
    $.validator.addMethod('allowedfileextensions', function (value, element, params) {
		if (value.includes(".jpeg") || value.includes(".jpg") || value.includes(".png"))
		{
			return true;
		}
		return false;
    });

    $.validator.unobtrusive.adapters.add('allowedfileextensions', ['expectedvalue'],
        function (options) {

            // Add validation rule for HTML elements that contain data-testvalidation attribute

            options.rules['allowedfileextensions'] = {

                // pass the data from data-testvalidation-expectedvalue to
                // the params argument of the testvalidation method

                expectedvalue: options.params['expectedvalue']
        };
        // get the error message from data-testvalidation-expectedvalue
        // so that unobtrusive validation can use it when validation rule fails
        options.messages['maxfilesize'] = options.message;
    });
}(jQuery));

$(function ($) {
    $.validator.addMethod('maxfilesize', function (value, element, params) {
		if (element.files[0].size > 1048576)
		{
			return false;
		}
		return true;
    });

    $.validator.unobtrusive.adapters.add('maxfilesize', ['expectedvalue'],
        function (options) {

            options.rules['maxfilesize'] = {

                expectedvalue: options.params['expectedvalue']
        };
        options.messages['maxfilesize'] = options.message;
    });
}(jQuery));

$('#profileEditModal').on('hidden.bs.modal', function () {
	let url = window.location.toString().split("/").pop();
	window.location.replace(window.location.toString().replace('/' + url, ''));
});