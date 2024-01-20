function onFormatHours(v) {
    const hours = document.getElementById("hours");
    if (v > 85)
        hours.value = 85;
    else if (v < 0)
        hours.value = 0;

    onFormatTime();
}

function onFormatMinutes(v) {
    const minutes = document.getElementById("minutes");

    if (v > 59)
        minutes.value = 59;
    else if (v < 0)
        minutes.value = 0;

    onFormatTime();
}

function onFormatTime() {
    const hours = document.getElementById("hours");
    const minutes = document.getElementById("minutes");

    const length = document.getElementById("length");

    const mul = hours.value * 60;
    length.value = +mul + +minutes.value;
}

function editBeforeSubmit() {
    const hours = document.getElementById("hours");
    const minutes = document.getElementById("minutes");

    hours.remove();
    minutes.remove();

    const movieForm = document.getElementById("add-movie-form");

    movieForm.submit();
}