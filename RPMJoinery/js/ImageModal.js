

$('.myImg').click(function () {
    // Get the modal
    var modal = document.getElementById('myModal');

    // Get the image and insert it inside the modal - use its "alt" text as a caption
    var img = $('.myImg');
    var modalImg = $("#img01");
    var captionText = document.getElementById("caption");

    modal.style.display = "block";
    var newSrc = this.src;
    var cap = this.alt;
    modalImg.attr('src', newSrc);
    captionText.innerHTML = cap;
});

$('.myImg2').click(function () {
    // Get the modal
    var modal = document.getElementById('myModal');

    // Get the image and insert it inside the modal - use its "alt" text as a caption
    var img = $('.myImg2');
    var modalImg = $("#img01");
    var captionText = document.getElementById("caption");

    modal.style.display = "block";
    var newSrc = this.src;
    var cap = this.alt;
    modalImg.attr('src', newSrc);
    captionText.innerHTML = cap;
});

$('.myImg3').click(function () {
    // Get the modal
    var modal = document.getElementById('myModal');

    // Get the image and insert it inside the modal - use its "alt" text as a caption
    var img = $('.myImg3');
    var modalImg = $("#img01");
    var captionText = document.getElementById("caption");

    modal.style.display = "block";
    var newSrc = this.src;
    var cap = this.alt;
    modalImg.attr('src', newSrc);
    captionText.innerHTML = cap;
});

$('.myImg4').click(function () {
    // Get the modal
    var modal = document.getElementById('myModal');

    // Get the image and insert it inside the modal - use its "alt" text as a caption
    var img = $('.myImg4');
    var modalImg = $("#img01");
    var captionText = document.getElementById("caption");

    modal.style.display = "block";
    var newSrc = this.src;
    var cap = this.alt;
    modalImg.attr('src', newSrc);
    captionText.innerHTML = cap;
});

// Get the <span> element that closes the modal
//var span = document.getElementsByClassName("close")[0];

// When the user clicks on <span> (x), close the modal
//span.onclick = function () {
//    modal.style.display = "none";
//}