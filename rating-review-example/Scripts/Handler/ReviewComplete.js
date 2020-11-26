
$(document).ready(function () {
    var currentScore = 5;
    $("#down_time").text(currentScore);
    const downTimeHandler = setInterval(() => {
        currentScore--;
        $("#down_time").text(currentScore);
        if (currentScore === 0) {
            clearInterval(downTimeHandler);
           window.location.href = '/Home/Rating'
        }
    }, 1000);
});