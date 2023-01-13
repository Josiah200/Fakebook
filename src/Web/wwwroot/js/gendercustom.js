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
	$("#gendercustombtn").removeClass('active');
	$('#genderfemalebtn').removeClass('active');
	$('#gendermalebtn').addClass('active');
});

document.getElementById('femaleradio').addEventListener('click', function() {
	$('.genderinput').val('Female');
	$('#gendermalebtn').removeClass('active');
	$("#gendercustombtn").removeClass('active');
	$('#genderfemalebtn').addClass('active');
});

document.getElementById('gendercustom').addEventListener('click', function() {
	$(".genderinput").val('');
	$('#gendermalebtn').removeClass('active');
	$('#genderfemalebtn').removeClass('active');
	$("#gendercustombtn").addClass('active');
});