/*function onSeatClick(id) {
    const seat = document.getElementById(id);

    if (seat.classList.contains("seat-available")) {
        seat.classList.remove("seat-available");
        seat.classList.add("seat-selected");
        document.getElementById("selected-seats-text").value += " " + id;
    }
    else if (seat.classList.contains("seat-selected")) {
        seat.classList.remove("seat-selected");
        seat.classList.add("seat-available");

        const seats = document.querySelectorAll(".seat-selected");

        document.getElementById("selected-seats-text").value = ""

        seats.forEach((seat) => {
            document.getElementById("selected-seats-text").value += " " + seat.id;
        })
    }
}*/

/*function onButtonSubmit() {
    const seats = document.querySelectorAll(".seat-selected");

    if (seats.length <= 0) {
        alert("At least one seat must be selected to make a reservation.");
        return;
    }

    document.getElementById("reserve-form").submit();
}*/

/*function onDateTimeSelected(id) {
    const tickeView = document.getElementById("ticketView");

    if (tickeView.hidden) {
        tickeView.hidden = false;
        document.getElementById(id).classList.add("provoles-selected");
        return;
    }

    tickeView.hidden = true;
    const selected = document.querySelectorAll(".provoles-selected");

    selected.forEach((s) => {
        s.classList.remove("provoles-selected");
    });
}*/