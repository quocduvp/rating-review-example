
$(document).ready(function () {
    var scoreId = -1;
    var currentScore = 0;
    var definedScores = [];
    renderInit().catch(error => {
        alert(error.message)
    });

    getListServices()
        .then(services => {
            let passCode = window.localStorage.getItem('passCode')
            if (passCode) {
                passCode = JSON.parse(passCode)    
                const currentService = services.find(v => v.Id === passCode.ServiceId)
                
                $('#service_question').html(currentService.Question)
            }
            
        })
        .catch(error => {})

    // submit form
    $("#submit_review").click(() => {
        submitReview();
    });

    async function renderInit() {
        const listRatings = $("#list_ratings");
        definedScores = await getDefinedScores();
        definedScores.forEach((data) => {
            listRatings.append(
                `
              <div id="rating_${data.Score}" class="m-1">
                  <img class="rating-stars" style="width: 40px" src="/Assets/star-not-rating.png" />
              </div>
            `
            );

            $(`#rating_${data.Score}`).click(() => {
                clearCurrentSelect();
                currentScore = data.Score;
                scoreId = data.Id
                autoSelect();
            });
        });
        autoSelect();
    }

    function clearCurrentSelect() {
        $("#rating_response").html(``);
        $(".rating-stars").attr("src", "/Assets/star-not-rating.png");
    }

    function autoSelect() {
        for (let a = 1; a <= currentScore; a++) {
            const rating = definedScores.find((v) => v.Score === currentScore);
            if (!rating) {
                return;
            }
            $("#rating_response").html(`
                  <div class=" mt-3">
                    <img style="width: 40px" src="${rating.Icon}" />
                  </div>
                  <div>
                    <h6>
                      <b>${rating.Text}</b>
                    </h6>
                  </div>
          `);
            $(`#rating_${a} .rating-stars`).attr("src", "/Assets/star.png");
        }
    }

    function submitReview() {
        const text = $("#feedback").val();
        handkeSubmitReview(scoreId, text).then(v => {
            window.location.href = '/Home/ReviewComplete'
        }).catch(error => {
            alert("Đánh giá thất bại vui lòng thử lại")
        })
    }
});





