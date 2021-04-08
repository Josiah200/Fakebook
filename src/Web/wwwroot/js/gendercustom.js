let custombtn = document.getElementById('gendercustom');

custombtn.addEventListener('click', function() {
		if(!($('#gendercustombtn').hasClass('active')))
		{
			$(".genderinput").toggle();
		}
});

document.querySelectorAll('.genderradio').forEach(item => {
	item.addEventListener('click', event => {
		if($('#gendercustombtn').hasClass('active'))
		{
			$(".genderinput").toggle();
		}
	})
});

document.getElementById('maleradio').addEventListener('click', function() {
	$('.genderinput').val('Male');
});

document.getElementById('femaleradio').addEventListener('click', function() {
	$('.genderinput').val('Female');
});

document.getElementById('gendercustom').addEventListener('click', function() {
	$(".genderinput").val('');
});