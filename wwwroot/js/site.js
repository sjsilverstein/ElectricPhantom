// Write your JavaScript code.
$(document).ready(function(){
    console.log("JS document ready!")

    //Lock custom navbar when scrolled.
    $(window).scroll(function() {
        var distanceFromTop = $(document).scrollTop();
        if (distanceFromTop >= $('#Navbar').height())
        {
            $('#Navbar').addClass('custom_navbar');
        }
        else
        {
            $('#Navbar').removeClass('custom_navbar');
        }
    });
    $('.carousel').carousel({        
        interval: 2000
    });

    //Show Hidden Gallery Full Size Display on Img click.
    $('img.img_moon_phase_thumbnail').on( "click", function() {
        $('#fullscalegalleryimg').show();
        var srcImg = $(this).attr("src");
        var imgName = $(this).attr("alt");
        var imgDesc = $(this).attr("data-desc");
        $('#fullscalegalleryimg img').attr("src", srcImg);
        $('#fullscalegalleryimg h1').html(imgName);
        $('#fullscalegalleryimg p').html(imgDesc);
        console.log(srcImg);
      });
    //Moon Galllery on Hover add border styling
    $('div.img_box').hover(function(){
        $(this).addClass('moon_border');
    },function(){
        $(this).removeClass('moon_border');
    });
    //Hidden Gallery Full Size Display on click.
      $("#fullscalegalleryimg").click(function(){
		$(this).hide();
	});
});