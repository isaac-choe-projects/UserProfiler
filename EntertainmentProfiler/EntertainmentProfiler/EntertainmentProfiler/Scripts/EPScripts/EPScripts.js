$(document).ready(function () {
    // Bind the over lay navigation click events
    $('.menu-toggle').click(function () {
        $(this).toggleClass('active');
        $('#overlay').toggleClass('open');
    });
});

function showLoadingAnimation()
{
    $('.main-content-container').css({ opacity: 0.1 });
    $('#loading-animation-id').show();
}

function hideLoadingAnimation() {
    $('.main-content-container').css({ opacity: 1 });
    $('#loading-animation-id').hide();
}